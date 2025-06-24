using System.Collections;
using UnityEngine;

public class Seeds : Powerups
{
    public Seeds()
    {
        powerupType = 3;
    }
    public override void OnPlayerHit(GameController controller)
    {

        controller.distance += 500f;

    }
    
}
