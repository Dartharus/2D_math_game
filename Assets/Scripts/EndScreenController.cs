using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public void onButtonReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
