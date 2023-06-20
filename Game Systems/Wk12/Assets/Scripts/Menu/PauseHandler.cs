using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{

    public static bool isPaused;
    public GameObject pauseMenu, optionsMenu;
    public CharacterController controller;

    public void Paused()
    {
        GameManager.Instance.gameState = GameState.Pause;
        isPaused = true;
        Time.timeScale = 0;
        Debug.Log(Time.timeScale);
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void UnPaused()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.Instance.gameState = GameState.Alive;
    }

    private void Start()
    {
        UnPaused();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    Paused();
                }
                else
                {
                    UnPaused();
                }
            }
        }
    }
}
