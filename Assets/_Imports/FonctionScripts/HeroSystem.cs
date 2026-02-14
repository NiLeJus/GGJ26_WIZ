using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{

    [field: SerializeField] public HeroView HeroView { get; private set; }
    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<GAEnemyTurn>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<GAEnemyTurn>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<GAEnemyTurn>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<GAEnemyTurn>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
   public void Setup(HeroData heroData)
    {
     HeroView.Setup(heroData) ;
    }

    private void EnemyTurnPreReaction(GAEnemyTurn gaEnemyTurn)
    {
        GADiscardAllCards gaDiscardAllCards = new();
        ActionSystem.Instance.AddReaction(gaDiscardAllCards);
    }

    private void EnemyTurnPostReaction(GAEnemyTurn gaEnemyTurn)
    {
        int burnStacks = HeroView.GetStatusEffectStack(StatusEffectType.BURN);
        if (burnStacks > 0)
        {
            ApplyBurnGA applyBurnGa = new(burnStacks, HeroView);
            ActionSystem.Instance.AddReaction(applyBurnGa);
        }

        GADrawCards gaDrawCards = new(5);
        ActionSystem.Instance.AddReaction(gaDrawCards);
    }


}
