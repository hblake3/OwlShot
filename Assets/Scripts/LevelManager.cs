using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AudioClip levelMusic;
    private void Start()
    {
        SoundManager.Instance.PlayMusic(levelMusic);
    }

    public void LoadGame(){
        PlayerPrefs.SetString("CurrentLevel", "Game");

        SceneManager.LoadScene("Game");
    }

    public void LoadHardLeve(){
        PlayerPrefs.SetString("CurrentLevel", "HardLevel");

        SceneManager.LoadScene("HardLevel");
    }

    // Quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
                // If we are running in the Unity Editor
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If we are running a built game
            Application.Quit();
        #endif
    }
}
