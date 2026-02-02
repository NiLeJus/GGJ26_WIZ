using DG.Tweening;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private SpriteRenderer spriteRenderer; // Si utilisé

    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    //voir si la current health est un sprite ou int
    public virtual void SetupBase(int health, Sprite image)
    {
        MaxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;
        UpdateHealthText();
    }

    protected void UpdateHealthText()
    {
        healthText.text = 
          //  "HP: " +
          CurrentHealth + "/" + MaxHealth;
    }
    
    public void Damage(int damageAmount){
     
        CurrentHealth -= damageAmount;
        if (CurrentHealth < 0){
     

            CurrentHealth = 0;}

        transform.DOShakePosition(0.2f, 0.5f) ;
        UpdateHealthText();
    }

    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        UpdateHealthText();
    }

}
