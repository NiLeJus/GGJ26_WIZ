using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    Debug.Log("CardSystem OnEnable - Attaching performers");

    
    ActionSystem.AttachPerformer<GADrawCards>(DrawCardsPerformer);
    ActionSystem.AttachPerformer<GADiscardAllCards>(DiscardAllCardsPerformer);
    ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
    
  }

  private void OnDisable()
  {
    ActionSystem.DetachPerformer<GADrawCards>();
    ActionSystem.DetachPerformer<GADiscardAllCards>();
    ActionSystem.DetachPerformer<PlayCardGA>();

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
       int amountAfterRefill = Mathf.Min(notDrawnAmount, drawPile.Count);
       for (int i = 0; i < amountAfterRefill; i++)
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
  public void Setup(List<CardData> deckData)
  {
    Debug.Log($"Setting up CardSystem with {deckData.Count} cards.");
    foreach (var cardData in deckData)
    {
      Card card = new(cardData);
      drawPile.Add(card);
    }
  }

  
  private IEnumerator DrawCard()
  {
    Debug.Log($"DrawPile count: {drawPile.Count}");
    if (drawPile.Count == 0)
    {
        Debug.LogError("Draw pile is empty, cannot draw a card.");
        yield break; // Stop the coroutine
    }
    Card card = drawPile.Draw();
    if (card == null)
    {
        Debug.LogError("Draw() returned a null card!");
        yield break; // Stop the coroutine
    }
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

      CardView cardView = handView.RemoveCard(card);
      yield return DiscardCard(cardView);
    }
    hand.Clear();
  }

  public IEnumerator DiscardCard(CardView cardView)
  {
    discardPile.Add(cardView.Card);
    cardView.transform.DOScale(0, 0.15f).SetEase(Ease.OutElastic);
    Tween tween = cardView.transform.DOScale(1, 0.15f);
    yield return tween.WaitForCompletion();
    Destroy(cardView.gameObject);
  }
  



 private IEnumerator PlayCardPerformer(PlayCardGA playCardGa) 
  {
    if (playCardGa.Card == null)
    {
        Debug.LogError("PlayCardPerformer: La carte est null !");
        yield break;
    }

    hand.Remove(playCardGa.Card);
    
    if (handView != null)
    {
        CardView cardView = handView.RemoveCard(playCardGa.Card);
        if (cardView != null) yield return DiscardCard(cardView);
    }
    else
    {
        Debug.LogError("PlayCardPerformer: 'handView' n'est pas assigné dans l'inspecteur de CardSystem !");
    }
    
    //Spending mana Here
    SpendManaGAction spendManaGAction = new(playCardGa.Card.Mana);
    ActionSystem.Instance.AddReaction(spendManaGAction);

    if (playCardGa.Card.ManualTargetEffect != null)
    {
      GAPerformEffect gaPerformEffect = new(playCardGa.Card.ManualTargetEffect, new() { playCardGa.ManualTarget });
      ActionSystem.Instance.AddReaction(gaPerformEffect);
    }

    if (playCardGa.Card.OtherEffects != null)
    {
      foreach (var effectWrapper in playCardGa.Card.OtherEffects)
      {
        List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();
        GAPerformEffect gaPerformEffect = new(effectWrapper.Effect, targets);
        ActionSystem.Instance.AddReaction(gaPerformEffect);
      }
    }
    
    /*foreach (var gaPerformEffect in from effectWrapper in playCardGa.Card.OtherEffects let targets = effectWrapper.TargetMode.GetTargets() select new GAPerformEffect(effectWrapper.Effect, targets))
    {
      ActionSystem.Instance.AddReaction(gaPerformEffect);
    }*/
  }

}
