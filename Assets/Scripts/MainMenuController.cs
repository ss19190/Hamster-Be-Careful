using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public InputField usernameInput;
    public GameObject errorPopupPanel;
    public Text errorMessageText;

    private readonly string usernamePattern = @"^[a-zA-Z0-9_]{3,16}$";

    void Start()
    {
        errorPopupPanel.SetActive(false); // Hide popup on start
    }

    public void InfiniteButtonClicked()
    {
        string username = usernameInput.text.Trim();

        if (!Regex.IsMatch(username, usernamePattern))
        {
            ShowError("Please enter a valid username (3–16 letters, numbers, or _)");
            return;
        }

        PlayerPrefs.SetInt("isLevelMode", 0);
        SceneManager.LoadScene("GameScene");
        PlayerPrefs.SetString("Username", username); // Save username for later use
    }

    public void HighScoreButtonClicked()
    {
        SceneManager.LoadScene("HighscoreScene");
    }

    public void ShowError(string message)
    {
        errorMessageText.text = message;
        errorPopupPanel.SetActive(true);
    }

    public void CloseErrorPopup()
    {
        errorPopupPanel.SetActive(false);
    }
}

