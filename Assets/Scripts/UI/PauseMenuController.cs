using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Start()
    {
        EventManager.Instance.OnPauseGame.AddListener(Pause);
    }

    public void Pause()
    {
        if (!pauseMenu.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
