using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text scoreText;
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;

    private void Start()
    {
        //playerScore = null;
        restartButton.onClick.AddListener(RestartGame); //add listener for Restart button
        mainMenuButton.onClick.AddListener(LoadMainMenu); //add listener for Main Menu button

        int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);

        // Update the score text
        if (scoreText != null)
        {
            scoreText.text = "Your Score: " + playerScore + " PTS";
        }

    }

    public void DisplayGameOver()
    {

        gameOverScreen.SetActive(true); //show the Game Over screen
        Time.timeScale = 0f; //pause the game
    }

public void RestartGame()
{
    Time.timeScale = 1f; //resume game speed

    string currentLevel = PlayerPrefs.GetString("CurrentLevel", "Game"); //default to "Game" if no level is saved
    Debug.Log("Reloading scene: " + currentLevel);

    SceneManager.LoadScene(currentLevel); //load the gameplay scene (Game or HardLevel)
}



    // Load the main menu scene
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu"); 
    }

}
