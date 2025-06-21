using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelModeController : MonoBehaviour
{
    public Button[] levelButtons;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button button in levelButtons)
        {
            int levelIndex = System.Array.IndexOf(levelButtons, button) + 1; // Level index starts from 1
            button.onClick.AddListener(() => LoadLevel(levelIndex));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("isLevelMode", 1);
        PlayerPrefs.SetInt("LevelModeLevel", levelIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
