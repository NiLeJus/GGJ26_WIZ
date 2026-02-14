using System;
using UnityEngine;

public class OnEnemyAttackCondition : PerkCondition
{
    public override bool SubConditionIsMet(GameAction gameAction)
    {
        return true;
    }

    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.SubscribeReaction<GAAttackHero>(reaction, reactionTiming);
    }
    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.UnsubscribeReaction<GAAttackHero>(reaction, reactionTiming);
    }
}
