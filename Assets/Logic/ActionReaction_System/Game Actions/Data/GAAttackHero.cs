using UnityEngine;

public class GAAttackHero : GameAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public EnemyView Attacker { get; private set; }

    public GAAttackHero(EnemyView attacker)
    {
           Attacker = attacker;
    }
 
    
}
