using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DealDamageEffect : Effect
{
    [SerializeField] private int damageAmount;

    public override GameAction GetGameAction()  // Correction: "GamcAction" → "GameAction"
    {
        List<CombatantView> targets = new(EnemySystem.Instance.Enemies);  // Tous ennemis actuels
        GADealDamage gaDealDamage = new(damageAmount, targets);  // Correction: supprimé doublon "ealDamage"
        return gaDealDamage;
    }
}