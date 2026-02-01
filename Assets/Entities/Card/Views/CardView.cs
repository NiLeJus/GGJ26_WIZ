using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{ 
    
    //Card source
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private TMP_Text description;
    [SerializeField] private SpriteRenderer imageSR;
    
    [SerializeField] private GameObject wrapper;
    [SerializeField] private LayerMask dropLayer; 
    
    //Position for Dragging
    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;

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

        if (!Interactions.Instance.PlayerCanHover()) return;
        
        Debug.Log("M enter");
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardViewHoverSystem.Instance.Show(Card, pos);
    }

    void OnMouseExit()
    { 
        if (!Interactions.Instance.PlayerCanHover()) return;
      CardViewHoverSystem.Instance.Hide();
      wrapper.SetActive(true);
    }

    
    void OnMouseDown()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        Interactions.Instance.PlayerIsDragging = true;
        wrapper.SetActive(true);
        CardViewHoverSystem.Instance.Hide();
        dragStartPosition = transform.position;
        dragStartRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, dropLayer))
        {
        Debug.Log(hit.transform.name);
        }
    }

    void OnMouseDrag()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1); 
    }

    void OnMouseUp()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        if (ManaSystem.Instance.HasEnoughMana(Card.Mana) && (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, dropLayer)))
        {
            GAPlayCard gaPlayCard = new(Card);
            ActionSystem.Instance.Perform(gaPlayCard);
        }
        else
        {
            transform.position = dragStartPosition;
            transform.rotation = dragStartRotation ;
        }
        Interactions.Instance.PlayerIsDragging = false;
    }
}



/*

// Dans void OnMouseUp MaJ de :
if (ManaSystem.Instance.HasEnoughMana(Card.Mana) &&(Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, dropLayer))

*/

