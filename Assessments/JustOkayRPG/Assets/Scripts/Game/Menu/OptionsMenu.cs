using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Toggle screenToggle;
    [SerializeField] private TMP_Dropdown resDropdown;
    [SerializeField] private TMP_Dropdown gfxDropdown;
    
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    //[SerializeField] private GameObject mainMenu;

    private bool firstSet = true;
    private Resolution[] resolutions;
    
    


    void Start()
    {
        
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        int currentResIndex = 0;
        List<String> options = new List<string>();

        //Resolution tempRes = new Resolution();
           
        for (int i = 0; i < resolutions.Length; i++)
        {
           // if (tempRes.width != resolutions[i].width && tempRes.height != resolutions[i].height)
            //{
            Debug.Log("Resolutions" + i + " : " + resolutions[i].height + " = " + (resolutions[i].width / (16f / 9f)) + " = "+resolutions[i].width);
                if (resolutions[i].height == Mathf.RoundToInt(resolutions[i].width / (16f / 9f)))
                {
                    string option = resolutions[i].refreshRate + "Hz - " + resolutions[i].width + "x" + resolutions[i].height;
                    //if (!options.Contains(option))
                    //{
                        options.Add(option);
                        if (resolutions[i].width == Screen.currentResolution.width &&
                            resolutions[i].height == Screen.currentResolution.height)
                        {
                            currentResIndex = i;
                            Debug.Log("Resolution Index: " + currentResIndex + "\nWidth: " + resolutions[i].width +
                                      " Height: " + resolutions[i].height);
                        }
                    //}
                }
            //}
            //tempRes = resolutions[i];
        }
       
        resDropdown.AddOptions(options);
        resDropdown.SetValueWithoutNotify(currentResIndex);
        resDropdown.RefreshShownValue();


        gfxDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
        gfxDropdown.RefreshShownValue();

        if (QualitySettings.GetQualityLevel() != SettingsData.settingsData.gfxQuality)
        {
            Debug.Log("GFX Quality Level does not match saved settings.\n Settings Data did not load from AnyKey.cs or does not match for some reason");
        }
        
        
        mixer.GetFloat("music", out float mixerVal);
        if (Mathf.Approximately(Mathf.Log10(SettingsData.settingsData.musicVol) * 20, mixerVal))
        {
            musicSlider.SetValueWithoutNotify(SettingsData.settingsData.musicVol);
        }
        else
        {
            Debug.Log("Music volume settings don't match. settingsData.musicVol = " + SettingsData.settingsData.musicVol + " mixerVal = " + mixerVal);
            musicSlider.SetValueWithoutNotify(SettingsData.settingsData.musicVol);
        }
        
        mixer.GetFloat("sfx", out mixerVal);
        if (Mathf.Approximately(Mathf.Log10(SettingsData.settingsData.sfxVol) * 20, mixerVal))
        {
            sfxSlider.SetValueWithoutNotify(SettingsData.settingsData.sfxVol);
        }
        else
        {
            Debug.Log("SFX volume settings don't match. settingsData.sfxVol = " + SettingsData.settingsData.sfxVol + " mixerVal = " + mixerVal);
            sfxSlider.SetValueWithoutNotify(SettingsData.settingsData.sfxVol);
        }
        firstSet = false;
        
        screenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
        if (SettingsData.settingsData.isFullScreen != Screen.fullScreen)
        {
            Debug.Log("Full Screen does not match saved settings.\n Settings Data did not load from AnyKey.cs or does not match for some reason");
        }


    }

    private void Update()
    {
        
    }
    
    
    public void FullScreen(bool isFullScreen)
    {
        /*if (firstSet)
        {
            Screen.fullScreen = isFullScreen;
            screenToggle.isOn = isFullScreen;
        }
        else
        {*/
            SettingsData.settingsData.isFullScreen = Screen.fullScreen = isFullScreen;
        //}

        Debug.Log("Full: " + isFullScreen);
    }

    public void ScreenRes(int resIndex)
    {
        Debug.Log("resIndex: " + resIndex);
        /*if (resIndex == -1)
        {
            bool resFound = false;
            for(int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == SettingsData.settingsData.screenWidth &&
                    resolutions[i].height == SettingsData.settingsData.screenHeight)
                {
                    resDropdown.value = i;
                    Screen.SetResolution(SettingsData.settingsData.screenWidth, SettingsData.settingsData.screenHeight,
                        Screen.fullScreen);
                    resFound = true;
                }
            }

            if (!resFound)
            {
                Debug.Log("Saved Resolution not found!\n   Updating saved settings to current resolution.");
                SettingsData.settingsData.screenWidth = Screen.currentResolution.width;
                SettingsData.settingsData.screenHeight = Screen.currentResolution.height;
            }
            Screen.SetResolution(SettingsData.settingsData.screenWidth, SettingsData.settingsData.screenHeight,Screen.fullScreen);
        }
        else
        {*/
            Resolution res = resolutions[resIndex];
            SettingsData.settingsData.screenWidth = res.width;
            SettingsData.settingsData.screenHeight = res.height;
            Screen.SetResolution(res.width, res.height,Screen.fullScreen);
            
        //}
        
    }

    public void GfxLevel(int level)
    {
        /*if (firstSet)
        {
            QualitySettings.SetQualityLevel(level);
        }
        else
        {*/
            SettingsData.settingsData.gfxQuality = level;
            QualitySettings.SetQualityLevel(level);
        //}
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void SetMusicVol()
    {
        /*if (firstSet)
        {
            musicSlider.value = SettingsData.settingsData.musicVol;
            mixer.SetFloat("music", Mathf.Log10(SettingsData.settingsData.musicVol) * 20);
        }
        else
        {*/
            SettingsData.settingsData.musicVol = musicSlider.value;
            mixer.SetFloat("music", Mathf.Log10(SettingsData.settingsData.musicVol) * 20);
        //}
    }

    public void SetSfxVol()
    {
        /*if (firstSet)
        {
            mixer.SetFloat("sfx", Mathf.Log10(SettingsData.settingsData.sfxVol) * 20);
            sfxSlider.value = SettingsData.settingsData.sfxVol;
        }
        else
        {*/
            
            SettingsData.settingsData.sfxVol = sfxSlider.value;
            mixer.SetFloat("sfx", Mathf.Log10(SettingsData.settingsData.sfxVol) * 20);
            if (!firstSet)
            {
                sfxSource.Play();
            }
            //}
    }
}
