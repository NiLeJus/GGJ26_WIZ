
using System.Collections;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    [SerializeField] private UI_Mana ui_Mana;
    
    private const int MAX_MANA = 3;
    private int currentMana = MAX_MANA;

    void OnEnable()
    {
        ActionSystem. AttachPerformer<SpendManaGAction>(SpendManaPerformer);
        ActionSystem. AttachPerformer<GARefillMana>(RefillManaPerformer);
        ActionSystem. SubscribeReaction<GAEnemyTurn>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
    
    void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGAction>();
        ActionSystem.DetachPerformer<GARefillMana>();
        ActionSystem. UnsubscribeReaction<GAEnemyTurn>(EnemyTurnPostReaction, ReactionTiming.POST);

    }

    public void Start()
    {
        ui_Mana.UpdateManaText(currentMana);
    }
    
    public bool HasEnoughMana(int mana)
    {
             return currentMana >= mana;
    }
   
    //Performers
    private IEnumerator SpendManaPerformer(SpendManaGAction spendManaGAction)
    {
        currentMana -= spendManaGAction. Amount;
        ui_Mana. UpdateManaText(currentMana);
        yield return null;
    }

    private IEnumerator RefillManaPerformer(GARefillMana gaRefillMana)
    {
        currentMana = MAX_MANA;
        ui_Mana. UpdateManaText(currentMana);
        yield return null;
    }
    
    
    //Reaction
    //Pour redonner mana debut tour :
    private void EnemyTurnPostReaction(GAEnemyTurn gaEnemyTurn)
    {
        GARefillMana gaRefillMana = new();
        ActionSystem. Instance.AddReaction(gaRefillMana);
    }
}
