using System.Collections.Generic;
using UnityEngine;

public class GameAction : MonoBehaviour
{
    public List<GameAction> PreReactions { get; set; }
    
    public List<GameAction> PerformReactions { get; set; }
    public List<GameAction> PostReactions { get; set; }
    
}
