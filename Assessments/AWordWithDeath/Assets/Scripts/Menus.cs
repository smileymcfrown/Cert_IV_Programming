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
    // private PlayWord playWord;
    private bool quitActive = false;
    
    public void DifficultyMenu()
    {
        difficultyPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void DifficultySelect(string choice)
    {
        loadWords.LoadFile(choice);
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
        Application.Quit();
    }

    
    
    // To continue playing and load next word
    public void NextWord()
    {
        wordPanel.SetActive(false);
        wordPanel.SetActive(true);
        winPanel.SetActive(false);
    }
}
