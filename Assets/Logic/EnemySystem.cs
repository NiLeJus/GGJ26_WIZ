using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
   [SerializeField] private float interTurnDelay = 2f;
   [SerializeField] public EnemyBoardView enemyBoardView;
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
   
   private IEnumerator KillEnemyPerformer(GAKillEnemy killEnemyGA)
   {
      yield return enemyBoardView.RemoveEnemy(killEnemyGA. EnemyView);
   }
   
   private IEnumerator EnemyTurnPerformer(GAEnemyTurn enemyTurnGA)
   {
      Debug.Log("Enemy Turn Started");


      foreach (var enemy in enemyBoardView.EnemyViews)
      {
         GAAttackHero attackHeroGA = new(enemy);
         ActionSystem.Instance.AddReaction(attackHeroGA);


      }
      Debug.Log("Enemy Turn Ends");

      yield return null;

   }


   private IEnumerator AttackHeroPerformer(GAAttackHero gaAttackHero)
   {
      EnemyView attacker = gaAttackHero.Attacker;
      Tween tween = attacker. transform. DOMoveX(attacker. transform.position.x - 1f, 0.15f);
      yield return tween.WaitForCompletion();
      attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.25f);
      //deal damage
      GADealDamage gaDealDamage = new(attacker.AttackPower, new() { HeroSystem.Instance.HeroView });
      ActionSystem.Instance.AddReaction(gaDealDamage);


   }
   
   public void Setup(List<EnemyData> enemyDatas){

      foreach (var enemyData in enemyDatas)
         enemyBoardView.AddEnemy(enemyData);
   }
}
