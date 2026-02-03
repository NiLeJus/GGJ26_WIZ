using UnityEngine;

public class EnemyViewCreator : Singleton<EnemyViewCreator>
{
    //A ajouter au game object enemy view creator dans creators, puis ajouter dans le fiels le game object enemy view,
    //dans enemy system ajouter le script enemyBoardView
    //Dans enemy boardview (le game objectt) on peut ajouter les slot nous que 1 puis ajoute l'enemie au slot
    

    [SerializeField] private EnemyView enemyViewPrefab;

    public EnemyView CreateEnemyView(EnemyData enemyData, Vector3 position, Quaternion rotation)
    {
        EnemyView enemyView = Instantiate(enemyViewPrefab, position, rotation);
        enemyView. Setup(enemyData);
        return enemyView;
    }


}
