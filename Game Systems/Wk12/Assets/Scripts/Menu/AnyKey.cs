using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AnyKey : MonoBehaviour
{
    [SerializeField] private GameObject anyKeyPnl, mainPnl;//,optionsPnl;
    [SerializeField] private AudioMixer mixer;
    private Resolution[] resolutions;
    void Start()
    {
        
        DataManager.instance.LoadSettings();
        if (SettingsData.settingsData.screenWidth != 0 && SettingsData.settingsData.screenHeight != 0)
        {
            Debug.Log("Settings loaded?!?! Probably");
        }
            
        TheSetup();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            mainPnl.SetActive(true);
            //optionsPnl.SetActive(false);
            anyKeyPnl.SetActive(false);
        }
    }

    void TheSetup()
    {
        bool resFound = false;
        if (SettingsData.settingsData.screenWidth == 0 || SettingsData.settingsData.screenHeight == 0)
        {
            SettingsData.settingsData.screenWidth = Screen.currentResolution.width;
            SettingsData.settingsData.screenHeight = Screen.currentResolution.height;
            resFound = true;
        }
        else if(SettingsData.settingsData.screenWidth != Screen.currentResolution.width ||
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
            Debug.Log("Saved Resolution not found!\n   Updating saved settings to current resolution.");
            SettingsData.settingsData.screenWidth = Screen.currentResolution.width;
            SettingsData.settingsData.screenHeight = Screen.currentResolution.height;
        }

        Screen.fullScreen = SettingsData.settingsData.isFullScreen;
        QualitySettings.SetQualityLevel(SettingsData.settingsData.gfxQuality);
        mixer.SetFloat("music", Mathf.Log10(SettingsData.settingsData.musicVol) * 20);
        mixer.SetFloat("sfx", Mathf.Log10(SettingsData.settingsData.sfxVol) * 20);
    }
}
