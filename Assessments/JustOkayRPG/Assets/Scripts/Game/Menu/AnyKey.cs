using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AnyKey : MonoBehaviour
{
    [SerializeField] private GameObject anyKeyPnl, mainPnl;//,optionsPnl;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject playBtn;
    [SerializeField] private GameObject newGameBtn;
    [SerializeField] private KeyBinder keyBinder;
    //public Sprite newGameImg;
    
    private Resolution[] resolutions;
    private static bool firstStart = true;
    
    void Start()
    {
        // Only load the game data and settings when the game is first started;
        // Otherwise, all game and setting data is loaded/saved through button and menu interactions
        if (firstStart)
        {
            DataManager.instance.LoadSettings();
            DataManager.instance.LoadGame();

            // Moved checking for no load data to here from DataManager() because it's easier to then adjust the menu
            // from "New Game" to "Continue" if there a save file is found.

            // Also moved saved settings check here for consistency.
            // This is not where these should go and these scripts should really refactored to better meet SOLID principles

            if (GameData.gameData == null)
            {
                Debug.Log("No game data to load. Creating new game data.");
                DataManager.instance.NewGame();

                // If there is not saved game, the "Play" button that opens a second menu with Continue/New Game options
                // will be switched for a "New Game" button that just loads the level. 
                newGameBtn.SetActive(true);
                playBtn.SetActive(false);
            }

            if (SettingsData.settingsData == null)
            {
                Debug.Log("No settings data to load.");
                // Set default values
                SettingsData.settingsData = new SettingsData();
                // Set default keys
                keyBinder.SetDefaultKeys();
            }
            else
            {
                // Load keys from saved file if found (if they were never changed the defaults were saved to the file)
                keyBinder.LoadKeys();
                Debug.Log("Settings *should* be loaded");
            }

            // Setting the various saved graphics and audio settings here
            // so there is not a jump in audio when moving to the options panel. 
            TheSetup();
        }

        firstStart = false;
        
        Debug.Log("Any Key Screen - Player Position");
        
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
        Debug.Log(posRotArray);;

    }
    

    // Update is called once per frame
    void Update()
    {
        // Check for a key press or click to remove the screen and show the main menu
        if (Input.anyKey)
        {
            mainPnl.SetActive(true);
            //optionsPnl.SetActive(false);
            anyKeyPnl.SetActive(false);
        }
    }

    void TheSetup()
    {
        // Bit of error checking shenanigans here to check if a resolution was set
        // - if it was set but isn't the current resolution, cycle through available ones to check compatibility
        // - if it's not set or doesn't match any available resolutions, it defaults to the current resolution
        Debug.Log("Saved Res: " + 
                  SettingsData.settingsData.screenWidth + " x " + SettingsData.settingsData.screenHeight);
        bool resFound = false;
        
        // May not be necessary as all options other than the former else-if do the same thing. 
        /*if (SettingsData.settingsData.screenWidth == 0 || SettingsData.settingsData.screenHeight == 0)
        {
            SettingsData.settingsData.screenWidth = Screen.currentResolution.width;
            SettingsData.settingsData.screenHeight = Screen.currentResolution.height;
            resFound = true;
        }
        else*/ 
        
        if(SettingsData.settingsData.screenWidth != Screen.currentResolution.width ||
                SettingsData.settingsData.screenHeight != Screen.currentResolution.height)
        {
            resolutions = Screen.resolutions;
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == SettingsData.settingsData.screenWidth &&
                    resolutions[i].height == SettingsData.settingsData.screenHeight)
                {
                    Screen.SetResolution(SettingsData.settingsData.screenWidth,
                        SettingsData.settingsData.screenHeight, Screen.fullScreen);
                    resFound = true;
                }
            }
        }
        
        if (!resFound)
        {
            SettingsData.settingsData.screenWidth = Screen.currentResolution.width;
            SettingsData.settingsData.screenHeight = Screen.currentResolution.height;
            Debug.Log("Saved Resolution not found!"+ 
                      "\n                    Updating saved settings to current resolution.");
            Debug.Log("WxH: " + SettingsData.settingsData.screenWidth +
                      " x " + SettingsData.settingsData.screenHeight);
        }
        
        //Set the rest of the stuff
        Screen.fullScreen = SettingsData.settingsData.isFullScreen;
        QualitySettings.SetQualityLevel(SettingsData.settingsData.gfxQuality);
        mixer.SetFloat("music", Mathf.Log10(SettingsData.settingsData.musicVol) * 20);
        mixer.SetFloat("sfx", Mathf.Log10(SettingsData.settingsData.sfxVol) * 20);
    }
}
