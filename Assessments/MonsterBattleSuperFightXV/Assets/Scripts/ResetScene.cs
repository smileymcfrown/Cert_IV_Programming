using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetScene : MonoBehaviour
{
    public GameObject menuPanel;
    private void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResetSceneNow()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        
        
    }
}
