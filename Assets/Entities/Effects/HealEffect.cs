using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class HealEffect : Effect
{
    [SerializeField] private int healAmount;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        // If no targets are provided (e.g., NoTM), target the caster (Self/Hero)
        if (targets == null || targets.Count == 0)
        {
            targets = new List<CombatantView> { caster };
        }

        GAHeal gaHeal = new(healAmount, targets);
        return gaHeal;
    }
}
