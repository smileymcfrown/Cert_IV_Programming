using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel, playPanel, optionsPanel;
    private DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        //dataManager.LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        playPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void OptionsMenu()
    {
        optionsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void ExitToDesktop()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
