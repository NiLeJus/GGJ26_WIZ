using System.Collections.Generic;

public class GAHeal : GameAction
{
    public int Amount { get; }
    public List<CombatantView> Targets { get; }

    public GAHeal(int amount, List<CombatantView> targets)
    {
        Amount = amount;
        Targets = targets;
    }
}
