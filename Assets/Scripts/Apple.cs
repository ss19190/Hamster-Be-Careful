using System.Collections;
using UnityEngine;

public class Apple : Powerups
{
    public Apple()
    {
        powerupType = 0;
    }

    public override void OnPlayerHit(GameController controller)
    {

        AddHeart(controller);
        Debug.Log("Apple collected! Hearts: " + controller.heartCount);

    }
    
    
}
