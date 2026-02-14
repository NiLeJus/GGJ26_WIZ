using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DealDamageEffect : Effect
{
    [SerializeField] private int damageAmount;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster) 
    {
      
        DealDamageGA dealDamageGa = new(damageAmount, targets, caster); 
        return dealDamageGa;
    }
}