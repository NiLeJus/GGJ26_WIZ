using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class HealEffect : Effect
{
    [SerializeField] private int healAmount;

    public override GameAction GetGameAction()
    {
        List<CombatantView> targets = new();
        // Assuming HeroSystem is available as it is referenced in EnemySystem
        if (HeroSystem.Instance != null && HeroSystem.Instance.HeroView != null)
        {
            targets.Add(HeroSystem.Instance.HeroView);
        }
        
        GAHeal gaHeal = new(healAmount, targets);
        return gaHeal;
    }
}
