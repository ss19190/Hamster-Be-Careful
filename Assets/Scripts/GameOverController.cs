using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameOverController : MonoBehaviour
{
    public Text distanceText;
    public Text timeText;
    public Text endText;
    public Text buttonText;
    private float distanceInt;
    private float timeInt;
    private string distanceString;
    private string timeString;
    public bool isLevelMode;
    public int levelModeLevel;
    private bool isGoalReached;
    public Button restartButton;

    void Start()
    {
        PlayerPrefs.SetInt("Total Deaths", PlayerPrefs.GetInt("Total Deaths", 0) + 1);
        isLevelMode = PlayerPrefs.GetInt("isLevelMode", 0) == 1;
        distanceInt = PlayerPrefs.GetInt("Final Distance");
        PlayerPrefs.SetFloat("Total Distance", PlayerPrefs.GetFloat("Total Distance", 0) + distanceInt);
        distanceString = distanceInt.ToString("F2");
        timeInt = PlayerPrefs.GetFloat("Final Time");
        PlayerPrefs.SetFloat("Total Time", PlayerPrefs.GetFloat("Total Time", 0) + timeInt);
        timeString = timeInt.ToString("F2");
        levelModeLevel = PlayerPrefs.GetInt("LevelModeLevel", 1);
        isGoalReached = PlayerPrefs.GetInt("GoalReached", 0) == 1;

        distanceText.text = "Distance: " + distanceString;
        timeText.text = "Time: " + timeString;

        if (isLevelMode)
        {
            if (isGoalReached)
            {
                endText.text = "Level " + levelModeLevel + " Completed!";
                if (levelModeLevel == 5)
                    restartButton.gameObject.SetActive(false);
                buttonText.text = "Next Level";
                PlayerPrefs.SetInt("LevelModeLevel", levelModeLevel + 1);
            }
            else
            {
                endText.text = "Level " + levelModeLevel + " Failed!";
                buttonText.text = "Retry Level";
            }
        }
        else
            SaveHighScore();
    }

    public void SaveHighScore()
    {
        string username = PlayerPrefs.GetString("Username");
        string path = Application.persistentDataPath + "/highscores.txt";

        string entry = $"{username}:{distanceString}:{timeString};";
        File.AppendAllText(path, entry);
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}

