using System.Collections;
using UnityEngine;

public class HealSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<GAHeal>(HealPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<GAHeal>();
    }

    private IEnumerator HealPerformer(GAHeal gaHeal)
    {
        foreach (var target in gaHeal.Targets)
        {
            target.Heal(gaHeal.Amount);
        }
        yield return null;
    }
}
