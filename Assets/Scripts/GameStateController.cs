using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// This class maintains paused / active states and scene management (i.e. restarting scene for replays).

public class GameStateController : MonoBehaviour
{
    private bool gamePaused = false;
    [SerializeField] GameObject gameOverScreen;

    // Set the play state 
    public void SetPausedState(bool _state){
        gamePaused = _state;
        ChangeGameState();
    }

    // Return the play state
    public bool GetPlayState(){
        return gamePaused;
    }

    // Change the game state to paused or active
    private void ChangeGameState(){
        if(gamePaused){
            Time.timeScale = 0;
            DisplayGameOver();
        }

        else
            Time.timeScale = 1;
    }

    private void DisplayGameOver(){
        gameOverScreen.SetActive(true);

    }

    // Reset the scene to play again
    public void ResetGame(){
        SceneManager.LoadScene("Game");
        SetPausedState(false);
    }

}
