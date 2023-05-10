//using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;

public class TextSaving : MonoBehaviour
{
    public string[] loadData;
    public void CharacterSaveSlot(string path, string content)
    {
        //Path of the file
        string path1 = Application.streamingAssetsPath;
        string path2 = Application.dataPath + "SaveSlot1";
        
        //Create file if it doesn't exist
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "");

        }

        //Add the content to the file
        //Adds more to the file.. not as new lines though
        //File.AppendAllText(path3, content);
        File.WriteAllText(path, content);

        Debug.Log(path);
    }

    public void CharacterLoadSlot(string path)
    {
        if (File.Exists(path))
        {
            loadData = File.ReadAllLines(path);
        }
    }
}
