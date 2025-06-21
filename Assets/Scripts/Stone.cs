using System.Collections;
using UnityEngine;

public class Stone : Obstacle
{
    public override void OnPlayerHit(GameController controller)
    {
        if(PlayerPrefs.GetInt("Ball taken") == 0)
            DeleteHeart(controller);
    }
}
