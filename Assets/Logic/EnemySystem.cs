using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
   [SerializeField] private float interTurnDelay = 2f;

   private IEnumerator EnemyTurnPerformer(GAEnemyTurn gaEnemyTurn)
   {
      Debug.Log("Enemy Turn Started");
      yield return new WaitForSeconds(interTurnDelay);
      Debug.Log("Enemy Turn Ends");
      
   }

   void OnEnable()
   {
      ActionSystem.AttachPerformer<GAEnemyTurn>(EnemyTurnPerformer);
   }

   void OnDisable()
   {
      ActionSystem.DetachPerformer<GAEnemyTurn>();
   }
}
