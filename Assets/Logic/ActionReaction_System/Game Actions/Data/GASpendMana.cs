using UnityEngine;

public class GASpendMana : GameAction
{
    public int Amount { get; set; }

    public GASpendMana(int amount)
    {
        Amount = amount;
    }

   
}
