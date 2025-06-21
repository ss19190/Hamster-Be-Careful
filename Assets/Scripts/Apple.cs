using System.Collections;
using UnityEngine;

public class Apple : Powerups
{
    public override void OnPlayerHit(GameController controller)
    {

        AddHeart(controller);
        Debug.Log("Apple collected! Hearts: " + controller.heartCount);

    }
    
    
}
