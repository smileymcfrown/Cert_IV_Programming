using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextSaving : MonoBehaviour
{
    public string[] loadData;
    //content of the file
    //Path of the file
    public void CharacterSaveSlot(string path, string content)
    {        
        //Create File if it doesnt exist
        if (!File.Exists(path))
        {
                            //Path and Content
            File.WriteAllText(path, "");
        }
        //Add the content to the file
        //Append allows to to add more to the file
      // File.AppendAllText(path3, content);
        //Write creates or replaces a text file
       File.WriteAllText(path, content);

        Debug.Log(path);
    }
    public void CharacterLoadSlot(string path)
    {
        //if the path exists
        if (File.Exists(path))
        {
            //read file
            loadData = File.ReadAllLines(path);
        }
    }

}
