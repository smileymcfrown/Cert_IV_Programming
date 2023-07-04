using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{

    public static bool isPaused;
    public GameObject pauseMenu, optionsMenu;
    public Transform player;
    public CharacterController controller;
    [SerializeField] private KeyBinder keyBinder;
    public void Paused()
    {
        //Reload the key bindings so that the Keybinding Menu is filled out correctly.
        keyBinder.SetKeyText();
        
        // Pause game, lock movement, show mouse 
        GameManager.Instance.gameState = GameState.Pause;
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Save current player position and rotation
        GameData.gameData.playerPosition[0] = player.position.x;
        GameData.gameData.playerPosition[1] = player.position.y;
        GameData.gameData.playerPosition[2] = player.position.z;
        GameData.gameData.playerRotation[0] = player.rotation.eulerAngles.x;
        GameData.gameData.playerRotation[1] = player.rotation.eulerAngles.y;
        GameData.gameData.playerRotation[2] = player.rotation.eulerAngles.z;

        // Debug to show transform pos. and rot. and saved pos. and rot. in an easy to read way 
        #region Show Debug of Player Position Cleanly
        Debug.Log("Pause Menu - Player Position");
        Debug.Log("RealPos: " + player.position + " RealRot: " + player.eulerAngles);
            
        string posRotArray = "SavePos: (";
        for (int x = 0; x < GameData.gameData.playerPosition.Length; ++x)
        {
            posRotArray += GameData.gameData.playerPosition[x];
            if(x < GameData.gameData.playerPosition.Length -1){posRotArray += ", ";}
        }
        posRotArray += ")  Rotation: (";
        for (int x = 0; x < GameData.gameData.playerRotation.Length; ++x)
        {
            posRotArray += GameData.gameData.playerRotation[x];
            if(x < GameData.gameData.playerRotation.Length -1){posRotArray += ", ";}
        }
        posRotArray += ")";
        Debug.Log(posRotArray);
        #endregion
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
    
    public void ExitToDesktop()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
