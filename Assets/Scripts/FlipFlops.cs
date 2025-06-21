using System.Collections;
using UnityEngine;

public class FlipFlops : Powerups
{
    public override void OnPlayerHit(GameController controller)
    {

        controller.newSpeed += 2f;

        foreach (ObjectMover obstacle in Object.FindObjectsOfType<ObjectMover>())
            obstacle.speed = controller.newSpeed;
    }
    
}
