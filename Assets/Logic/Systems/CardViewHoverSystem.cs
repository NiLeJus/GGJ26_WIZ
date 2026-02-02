using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
   [SerializeField] private CardView cardViewHover;

   public void Show(Card card, Vector3 position)
   {
      cardViewHover.gameObject.SetActive(true);
      cardViewHover.Setup(card);
      cardViewHover.transform.position = new Vector3(position.x, position.y + 3f, position.z);

   }

   public void Hide()
   {
      cardViewHover.gameObject.SetActive(false);
   }
}
