using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomisationGet : MonoBehaviour
{
    public Renderer character, helm;
    public TextSaving loadText;
    public string saveSlot = "SaveSlot1";
    public string path;
    void Awake()
    {
        path = Application.persistentDataPath + saveSlot;
        loadText.CharacterLoadSlot(path);

        gameObject.name = loadText.loadData[0];
        SetTexture("Skin", int.Parse(loadText.loadData[3]));
        SetTexture("Mouth", int.Parse(loadText.loadData[4]));
        SetTexture("Eyes", int.Parse(loadText.loadData[5]));
        SetTexture("Hair", int.Parse(loadText.loadData[6]));
        SetTexture("Clothes", int.Parse(loadText.loadData[7]));
        SetTexture("Armour", int.Parse(loadText.loadData[8]));
        SetTexture("Helm", int.Parse(loadText.loadData[9]));

        //element 0 = name
        //element 1 = class
        //element 2 = race
        //element 3-9 = texture
        //element 10-15 = stats
    }

    void SetTexture(string type, int index)
    {
        Texture2D texture = null;
        int materialIndex = 0;
        Renderer renderer = null;
        switch (type)
        {
            case "Skin":
                texture = Resources.Load("Character/Skin_" + index) as Texture2D;
                materialIndex = 1;
                renderer = character;
                break;
            case "Mouth":
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                materialIndex = 2;
                renderer = character;
                break;
            case "Eyes":
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                materialIndex = 3;
                renderer = character;
                break;
            case "Hair":
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                materialIndex = 4;
                renderer = character;
                break;
            case "Clothes":
                texture = Resources.Load("Character/Clothes_" + index) as Texture2D;
                materialIndex = 5;
                renderer = character;
                break;
            case "Armour":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                materialIndex = 6;
                renderer = character;
                break;
            case "Helm":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                materialIndex = 1;
                renderer = helm;
                break;
        }

        Material[] mats = renderer.materials; // Grab the existing Materials array from the renderer
        mats[materialIndex].mainTexture = texture;  // Sets the new materials to the new temporary material arrayI
        renderer.materials = mats; // Overwrite the original array with the new array
    }
}
