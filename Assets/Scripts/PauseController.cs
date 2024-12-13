using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public static bool GameQuit = false;
    
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject pauseButton; 
    
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        GameQuit = true;
        Application.Quit();
        SceneManager.LoadScene(SceneData.homepage);
    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
}
