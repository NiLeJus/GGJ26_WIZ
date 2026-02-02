using System.Collections.Generic;
using UnityEngine;

public class GADealDamage : GameAction
{

public int Amount { get; set; }

public List<CombatantView> Targets { get; set; }

public GADealDamage(int amount, List<CombatantView> targets)
{
    Amount = amount;
    Targets = new (targets);
}


}
