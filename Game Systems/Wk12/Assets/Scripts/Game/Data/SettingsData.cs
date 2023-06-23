using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public static SettingsData settingsData;
    public float musicVol; 
    public float sfxVol;
    public int gfxQuality;
    public bool isFullScreen;
    public int screenWidth;
    public int screenHeight;

    public SettingsData()
    {
        this.musicVol = .4f;
        this.sfxVol = .6f;
        this.gfxQuality = 2;
        this.isFullScreen = false;
        this.screenWidth = 0;
        this.screenHeight = 0;
    }
}
