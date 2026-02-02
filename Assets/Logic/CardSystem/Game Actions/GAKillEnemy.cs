using UnityEngine;

public class GAKillEnemy : GameAction
{
    public EnemyView EnemyView { get; private set; }

    public GAKillEnemy(EnemyView enemyView)
    {
            EnemyView = enemyView;
    }


}
