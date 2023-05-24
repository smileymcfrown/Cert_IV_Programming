using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;

public class LoadWords : MonoBehaviour
{
    public string[] wordList;

    public void LoadFile(string dictName)
    {
        string path = Application.streamingAssetsPath + dictName;

        if (File.Exists(path))
        {
            wordList = File.ReadAllLines(path);
        }
        else
        {
            //Show an error canvas panel that says the application is corrupted and needs to be reinstalled
        }
    }

    public string SelectWord()
    {
        return wordList[Random.Range(0, wordList.Length - 1)];
    }
}
