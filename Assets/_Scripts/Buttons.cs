using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button StartButton;
    public Button QuitButton;
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
