using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreViewer : MonoBehaviour
{
    public Text distanceText; // Shows: username - distance
    public Text timeText;     // Shows: username - time

    private List<HighscoreEntry> entriesForDistance = new();
    private List<HighscoreEntry> entriesForTime = new();

    void Start()
    {
        LoadHighscores();
        DisplayHighscores();
    }

    void LoadHighscores()
    {
        string path = Application.persistentDataPath + "/highscores.txt";
        if (!File.Exists(path)) return;

        string[] rawEntries = File.ReadAllText(path).Split(';');
        entriesForDistance.Clear();
        entriesForTime.Clear();

        foreach (string entry in rawEntries)
        {
            if (string.IsNullOrWhiteSpace(entry)) continue;

            string[] parts = entry.Split(':');
            if (parts.Length == 3 &&
                float.TryParse(parts[1], out float distance) &&
                float.TryParse(parts[2], out float time))
            {
                string username = parts[0];
                entriesForDistance.Add(new HighscoreEntry(username, distance, time));
                entriesForDistance = entriesForDistance.OrderByDescending(e => e.distance).ToList(); // sort by distance
                entriesForTime.Add(new HighscoreEntry(username, distance, time));
                entriesForTime = entriesForTime.OrderByDescending(e => e.time).ToList(); // sort by time
            }
        }
    }

    void DisplayHighscores()
    {
        distanceText.text = "DISTANCE\n------------\n";
        timeText.text = "TIME\n------------\n";

        for (int i = 0; i < entriesForDistance.Count; i++)
        {
            var e = entriesForDistance[i];
            distanceText.text += $"{i + 1}. {e.username} - {e.distance:F2}\n";
        }
        for (int i = 0; i < entriesForTime.Count; i++)
        {
            var e = entriesForTime[i];
            timeText.text += $"{i + 1}. {e.username} - {e.time:F2}\n";
        }
    }
    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

