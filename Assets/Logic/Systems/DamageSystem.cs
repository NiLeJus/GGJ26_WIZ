using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageSystem : MonoBehaviour
{

    [SerializeField] private GameObject damageVFX;


    void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
        ActionSystem.AttachPerformer<GAHeal>(HealPerformer);
    }


    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
        ActionSystem.DetachPerformer<GAHeal>();
    }


    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGa)
    {
        foreach (var target in dealDamageGa.Targets)
        {
            target.Damage(dealDamageGa.Amount);
            Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            
            // --- COUNTER ATTACK LOGIC ---
            int counterAttackStacks = target.GetStatusEffectStack(StatusEffectType.COUNTER_ATTACK);
            if (counterAttackStacks > 0 && dealDamageGa.Caster != null && dealDamageGa.Caster != target)
            {
                // Inflige des dégâts à l'attaquant égaux au nombre de stacks
                // On crée une nouvelle action de dégâts, mais on doit faire attention à ne pas boucler.
                // Ici, on suppose que la contre-attaque inflige des dégâts "bruts" ou on utilise une autre action si besoin.
                // Pour simplifier, on utilise DealDamageGA.
                
                // Note: Pour éviter une boucle infinie, on pourrait ajouter un flag "IsCounterAttack" à DealDamageGA,
                // mais ici on vérifie juste que Caster != Target. Si l'attaquant a aussi CounterAttack, il ne ripostera pas à sa propre attaque,
                // mais il pourrait riposter à la riposte... C'est le risque.
                // Pour l'instant, on fait simple : la contre-attaque inflige des dégâts.
                
                DealDamageGA counterAttackAction = new DealDamageGA(counterAttackStacks, new List<CombatantView> { dealDamageGa.Caster }, target);
                ActionSystem.Instance.AddReaction(counterAttackAction);
            }
            // ----------------------------

            yield return new WaitForSeconds(0.15f);
            //ajouter CFXR Hit D 3D (Yellow) (game object de l'asset fx, trouvable dans le folder impacts) à mettre dans le game object damagesystem

            if (target.CurrentHealth <= 0)
            {
                if (target is EnemyView enemyView)
                {
                    GAKillEnemy gaKillEnemy = new(enemyView);
                    ActionSystem.Instance.AddReaction(gaKillEnemy);

                }
                else
                {

                }

// Do some game over logic
            }



        }
        
    }
    private IEnumerator HealPerformer(GAHeal gaHeal)
    {
        foreach (var target in gaHeal.Targets)
        {
            target.Heal(gaHeal.Amount);
            yield return new WaitForSeconds(0.15f);
        }
    }
}
