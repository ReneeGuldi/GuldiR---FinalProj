using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartManager : MonoBehaviour
{
    public void InitializeStartMenu()
    {
        Debug.Log("StartMenu Initialization Complete");
    }
}
public class InGameMenuButtons : MonoBehaviour
{
    public Button settingsButton;
    public Button mainMenuButton;

    private StartManager startMenuManager;

    private void Start()
    {
        startMenuManager = FindObjectOfType<StartManager>();
    }
    public void OnSettingsButtonClick()
    {

        Debug.Log("Settings button clicked!");
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("Return to Main Menu Button clicked");
        LoadStartMenu();
    }

    private void LoadStartMenu()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("StartMenu");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (scene.name == "StartMenu" && startMenuManager != null)
        {
            startMenuManager.InitializeStartMenu();
        }
    }
}
