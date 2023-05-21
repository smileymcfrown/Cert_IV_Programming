using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Menus : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel, quitPanel, difficultyPanel, gamePanel;
    public GameManager gameManager;
    private bool quitActive = false;
    
    public void DifficultyMenu()
    {
        gamePanel.SetActive(true);
        difficultyPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void DifficultySelect(int choice)
    {
        gameManager.difficulty = choice;
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
    
    
}
