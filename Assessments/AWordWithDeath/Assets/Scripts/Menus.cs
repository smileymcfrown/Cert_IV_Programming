using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel, quitPanel, difficultyPanel, gamePanel, wordPanel, losePanel, winPanel, censoredPanel, keyPanel, blade;
    private GameManager gameManager;
    [SerializeField] LoadWords loadWords;
    private bool quitActive = false;

    public void Start()
    {
        GameManager.Instance.bladeStartPos = blade.transform.position;
    }

    public void MainMenu()
    {
        Reset();
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

    public void DifficultySelect(string dictName)
    {

        
        //Reset rounds 
        GameManager.Instance.round = 1;
        GameManager.Instance.score = 0;

        //Load Dictionary
        loadWords.LoadFile(dictName);

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
        
        Reset();
        
        wordPanel.SetActive(false);
        wordPanel.SetActive(true);
        winPanel.SetActive(false);
    }

    // Restart the game without going to Main Menu
    public void NewGame()
    {
        Reset();
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        wordPanel.SetActive(false);
        gamePanel.SetActive(false);

        difficultyPanel.SetActive(true);
    }

    // Reset everything when a new round or game is started 
    private void Reset()
    {
        blade.transform.position = GameManager.Instance.bladeStartPos;

        censoredPanel.SetActive(false);
        
        for (int i = keyPanel.transform.childCount - 1; i >= 0; i--)
        {
            Button button = keyPanel.transform.GetChild(i).GetComponent<Button>();
            button.interactable = true;
            //ColorBlock colours = button.colors;
            //colours.normalColor = Color.white;
            //button.colors = colours;
            button.GetComponentInChildren<TMP_Text>().color = new Color32(81,84,255,255);
        }

        for (int i = wordPanel.transform.childCount -1; i >= 0; i--)
        {
            Destroy(wordPanel.transform.GetChild(i).gameObject);
        }
    }
}
