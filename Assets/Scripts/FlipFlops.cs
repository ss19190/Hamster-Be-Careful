using System.Collections;
using UnityEngine;

public class FlipFlops : Powerups
{
    public FlipFlops()
    {
        powerupType = 2;
    }
    public override void OnPlayerHit(GameController controller)
    {

        controller.newSpeed += 2f;

        foreach (ObjectMover obstacle in Object.FindObjectsOfType<ObjectMover>())
            obstacle.speed = controller.newSpeed;
    }
    
}
