using UnityEngine;
using System;  

[Serializable]
public class DrawCardsEffect : Effect
{
   [SerializeField] private int drawAnount;
   
   public override GameAction GetGameAction()
   {
      GADrawCards gaDrawCards = new(drawAnount);
      return gaDrawCards;
   }
}
