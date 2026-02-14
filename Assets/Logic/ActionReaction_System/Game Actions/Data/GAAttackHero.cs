using UnityEngine;

public class GAAttackHero : GameAction, IHaveCaster
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public EnemyView Attacker { get; private set; }
    public CombatantView Caster { get; private set; }

    public GAAttackHero(EnemyView attacker)
    {
           Attacker = attacker;
           Caster = Attacker;
    }
 
    
}
