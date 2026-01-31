using System.Collections;
using DG.Tweening;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
  [SerializeField] private GameObject cardPrefab;
//  [SerializeField] private Card cardPrefab;
  [SerializeField] private Transform spawn;
  [SerializeField] private Transform hand;

  private void OnEnable()
  {
    ActionSystem.AttachPerformer<GADrawCard>(DrawCardPerformer);
  }

  private void OnDisable()
  {
    ActionSystem.DetachPerformer<GADrawCard>();
  }

  private IEnumerator DrawCardPerformer(GADrawCard GAdrawCard)
  {
    // Card card = Instantiate(cardPrefab, spawn.position, Quaternion.identity);
    Tween tween = card.transform.DOMove(hand.position, 0.5f);
    yield return tween.WaitForCompletion();
  }
}
