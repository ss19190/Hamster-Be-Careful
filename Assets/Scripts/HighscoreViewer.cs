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
                entriesForTime.Add(new HighscoreEntry(username, distance, time));
            }
        }

        entriesForDistance = entriesForDistance.OrderByDescending(e => e.distance).ToList(); // sort by distance
        entriesForTime = entriesForTime.OrderByDescending(e => e.time).ToList(); // sort by time
    }

    void DisplayHighscores()
    {
        distanceText.text = "DISTANCE\n------------\n";
        timeText.text = "TIME\n------------\n";

        var topDistance = entriesForDistance.GetRange(0, Mathf.Min(5, entriesForDistance.Count));
        var topTime = entriesForTime.GetRange(0, Mathf.Min(5, entriesForTime.Count));

        for (int i = 0; i < topDistance.Count; i++)
        {
            var e = topDistance[i];
            distanceText.text += $"{i + 1}. {e.username} - {e.distance:F2}\n";
        }

        for (int i = 0; i < topTime.Count; i++)
        {
            var e = topTime[i];
            timeText.text += $"{i + 1}. {e.username} - {e.time:F2}\n";
        }
    }


    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


