using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DrawCardsEffect : Effect
{
   [SerializeField] private int drawAnount;
   
   public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
   {
      GADrawCards gaDrawCards = new(drawAnount);
      return gaDrawCards;
   }
}
