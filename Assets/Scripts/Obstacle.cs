using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Obstacle : ObjectBase
{
    protected void DeleteHeart(GameController controller)
    {
        if (PlayerPrefs.GetInt("TNT taken") == 0)
        {
            controller.hearts[controller.heartCount].SetActive(false);
            controller.heartCount--;

            bool isLevelMode = PlayerPrefs.GetInt("isLevelMode", 0) == 1;

            if (isLevelMode)
            {
                if (controller.heartCount < 0)
                {
                    PlayerPrefs.SetInt("Final Distance", (int)controller.distance);
                    PlayerPrefs.SetFloat("Final Time", (int)controller.timer);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("GameOverScene");
                }
            }
            else
            {
                SceneManager.LoadScene("LevelFinishedScene");  
            }

        }
        else
        {
            PlayerPrefs.SetInt("TNT taken", 0);
            controller.tnt.SetActive(false);
        }
   }
}
