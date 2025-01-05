using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject menuGame;
    [SerializeField] private GameObject endGame;
    [SerializeField] private GameObject winGame;

    public void EnableEndGamePanel(bool w)
    {
        if (w == false)
        {
            endGame.SetActive(true);
        }
        else
        {
            winGame.SetActive(true);
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        menuGame.SetActive(false);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuGame.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}