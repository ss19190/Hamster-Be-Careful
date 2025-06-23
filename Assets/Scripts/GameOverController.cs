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
    private float distanceInt;
    private float timeInt;
    private string distanceString;
    private string timeString;
    public bool isLevelMode;

    void Start()
    {

        isLevelMode = PlayerPrefs.GetInt("isLevelMode", 0) == 1;
        distanceInt = PlayerPrefs.GetInt("Final Distance");
        distanceString = distanceInt.ToString("F2");
        timeInt = PlayerPrefs.GetFloat("Final Time");
        timeString = timeInt.ToString("F2");

        distanceText.text = "Distance: " + distanceString;
        timeText.text = "Time: " + timeString;

        if (isLevelMode)
        {
            levelModeLevel = PlayerPrefs.GetInt("LevelModeLevel", 1);
            endText.text = "Level " + levelModeLevel + " Completed!"; 
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
        SceneManager.LoadScene("SampleScene");
    }

    public void MenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}

