using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using System.Net;
using Unity.VisualScripting;
using Application = UnityEngine.Device.Application;

public class LoadWords : MonoBehaviour
{
    
    public void LoadFile(string dictName)
    {
        string path = Application.dataPath + "/Dictionaries/" + dictName + ".ddt";

        if (File.Exists(path))
        {
            GameManager.Instance.wordList = File.ReadAllLines(path);
            
            int i = 0;
            string longest = "didn't work";
            foreach (string line in GameManager.Instance.wordList)
            {
                if (line.Length > i)
                {
                    i = line.Length;
                    longest = line;
                }
            }
            Debug.Log("Longest word in " + dictName + ": " + longest + " - " + longest.Length);
            
        }
        else
        {
            //Show an error canvas panel that says the application is corrupted and needs to be reinstalled
            Debug.Log("Could not load word list");
        }
    }


}
