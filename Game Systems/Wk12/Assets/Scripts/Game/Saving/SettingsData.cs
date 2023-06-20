using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float musicVol; 
    public float sfxVol;
    public int gfxQuality;
    public bool isFullScreen;
    public int screenWidth;
    public int screenHeight;
    public Transform lastCheckpoint;

    public SettingsData()
    {
        this.musicVol = .5f;
        this.sfxVol = .5f;
        this.gfxQuality = 2;
        this.isFullScreen = false;
        this.screenWidth = 1280;
        this.screenHeight = 720;
    }
}
