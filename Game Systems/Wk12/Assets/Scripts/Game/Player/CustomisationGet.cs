using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomisationGet : MonoBehaviour
{
    public Renderer character, helm;
   //public string path = "C:/Users/jsargent6/AppData/LocalLow/ScrubHaus/Game System GB2/SaveSlot1";
    public string path;
    public string saveSlot = "SaveSlot1";
    public TextSaving loadingText;
    void Awake()
    {
        /*path = Application.persistentDataPath + "/"+ saveSlot;
        loadingText.CharacterLoadSlot(path);
        SetTexture("Skin", int.Parse(loadingText.loadData[0]));
        SetTexture("Mouth", int.Parse(loadingText.loadData[1]));
        SetTexture("Eyes", int.Parse(loadingText.loadData[2]));
        SetTexture("Hair", int.Parse(loadingText.loadData[3]));
        SetTexture("Clothes", int.Parse(loadingText.loadData[4]));
        SetTexture("Armour", int.Parse(loadingText.loadData[5]));
        SetTexture("Helm", int.Parse(loadingText.loadData[6]));
        gameObject.name = loadingText.loadData[7];*/
    }
    //element 0-6 is texture
    //element 7 is name
    //element 8 is class
    //element 9 is race
    //element 10 - 15 are stats

    void SetTexture(string type, int index)
    {
        Texture2D texture = null;
        int materialIndex = 0;
        Renderer renderer = null;
        switch (type)
        {
            case "Skin":
                texture = Resources.Load("Character/Skin_"+index)as Texture2D;
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
        Material[] mats = renderer.materials;//grabs the material array on the renderer
        mats[materialIndex].mainTexture = texture;//sets the texture of the selected material to the texture we want it to be
        renderer.materials = mats;//put that thing back where it came from or so help me !
    }
}
