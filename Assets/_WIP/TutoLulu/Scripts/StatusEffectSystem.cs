using System;
using System.Collections;
using UnityEngine;

public class StatusEffectSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddStatusEffectGA>(AddStatusEffectPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddStatusEffectGA>();
    }

    private IEnumerator AddStatusEffectPerformer(AddStatusEffectGA addStatusEffectGa)
    {
        foreach (var target in addStatusEffectGa.Targets)
        {
            target.AddStatusEffect(addStatusEffectGa.StatusEffectType, addStatusEffectGa.StackCount);
            yield return  null;//Si on veux ajouter des VFX c'est ici
        }
    }
}
