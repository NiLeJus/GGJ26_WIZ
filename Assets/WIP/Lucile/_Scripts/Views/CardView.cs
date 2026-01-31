using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{ 
    [SerializeField] private TMP_Text title;
    
    [SerializeField] private TMP_Text mana;
    
    [SerializeField] private TMP_Text description;

    [SerializeField] private SpriteRenderer imageSR;
    
    [SerializeField] private GameObject wrapper;

    //Config de la carte avec donné, definition texte et image 
    public Card Card { get; private set; }
    public void Setup(Card card)
    {
        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();
        imageSR.sprite = card.Image;
    }
    //logique quand la souris est sur la carte s'agrandit passe en version hovver et axe x diff

    void OnMouseEnter()
    {
        Debug.Log("M enter");
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardViewHoverSystem.Instance.Show(Card, pos);

    }

    void OnMouseExit()
    {
      CardViewHoverSystem.Instance.Hide();
      wrapper.SetActive(true);
    }
}
