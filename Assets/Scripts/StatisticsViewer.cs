using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatisticsViewer : MonoBehaviour
{
    public Text distanceText; // Shows: username - distance
    public Text timeText;     // Shows: username - time
    public Text deathsText;


    void Start()
    {
        distanceText.text = "Total distance: " + PlayerPrefs.GetFloat("Total Distance", 0).ToString();
        timeText.text = "Total time: " + PlayerPrefs.GetFloat("Total Time", 0).ToString("F2");
        deathsText.text = "Total deaths: " + PlayerPrefs.GetInt("Total Deaths", 0).ToString();
    }

    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

