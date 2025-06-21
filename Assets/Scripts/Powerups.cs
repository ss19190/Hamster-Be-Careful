using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Powerups : ObjectBase
{
    protected void AddHeart(GameController controller)
    {
        if (controller.heartCount < 2)
        {
            controller.heartCount++;
            controller.hearts[controller.heartCount].SetActive(true);
        }
        else
        {
            controller.distance += 100f;
        }
    }
}
