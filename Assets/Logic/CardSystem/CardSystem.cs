using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{

  [SerializeField] private HandView handView;
  [SerializeField] private Transform drawPilePoint;
  [SerializeField] private Transform discardPilePoint;

  private readonly List<Card> drawPile = new ();
  private readonly List<Card> discardPile = new ();
  private readonly List<Card> hand = new ();
  
  
  //Lifecycle Action System
  private void OnEnable()
  {
    Debug.Log("🔥 CardSystem OnEnable - Attaching performers");

    
    ActionSystem.AttachPerformer<GADrawCards>(DrawCardsPerformer);
    ActionSystem.AttachPerformer<GADiscardAllCards>(DiscardAllCardsPerformer);
    ActionSystem.AttachPerformer<GAPlayCard>(PlayCardPerformer);
    
    
    ActionSystem.SubscribeReaction<GAEnemyTurn>(EnemyTurnPreReaction, ReactionTiming.PRE);
    ActionSystem.SubscribeReaction<GAEnemyTurn>(EnemyTurnPostReaction, ReactionTiming.POST);
  }

  private void OnDisable()
  {
    ActionSystem.DetachPerformer<GADrawCards>();
    ActionSystem.DetachPerformer<GADiscardAllCards>();
    ActionSystem.DetachPerformer<GAPlayCard>();
    
    ActionSystem.UnsubscribeReaction<GAEnemyTurn>(EnemyTurnPreReaction, ReactionTiming.PRE);
    ActionSystem.UnsubscribeReaction<GAEnemyTurn>(EnemyTurnPostReaction, ReactionTiming.POST);
  }

  
  //Performers
  
  private IEnumerator DrawCardsPerformer(GADrawCards gaDrawCards)
    { 
      Debug.Log("Performer Draw cards");
     int actualAmount = Mathf.Min(gaDrawCards.Amount, drawPile.Count);
     int notDrawnAmount = gaDrawCards.Amount - actualAmount;
     for (int i = 0; i< actualAmount; i++)
     {
       yield return DrawCard();
     }

     if (notDrawnAmount > 0)
     {
       RefillDeck();
       for (int i = 0; i < notDrawnAmount; i++)
       {
         yield return DrawCard();
       }
     }
    }

  private void RefillDeck()
  {
    drawPile.AddRange(discardPile);
    discardPile.Clear();
  }

  private IEnumerator DrawCard()
  {
    Card card = drawPile.Draw();
    hand.Add(card);
    CardView cardView =
      CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position,
        drawPilePoint.rotation);
    
    
    yield return handView.AddCard(cardView);
    
  }

  private IEnumerator DiscardAllCardsPerformer(GADiscardAllCards gaDiscardAllCards)
  {
    foreach (var card in hand)
    {
      discardPile.Add(card);
      CardView cardView = handView.RemoveCard(card);
      yield return DiscardCard(cardView);
    }
    hand.Clear();
  }

  public IEnumerator DiscardCard(CardView cardView)
  {
    cardView.transform.DOScale(0, 0.15f).SetEase(Ease.OutElastic);
    Tween tween = cardView.transform.DOScale(1, 0.15f);
    yield return tween.WaitForCompletion();
    Destroy(cardView.gameObject);
  }
  
  //Enemy 

  private void EnemyTurnPreReaction(GAEnemyTurn gaEnemyTurn)
  {
    GADiscardAllCards gaDiscardAllCards = new();
    ActionSystem.Instance.AddReaction(gaDiscardAllCards);
  }

  private void EnemyTurnPostReaction(GAEnemyTurn gaEnemyTurn)
  {
    Debug.Log("➕ Adding DrawCards to EnemyTurn PostReactions");
    GADrawCards gaDrawCards = new(5);
    ActionSystem.Instance.AddReaction(gaDrawCards);
  }
  
 private IEnumerator PlayCardPerformer(GAPlayCard gaPlayCard) 
  {
    hand.Remove(gaPlayCard.Card);
    CardView cardView = handView.RemoveCard(gaPlayCard.Card);
    yield return DiscardCard(cardView);
    
    //Spending mana Here
    GASpendMana gaSpendMana = new(gaPlayCard.Card.Mana);
    ActionSystem.Instance.AddReaction(gaSpendMana);
    
    foreach (var effect in gaPlayCard.Card.Effects)
    {
      GAPerformEffect gaPerformEffect = new (effect);
      ActionSystem.Instance.AddReaction(gaPerformEffect);
    }
  }

  public void Setup(List<CardData> deckData)
  {
    foreach (var cardData in deckData)
    {
      Card card = new(cardData);
      drawPile.Add(card);
    }
  }
}
