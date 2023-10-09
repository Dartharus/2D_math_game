using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelectController : MonoBehaviour
{
    private int easyStartLevel = 1;
    private int mediumStartLevel = 6;
    private int level;

    public void onButtonEasy() //function for the easy difficulty button
    {
        level = easyStartLevel;
        PlayerPrefs.SetInt("GameLevel", level);
        SceneManager.LoadScene("TestScene"); //load level 1
    }

    public void onButtonMedium() //function for the medium difficulty
    {
        level = mediumStartLevel;
        PlayerPrefs.SetInt("GameLevel", level);
        SceneManager.LoadScene("Level6"); //load level 6
    }

}
