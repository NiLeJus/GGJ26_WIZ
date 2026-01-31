
using UnityEngine;

public class Card
{
    //Information de la carte qui ne peuvent être modifier in game
   public string Title => data.name;
   public string Description => data.Description;
   public Sprite Image => data.Image;
   
   
   //Information (que mana) de la carte changeable in game
   public int Mana { get; private set; }
   
   
   private readonly CardData data;
   
   //logique pour que seul la carte change mana pas toutes les cartes
   public Card(CardData cardData)
      {
      data = cardData;
      Mana = cardData.Mana;
      }

}
