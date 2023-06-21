using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Save Data File Name")]
    [SerializeField] private string saveFileName;
    [SerializeField] private string settingsFileName;
    
    //private GameData gameData;
    //private SettingsData settingsData;
    private FileHandler fileHandler;
    public static DataManager instance { get; private set; }
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("You have two Data Managers in the scene");
        }

        instance = this;
    }

    private void Start()
    {
        
    }

    public void NewGame()
    {
        GameData.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.fileHandler = new FileHandler(Application.persistentDataPath, saveFileName);
        GameData.gameData = fileHandler.LoadGame();
        
        if (GameData.gameData == null)
        {
            Debug.Log("No game data to load. Starting new game.");
            NewGame();
        }
    }

    public void SaveGame()
    {
        this.fileHandler = new FileHandler(Application.persistentDataPath, saveFileName);
        fileHandler.SaveGame(GameData.gameData);
    }

    public void LoadSettings()
    {
        Debug.Log("You're in");
        /*this.fileHandler = new FileHandler(Application.persistentDataPath, settingsFileName);
        SettingsData.settingsData = fileHandler.LoadSettings();
        
        if (SettingsData.settingsData == null)
        {
            Debug.Log("No settings data to load.");
            SettingsData.settingsData = new SettingsData();
        }*/
    }

    public void SaveSettings()
    {
        this.fileHandler = new FileHandler(Application.persistentDataPath, settingsFileName);
        fileHandler.SaveSettings(SettingsData.settingsData);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
