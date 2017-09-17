using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public bool isPaused;
    public string currScene;
    public string nextScene;

    public GameObject pauseMenu;
    public GameObject winMenu;

    private void Awake()
    {
        isPaused = false;

        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
        }

        if (Input.GetButtonDown("Restart"))
        {
            Restart();
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currScene);
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Win()
    {
        Time.timeScale = 0;
        winMenu.SetActive(true);
    }
}