using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void LoadGame(){
        PlayerPrefs.SetString("CurrentLevel", "Game");

        SceneManager.LoadScene("Game");
    }

    public void LoadHardLeve(){
        PlayerPrefs.SetString("CurrentLevel", "HardLevel");

        SceneManager.LoadScene("HardLevel");
    }
}
