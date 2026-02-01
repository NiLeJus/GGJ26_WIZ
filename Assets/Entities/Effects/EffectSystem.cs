using System.Collections;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    void OnEnable()
    {

        ActionSystem.AttachPerformer<GAPerformEffect>(PerformEffectPerformer);
    }

    void OnDisable()
    {

        ActionSystem.DetachPerformer<GAPerformEffect>();
    }

// Performers 

        private IEnumerator PerformEffectPerformer(GAPerformEffect gaPerformEffect)
        {
           GameAction effectAction =  gaPerformEffect.Effect.GetGameAction();
            ActionSystem. Instance.AddReaction(effectAction);
            yield return null; 
        }

        
}
