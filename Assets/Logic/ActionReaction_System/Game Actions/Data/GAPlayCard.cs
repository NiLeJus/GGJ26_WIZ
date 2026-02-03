using UnityEngine;

public class GAPlayCard : GameAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Card Card { get; set; }

    public GAPlayCard(Card card)
    {
        Card = card;
    }
}
