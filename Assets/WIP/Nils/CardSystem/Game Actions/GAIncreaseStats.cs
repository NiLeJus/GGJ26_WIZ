using UnityEngine;

public class GAIncreaseStats : GameAction
{
    public Minion Target;
    public int AttackIncreaseAmount;

    public int HealthIncreaseAmount;

    public GAIncreaseStats (Minion target, int attackIncreaseAmount, int healthIncreaseAmount)
    {
        Target = target;
        AttackIncreaseAmount = attackIncreaseAmount;
        HealthIncreaseAmount = healthIncreaseAmount;
    }
}

public class Minion
{
    
}