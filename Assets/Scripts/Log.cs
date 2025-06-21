using System.Collections;
using UnityEngine;

public class Log : Obstacle
{
    public override void OnPlayerHit(GameController controller)
    {
        if(PlayerPrefs.GetInt("Ball taken") == 0)
            DeleteHeart(controller);
    }
}
