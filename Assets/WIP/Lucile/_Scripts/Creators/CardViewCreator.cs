using UnityEngine;
using System.Numerics;
using DG.Tweening;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private CardView cardViewPrefab;

    public CardView CreateCardView(Card card, Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(cardViewPrefab, position, rotation);
        // Clone prefab à position/rotation
        cardView.transform.localScale = Vector3.zero;
        cardView.transform.DOScale(Vector3.one,0.15f); // Pop animé vers taille normale (DOTween)
        cardView.Setup(card);
        return cardView;
    }
    
}
