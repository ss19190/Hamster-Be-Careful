using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Obstacle : ObjectBase
{
    public int obstaclesTaken;
    protected void DeleteHeart(GameController controller)
    {
        obstaclesTaken = PlayerPrefs.GetInt("Obstacles taken");
        obstaclesTaken++;
        PlayerPrefs.SetInt("Obstacles taken", obstaclesTaken);
        Debug.Log("Obstacles taken: " + obstaclesTaken);

        if (PlayerPrefs.GetInt("TNT taken") == 0)
        {
            controller.hearts[controller.heartCount].SetActive(false);
            controller.heartCount--;

            bool isLevelMode = PlayerPrefs.GetInt("isLevelMode", 0) == 1;

            if (controller.heartCount < 0)
            {
                PlayerPrefs.SetInt("Final Distance", (int)controller.distance);
                PlayerPrefs.SetFloat("Final Time", (float)controller.timer);
                if (isLevelMode)
                {
                    PlayerPrefs.SetInt("GoalReached", 0);
                }
                PlayerPrefs.Save();
                SceneManager.LoadScene("GameOverScene");
            }
            

        }
        else
        {
            PlayerPrefs.SetInt("TNT taken", 0);
            controller.tnt.SetActive(false);
            controller.tntTaken = false;
        }
   }
}
