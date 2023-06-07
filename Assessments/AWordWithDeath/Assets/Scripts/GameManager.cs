using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int score;
    public int round;
    //public List<string> wordList = new List<string>();
    public string[] wordList;
    public List<string> usedWords = new List<string>();

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

    // Start is called before the first frame update
    void Start()
    {
        //wordList.Add("Fuck This");
        //Debug.Log(wordList[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 


}
