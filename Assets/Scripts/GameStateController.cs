// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;


// // This class maintains paused / active states and scene management (i.e. restarting scene for replays).

// public class GameStateController : MonoBehaviour
// {
//     private bool gamePaused = false;
//     [SerializeField] GameObject gameOverScreen;
//     [SerializeField] GameObject startButton;
//     private bool isFirstGameStart = true; // Track if the game has started

//     // Set the play state 
//     public void SetPausedState(bool _state){
//         gamePaused = _state;
//         ChangeGameState();
//     }

//     // Return the play state
//     public bool GetPlayState(){
//         return gamePaused;
//     }

//     // Change the game state to paused or active
//     private void ChangeGameState(){
//         if(gamePaused){
//             Time.timeScale = 0;
//             //DisplayGameOver();
//         }

//         else
//             Time.timeScale = 1;
//     }

//     public void DisplayGameOver(){
//         gameOverScreen.SetActive(true);

//         SetPausedState(true); // Pause the game when the game is over
    


//     }

//     // Reset the scene to play again
//     public void ResetGame(){

//         SceneManager.LoadScene("Game");
//         // Reload the current scene to restart the game
//         SetPausedState(true);

//         startButton.SetActive(false);    // Keep the start button hidden after reset

//     }

//     //start the game
//     public void StartGame(){
//        if (isFirstGameStart)
//         {
//             startButton.SetActive(false);    // Hide the start button
//             SetPausedState(false);           // Unpause the game to start
//             isFirstGameStart = false;        // Mark that the game has started
//         }

//     }
//       private void Start()
//     {
//         // Ensure the start button is visible only once when the game starts
//         // Initially, the start button should be visible and Play Again should be hidden
//         if (isFirstGameStart)
//         {
//             startButton.SetActive(true); // Show the start button at the beginning
//         }

//         gameOverScreen.SetActive(false); // Ensure Game Over screen is hidden initially
//        // playAgainButton.SetActive(false); // Ensure Play Again button is hidden initially
//         SetPausedState(true); 
//     }



// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject startButton;

    private static bool isFirstGameStart = true; // Tracks if the game has started

    private void Awake()
    {
        gameOverScreen.SetActive(false);

        if (isFirstGameStart)
        {
            startButton.SetActive(true);
            Time.timeScale = 0; // Pause the game at the start
        }
        else
        {
            startButton.SetActive(false);
            Time.timeScale = 1; // Ensure the game starts immediately after Play Again
        }
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        Time.timeScale = 1; // Start the game
        isFirstGameStart = false; // Ensure Start Button doesn't show again
    }

    public void DisplayGameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0; // Pause the game when over
    }

    public void ResetGame()
    {
        isFirstGameStart = false; // Ensure Start Button never shows again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1; // Resume game after restart
    }
}


