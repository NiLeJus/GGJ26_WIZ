using Unity.VisualScripting;
using UnityEngine;

public class UI_EndTurnBtn : MonoBehaviour
{
  public void OnClick()
  {
    Debug.Log("On click end turn btn");
    GAEnemyTurn gaEnemyTurn = new();
    ActionSystem.Instance.Perform(gaEnemyTurn);
  }
}
