using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatantView : MonoBehaviour
{

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private HealthBarShrink healthBarShrink;
    [SerializeField] private SpriteRenderer spriteRenderer; // Si utilisé
    [SerializeField] private StatusEffectsUI statusEffectsUI;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    private Dictionary<StatusEffectType, int> statusEffects = new();
    //voir si la current health est un sprite ou int

    protected void SetupBase(int health, Sprite image)
    {
        MaxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;
        UpdateHealthUI();
    }


    protected void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = CurrentHealth + "/" + MaxHealth;
        }

        if (healthBarShrink != null)
        {
            healthBarShrink.OnHealthChanged((float)CurrentHealth / MaxHealth);
        }
    }
    
    public void Damage(int damageAmount){
        int remainingDamage = damageAmount;
        int currentArmor = GetStatusEffectStack(StatusEffectType.ARMOR);
        if (currentArmor > 0)
        {
            if (currentArmor >= damageAmount)
            {
                RemovesStatusEffect(StatusEffectType.ARMOR, remainingDamage);
                remainingDamage = 0;
            }
            else if (currentArmor<damageAmount)
            {
                RemovesStatusEffect(StatusEffectType.ARMOR, currentArmor);
                remainingDamage -= currentArmor;
            }
        }

        if (remainingDamage > 0)
        {
            CurrentHealth -= remainingDamage;
            if (CurrentHealth < 0)
            {


                CurrentHealth = 0;
            }

        }

        transform.DOShakePosition(0.2f, 0.5f) ;
                    UpdateHealthUI();
        
    }

    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        UpdateHealthUI();
    }

    public void AddStatusEffect(StatusEffectType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] += stackCount;
        }
        else
        {
            statusEffects.Add(type, stackCount);
        }

        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStack(type));
    }

    public void RemovesStatusEffect(StatusEffectType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] -= stackCount;
            if (statusEffects[type] <= 0)
            {
                statusEffects.Remove(type);
            }
            
        }
        statusEffectsUI.UpdateStatusEffectUI(type,GetStatusEffectStack(type));
    }

    public int GetStatusEffectStack(StatusEffectType type)
    {
        if (statusEffects.ContainsKey(type)) return statusEffects[type];
        else return 0;
    }
}
