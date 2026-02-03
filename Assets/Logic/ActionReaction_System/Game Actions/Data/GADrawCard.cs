using UnityEngine;

public class GADrawCards : GameAction
{
  public int Amount { get; set; }

  public GADrawCards(int amount)
  {
    Amount = amount;
  }
}
