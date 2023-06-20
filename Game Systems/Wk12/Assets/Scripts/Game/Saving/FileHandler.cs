using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;

public class FileHandler
{
    //List of LevelData class(es? instances?)
    //public static List<LevelData> savedLevels = new List<LevelData>();

    private string dataPath = "";
    private string dataFile = "";
    
    public FileHandler(string dataPath, string dataFile)
    {
        this.dataPath = dataPath;
        this.dataFile = dataFile;
    }

    // Loading and Saving Game Data to persistantDataPath/savegame.dat so it will appear in the build folder
    // Saved game is loaded into loadedData and returned to DataManager method that called it.

    public GameData LoadGame()
    {
        GameData loadedData = null;
        // Check for the level file
        if (File.Exists(dataPath + dataFile)) //(Application.persistentDataPath + "/savegame.dat");
        {
            // Get a binary formatter, open a file, deserialize from binary,
            // pipe it into a GameData variable, and close it up; bing bang bosh!
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath + dataFile, FileMode.Open);
            loadedData = (GameData)bf.Deserialize(file);
            file.Close();

            //To confirm file location
            Debug.Log(dataPath);
        }
        return loadedData;
    }

    public void SaveGame(GameData gameData)
    {
        // Get a binary formatter, open a file, serialize LevelData into binary and pipe it in, and close it up; bing bang bosh!
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath + dataFile);
        bf.Serialize(file, gameData);
        file.Close();

        //To confirm file location
        Debug.Log(dataPath);
    }
    
    public SettingsData LoadSettings()
    {
        SettingsData loadedData = null;
        // Check for the level file
        if (File.Exists(dataPath + dataFile)) //(Application.persistentDataPath + "/savegame.dat");
        {
            // Get a binary formatter, open a file, deserialize from binary,
            // pipe it into a GameData variable, and close it up; bing bang bosh!
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath + dataFile, FileMode.Open);
            loadedData = (SettingsData)bf.Deserialize(file);
            file.Close();

            //To confirm file location
            Debug.Log(dataPath + dataFile);
        }
        return loadedData;
    }

    public void SaveSettings(SettingsData settingsData)
    {
        // Get a binary formatter, open a file, serialize LevelData into binary and pipe it in, and close it up; bing bang bosh!
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath + dataFile);
        bf.Serialize(file, settingsData);
        file.Close();

        //To confirm file location
        Debug.Log(dataPath + dataFile);
    }
}

