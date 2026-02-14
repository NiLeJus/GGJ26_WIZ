using System;
using UnityEngine;

public abstract class PerkCondition
{    // A mettre dans model folder
    [SerializeField]protected ReactionTiming reactionTiming;
    

    public abstract void SubscribeCondition(Action<GameAction> reaction);
  
    public abstract void UnsubscribeCondition(Action<GameAction> reaction);

    public abstract bool SubConditionIsMet(GameAction gameAction);
}
