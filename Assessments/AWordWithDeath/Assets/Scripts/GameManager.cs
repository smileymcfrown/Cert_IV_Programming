using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int score;
    public int round;
    public string[] wordList;
    public List<string> usedWords = new List<string>();
    public Vector3 bladeStartPos = new Vector3();

    private void Awake()
    {
        //Make sure this is the only instance of GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}
