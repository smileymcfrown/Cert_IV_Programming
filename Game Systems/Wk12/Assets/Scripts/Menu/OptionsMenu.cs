using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_Dropdown resDropdown;
    

    private bool firstSet = true;
    private Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        int currentResIndex = 0;
        List<String> options = new List<string>();
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
       
        resDropdown.AddOptions(options);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();
        
        //if(hasSettings)
        //{
        //LoadOptions();
        //}
        //else
        //{
        SetMusicVol();
        SetSfxVol();
        //}
        firstSet = false;
    }

    private void Update()
    {
        
    }

    public void LoadOptions()
    {
        //Add code to get value from retrieved file
        
        //musicSlider.value = some value from an array or something
        //sfxSlider.value = some value from an array or something
        //GfxLevel(  );
        //FullScreen(  );
        SetMusicVol();
        SetSfxVol();
    }
    
    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("Full: " + isFullScreen);
    }

    public void ScreenRes()
    {
        
    }

    public void GfxLevel(int level)
    {
        QualitySettings.SetQualityLevel(level);
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void SetMusicVol()
    {
        float vol = musicSlider.value;
        mixer.SetFloat("music", Mathf.Log10(vol) * 20);
        //Set to array or something here
    }

    public void SetSfxVol()
    {
        float vol = sfxSlider.value;
        mixer.SetFloat("sfx", Mathf.Log10(vol) * 20);
        if (!firstSet)
        {
            sfxSource.Play();
        }
        //Set to array or something here
    }
}
