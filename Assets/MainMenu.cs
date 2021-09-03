using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        PauseGame();
        gameObject.SetActive(true);
    }

    public void StartButton()
    {
        gameObject.SetActive(false);
        ResumeGame();
    }
    
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
    
    private void PauseGame ()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame ()
    {
        Time.timeScale = 1;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
