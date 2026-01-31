using UnityEngine;

public class CardTemplate : MonoBehaviour
{
    public void OnMouseDown()
    {
    if (ActionSystem.Instance.IsPerforming) return;
    GADrawCard GAdrawCard = new();
    ActionSystem.Instance.Perform(GAdrawCard);
    Destroy(gameObject);
    }
}
