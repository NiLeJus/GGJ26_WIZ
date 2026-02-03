using UnityEngine;
using System.Collections;

public class DamageSystem : MonoBehaviour
{
    // a ajouter au game object damage system

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private ImageSequence imagesqc;
    
    void OnEnable()
    {
            ActionSystem. AttachPerformer<GADealDamage>(DealDamagePerformer);
            ActionSystem.AttachPerformer<GAHeal>(HealPerformer);
    }
    
    
    void OnDisable()
    {
            ActionSystem.DetachPerformer<GADealDamage>();
            ActionSystem.DetachPerformer<GAHeal>();
    }
    

    private IEnumerator DealDamagePerformer(GADealDamage gaDealDamage)
    {
        foreach (var target in gaDealDamage.Targets)
        {
            target.Damage(gaDealDamage.Amount);
          //  Instantiate(damageVFX, target.transform.position, Quaternion. identity);
            yield return new WaitForSeconds(0.15f);
            //ajouter CFXR Hit D 3D (Yellow) (game object de l'asset fx, trouvable dans le folder impacts) à mettre dans le game object damagesystem
            if (target.CurrentHealth <= 0)
            {
                if (target is EnemyView enemyView)
                {
                    GAKillEnemy gaKillEnemy = new(enemyView);
                    ActionSystem. Instance. AddReaction(gaKillEnemy);
                    GameOverScreen.SetActive(true);
                    imagesqc.transform.gameObject.SetActive(true);
                    imagesqc.StartSequence();
                    
                }
                else
                {
                    GameOverScreen.SetActive(true);
                    imagesqc.transform.gameObject.SetActive(true);
                    imagesqc.StartSequence();       
                }
                         
            }
            else
            {
              
            }


// Do some game over logic
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
