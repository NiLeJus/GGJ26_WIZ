using UnityEngine;

public class GAPerformEffect : GameAction
{
   public Effect Effect { get; set; }

   public GAPerformEffect(Effect effect)
   {
      Effect =  effect;  
   }
}

