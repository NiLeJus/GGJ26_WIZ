using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
   [SerializeField] private float interTurnDelay = 2f;
   [SerializeField] public EnemyBoardView enemyBoardView;
   [SerializeField] private GameObject victoryScreen; // Reference to the Victory Screen

   public List<EnemyView> Enemies => enemyBoardView.EnemyViews;

   void OnEnable()
   {
      ActionSystem. AttachPerformer<GAEnemyTurn>(EnemyTurnPerformer);
      ActionSystem. AttachPerformer<GAAttackHero>(AttackHeroPerformer);
      ActionSystem.AttachPerformer<GAKillEnemy>(KillEnemyPerformer);
   }


   void OnDisable()
   {
      ActionSystem.DetachPerformer<GAEnemyTurn>();
      ActionSystem.DetachPerformer <GAAttackHero>();
      ActionSystem.DetachPerformer<GAKillEnemy>();
   }
   

   
   private IEnumerator EnemyTurnPerformer(GAEnemyTurn gaEnemyTurn)
   {
      Debug.Log("Enemy Turn Started");


      foreach (var enemy in enemyBoardView.EnemyViews)
      {
         int burnStacks = enemy.GetStatusEffectStack(StatusEffectType.BURN);
         if (burnStacks > 0)
         {
            ApplyBurnGA applyBurnGa = new(burnStacks, enemy);
            ActionSystem.Instance.AddReaction(applyBurnGa);
         }
         GAAttackHero gaAttackHero = new(enemy);
         ActionSystem.Instance.AddReaction(gaAttackHero);


      }


      yield return null;

   }


   private IEnumerator AttackHeroPerformer(GAAttackHero gaAttackHero)
   {
      EnemyView attacker = gaAttackHero.Attacker;
      Tween tween = attacker. transform. DOMoveX(attacker. transform.position.x - 1f, 0.15f);
      yield return tween.WaitForCompletion();
      attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.25f);
      //deal damage
      DealDamageGA dealDamageGa = new(attacker.AttackPower, new() { HeroSystem.Instance.HeroView }, gaAttackHero.Caster);
      ActionSystem.Instance.AddReaction(dealDamageGa);


   }
   
   public void Setup(List<EnemyData> enemyDatas){

      foreach (var enemyData in enemyDatas)
         enemyBoardView.AddEnemy(enemyData);
   }
   
   private IEnumerator KillEnemyPerformer(GAKillEnemy gaKillEnemy)
   {
      yield return enemyBoardView.RemoveEnemy(gaKillEnemy. EnemyView);
      
      // Check for victory
      if (enemyBoardView.EnemyViews.Count == 0)
      {
         Debug.Log("Victory!");
         if (victoryScreen != null)
         {
            victoryScreen.SetActive(true);
         }
      }
   }
}
