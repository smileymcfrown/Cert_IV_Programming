using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel, quitPanel, difficultyPanel, gamePanel, wordPanel, losePanel, winPanel;
    private GameManager gameManager;
    [SerializeField] LoadWords loadWords;
    private bool quitActive = false;

    public void MainMenu()
    {
        mainMenuPanel.SetActive(true);
        quitPanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        gamePanel.SetActive(false);
    }
    
    public void DifficultyMenu()
    {
        difficultyPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void DifficultySelect(string choice)
    {
        //Reset rounds 
        GameManager.Instance.round = 1;
        GameManager.Instance.score = 0;

        //Load Dictionary
        loadWords.LoadFile(choice);

        // Enable and Disable Panels
        gamePanel.SetActive(true);
        wordPanel.SetActive(true);
        difficultyPanel.SetActive(false);
    }
    
    public void QuitMenu()
    {
        if (!quitActive)
        {
            quitPanel.SetActive(true);
            quitActive = true;
        }
        else
        {
            quitPanel.SetActive(false);
            quitActive = false;
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    
    // To continue playing and load next word
    public void NextWord()
    {
        GameManager.Instance.round++;
        wordPanel.SetActive(false);
        wordPanel.SetActive(true);
        winPanel.SetActive(false);
    }

    // Restart the game without going to Main Menu
    public void NewGame()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        wordPanel.SetActive(false);
        gamePanel.SetActive(false);

        difficultyPanel.SetActive(true);
    }
}
