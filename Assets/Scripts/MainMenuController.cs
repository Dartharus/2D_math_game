using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainScreen, aboutScreen;
    private int level = 1;  //set the level indicator to 1 in case the player does not want to play the tutorial

    public void OnButtonPlayGame() //load the difficulty select scene after play button is pressed
    {
        SceneManager.LoadScene("DifficultySelect");
    }

    public void OnButtonTutorial() //load the tutorial after tutorial button is pressed
    {
        level = 0;
        PlayerPrefs.SetInt("GameLevel", level);
        SceneManager.LoadScene("Tutorial");
    }

    public void onButtonAboutGame()
    {
        mainScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }
    public void onButtonBack()
    {
        mainScreen.SetActive(true);
        aboutScreen.SetActive(false);
    }

    public void onButtonQuit()
    {
        Application.Quit();
    }
}
