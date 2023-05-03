using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditorInternal;
using UnityEngine;
//you will need to change Scenes
using UnityEngine.SceneManagement;
public class CustomisationSet : Stats
{
    #region Variables   
    [Header("Character Name")]
    //name of our character that the user is making
    public string characterName;
    [Header("Texture List")]
    //Texture2D List for skin,hair, mouth, eyes
    public List<Texture2D> skin = new List<Texture2D>(); //1
    public List<Texture2D> mouth = new List<Texture2D>(); //2
    public List<Texture2D> eyes = new List<Texture2D>(); //3
    public List<Texture2D> hair = new List<Texture2D>(); //4
    public List<Texture2D> clothes = new List<Texture2D>();//5
    public List<Texture2D> armour = new List<Texture2D>(); //6
    [Header("Index")]
    //index numbers for our current skin, hair, mouth, eyes, clothes and armour textures
    public int skinIndex;
    public int mouthIndex, eyesIndex, hairIndex, clothesIndex, armourIndex, helmIndex;
    [Header("Renderer")]
    //renderer for our character mesh so we can reference a material list
    public Renderer character;
    public Renderer helm;
    [Header("Max Index")]
    //max amount of skin, hair, mouth, eyes, clothes and armour textures that our lists are filling with
    public int skinMax;
    public int mouthMax, eyesMax, hairMax, clothesMax, armourMax;

    public string[] materialNames = new string[7] { "Skin", "Mouth", "Eyes", "Hair", "Clothes", "Armour", "Helm" };
    public Vector2 screen;

    [Header("Class and Race")]
    public bool raceDrop;
    public string raceDropDisplay = "Select Race";
    public bool classDrop;
    public string classDropDisplay = "Select Class";
    public Vector2 scrollPosRace, scrollPosClass;
    public int bonusStats = 6;
    public string[] statName = new string[6] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };


    #endregion

    #region Start
    //in start we need to set up the following
    private void Start()
    {
        #region for loop to pull textures from file
        //for loop looping from 0 to less than the max amount of skin textures we need
        for (int i = 0; i < skinMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Skin_#    
            Texture2D temp = Resources.Load("Character/Skin_" + i) as Texture2D;
            //add our temp texture that we just found to the skin List
            skin.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < mouthMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            //add our temp texture that we just found to the  List
            mouth.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < eyesMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            //add our temp texture that we just found to the  List
            eyes.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < hairMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            //add our temp texture that we just found to the  List
            hair.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < clothesMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            //add our temp texture that we just found to the  List
            clothes.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of textures we need
        for (int i = 0; i < armourMax; i++)
        {
            //creating a temp Texture2D that it grabs, using Resources.Load from the Character File looking for Type_#
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            //add our temp texture that we just found to the  List
            armour.Add(temp);
        }
        #endregion
        //connect and find the Renderer thats in the scene to the variable we made for Renderer 
        character = GameObject.Find("Mesh").GetComponent<Renderer>();
        helm = GameObject.Find("cap").GetComponent<Renderer>();
        #region do this after making the function SetTexture
        //SetTexture for all materials to the first texture 0    
        #endregion
    }
    #endregion

    #region SetTexture
    //Create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing 
    void SetTexture(string type, int dir)
    {
        //we need variables that exist only within this function
        //these are ints index numbers, max numbers, material index and Texture2D array of textures and our renderer
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        Renderer curRend = new Renderer();
        #region Switch Material
        //inside a switch statement that is swapped by the string name of our material  
        switch (type)
        {
            #region Skin
            //case skin
            case "Skin":
                //index is the same as our skin index
                index = skinIndex;
                //max is the same as our skin max
                max = skinMax;
                //textures is our skin list .ToArray()
                textures = skin.ToArray();
                //material index element number
                matIndex = 1;
                //current renderer is the mesh renderer that we are getting the materials from
                curRend = character;
                //end case
                break;
            #endregion
            #region Mouth
            //case Mouth
            case "Mouth":
                //index is the same as our Mouth index
                index = mouthIndex;
                //max is the same as our Mouth max
                max = mouthMax;
                //textures is our Mouth list .ToArray()
                textures = mouth.ToArray();
                //material index element number
                matIndex = 2;
                curRend = character;

                //end case
                break;
            #endregion
            #region Eyes
            //case Eyes
            case "Eyes":
                //index is the same as our Eyes index
                index = eyesIndex;
                //max is the same as our Eyes max
                max = eyesMax;
                //textures is our Eyes list .ToArray()
                textures = eyes.ToArray();
                //material index element number
                matIndex = 3;
                curRend = character;
                //end case
                break;
            #endregion
            #region Hair
            case "Hair":
                index = hairIndex;
                //index is the same as our  index
                max = hairMax;
                //max is the same as our  max
                textures = hair.ToArray();
                //textures is our  list .ToArray()
                matIndex = 4;
                curRend = character;
                //material index element number
                break;
            #endregion
            #region Clothes
            case "Clothes":
                index = clothesIndex;
                //index is the same as our  index
                max = clothesMax;
                //max is the same as our max
                textures = clothes.ToArray();
                //textures is our  list .ToArray()
                matIndex = 5;
                curRend = character;
                //material index element number
                break;
            #endregion
            #region Armour
            case "Armour":
                index = armourIndex;
                //index is the same as our  index
                max = armourMax;
                //max is the same as our max
                textures = armour.ToArray();
                //textures is our  list .ToArray()
                matIndex = 6;
                curRend = character;
                //material index element number
                break;
            //break
            case "Helm":
                index = helmIndex;
                //index is the same as our  index
                max = armourMax;
                //max is the same as our max
                textures = armour.ToArray();
                //textures is our  list .ToArray()
                matIndex = 1;
                curRend = helm;
                //material index element number
                break;
                //break
                #endregion
        }
        #endregion
        //outside our switch statement
        #region Assign Direction
        //index plus equals our direction
        index += dir;
        //cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        //Material array is equal to our characters material list
        Material[] mat = curRend.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        mat[matIndex].mainTexture = textures[index];
        //our characters materials are equal to the material array
        curRend.materials = mat;
        #endregion
        //create another switch that is goverened by the same string name of our material
        #region Set Material Switch
        switch (type)
        {
            //case skin
            case "Skin":
                //skin index equals our index
                skinIndex = index;
                //break
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Eyes":
                eyesIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Clothes":
                clothesIndex = index;
                break;
            case "Armour":
                armourIndex = index;
                break;
            case "Helm":
                helmIndex = index;
                break;
        }
        #endregion
    }
    #endregion

    void SelectClass(int classIndex)
    {
        switch (classIndex)
        {
            case 0:
                characterStats[0].statValue = 13; //str
                characterStats[1].statValue = 11; //dex
                characterStats[2].statValue = 12; //con
                characterStats[3].statValue = 10; //int
                characterStats[4].statValue = 6; //wis
                characterStats[5].statValue = 8; //char
                characterClass = CharacterClass.Barbarian; //Done
                break;  
            case 1:
                characterStats[0].statValue = 6;
                characterStats[1].statValue = 10;
                characterStats[2].statValue = 8;
                characterStats[3].statValue = 12;
                characterStats[4].statValue = 11;
                characterStats[5].statValue = 13;
                characterClass = CharacterClass.Bard; //Done
                break; 
            case 2:
                characterStats[0].statValue = 10;
                characterStats[1].statValue = 6;
                characterStats[2].statValue = 11;
                characterStats[3].statValue = 8;
                characterStats[4].statValue = 13;
                characterStats[5].statValue = 12;
                characterClass = CharacterClass.Cleric; //Done
                break;
            case 3:
                characterStats[0].statValue = 6;
                characterStats[1].statValue = 12;
                characterStats[2].statValue = 8;
                characterStats[3].statValue = 11;
                characterStats[4].statValue = 13;
                characterStats[5].statValue = 10;
                characterClass = CharacterClass.Druid; //Done
                break;
            case 4:
                characterStats[0].statValue = 13;
                characterStats[1].statValue = 11;
                characterStats[2].statValue = 12;
                characterStats[3].statValue = 8;
                characterStats[4].statValue = 6;
                characterStats[5].statValue = 10;
                characterClass = CharacterClass.Fighter;
                break;
            case 5:
                characterStats[0].statValue = 13;
                characterStats[1].statValue = 11;
                characterStats[2].statValue = 12;
                characterStats[3].statValue = 10;
                characterStats[4].statValue = 6;
                characterStats[5].statValue = 8;
                characterClass = CharacterClass.Monk;
                break;
            case 6:
                characterStats[0].statValue = 13;
                characterStats[1].statValue = 11;
                characterStats[2].statValue = 12;
                characterStats[3].statValue = 10;
                characterStats[4].statValue = 6;
                characterStats[5].statValue = 8;
                characterClass = CharacterClass.Paladin;
                break;
            case 7:
                characterStats[0].statValue = 13;
                characterStats[1].statValue = 11;
                characterStats[2].statValue = 12;
                characterStats[3].statValue = 10;
                characterStats[4].statValue = 6;
                characterStats[5].statValue = 8;
                characterClass = CharacterClass.Ranger;
                break;
            case 8:
                characterStats[0].statValue = 13;
                characterStats[1].statValue = 11;
                characterStats[2].statValue = 12;
                characterStats[3].statValue = 10;
                characterStats[4].statValue = 6;
                characterStats[5].statValue = 8;
                characterClass = CharacterClass.Rogue;
                break;
            case 9:
                characterStats[0].statValue = 13;
                characterStats[1].statValue = 11;
                characterStats[2].statValue = 12;
                characterStats[3].statValue = 10;
                characterStats[4].statValue = 6;
                characterStats[5].statValue = 8;
                characterClass = CharacterClass.Sorcerer;
                break;
            case 10:
                characterStats[0].statValue = 13;
                characterStats[1].statValue = 11;
                characterStats[2].statValue = 12;
                characterStats[3].statValue = 10;
                characterStats[4].statValue = 6;
                characterStats[5].statValue = 8;
                characterClass = CharacterClass.Warlock;
                break;
        }
    }
    void SelectRace(int raceIndex)
    {
        switch (raceIndex)
        {
            case 0:
                characterStats[0].tempStatValue = 3; //str
                characterStats[1].tempStatValue = 0; //dex
                characterStats[2].tempStatValue = 0; //con
                characterStats[3].tempStatValue = 0; //int
                characterStats[4].tempStatValue = 0; //wis
                characterStats[5].tempStatValue = 3; //char
                break;
        }
    }    
    public override void Update()
    {

    }

    private void OnGUI()
    {
        //create the floats scrW and scrH that govern our 16:9 ratio
        screen.x = Screen.width / 16;
        screen.y = Screen.height / 9;

        #region Change Textures
        for (int i = 0; i < materialNames.Length; i++)
        {
            if (GUI.Button(new Rect(0.25f * screen.x, 2.5f * screen.y + (i * 0.5f * screen.y), 0.5f * screen.x, 0.5f * screen.y), "<"))
            {
                SetTexture(materialNames[i], -1);
            }

            GUI.Box(new Rect(0.75f * screen.x, 2.5f * screen.y + (i * 0.5f * screen.y), 1.5f * screen.x, 0.5f * screen.y), materialNames[i]);

            if (GUI.Button(new Rect(2.25f * screen.x, 2.5f * screen.y + (i * 0.5f * screen.y), 0.5f * screen.x, 0.5f * screen.y), ">"))
            {
                SetTexture(materialNames[i], 1);
            }
        }
        #endregion

        #region Random and Reset
        if (GUI.Button(new Rect(0.25f * screen.x, 2.5f * screen.y + (materialNames.Length * 0.5f * screen.y),1.25f * screen.x, 0.5f * screen.y), "Random"))
        {
            skinIndex = Random.Range(0, skinMax);
            mouthIndex = Random.Range(0, mouthMax);
            eyesIndex = Random.Range(0, eyesMax); 
            hairIndex = Random.Range(0, hairMax);
            clothesIndex = Random.Range(0, clothesMax);
            armourIndex =   Random.Range(0, armourMax);
            helmIndex = Random.Range(0, armourMax);

            for (int i = 0; i < materialNames.Length; i++)
            {
                SetTexture(materialNames[i], 0);
            }
        }

        if (GUI.Button(new Rect(1.5f * screen.x, 2.5f * screen.y + (materialNames.Length * 0.5f * screen.y),1.25f * screen.x, 0.5f * screen.y), "Reset"))
        {

            //Way 1 to reset all the shit
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Clothes", clothesIndex = 0);
            SetTexture("Armour", armourIndex = 0);
            SetTexture("Helm", helmIndex = 0);

            //Way 2 to reset all the shit
            skinIndex = 0;
            mouthIndex = 0; 
            eyesIndex = 0;
            hairIndex = 0;  
            clothesIndex = 0;  
            armourIndex = 0;    
            helmIndex = 0;
            for (int i = 0; i < materialNames.Length; i++)
            {
                SetTexture(materialNames[i], 0);
            }
        }
        #endregion

        #region Character Name
        characterName = GUI.TextArea(new Rect(0.25f * screen.x, 2.5f * screen.y + ((materialNames.Length +1) * 0.5f * screen.y), 2.5f * screen.x, 0.5f * screen.y), characterName, 32);
        #endregion

        #region Class Select
        //button for toggling dropdown
        if (GUI.Button(new Rect(12.75f * screen.x, 2.5f * screen.y,2 * screen.x, 0.5f * screen.y), classDropDisplay))
        {
            classDrop = !classDrop;
        }
        // if dropdown - scroll view that displays our classes as seletable buttons
        if (classDrop)
        {
            int listSize = System.Enum.GetNames(typeof(CharacterClass)).Length - 1;
            scrollPosClass = GUI.BeginScrollView(new Rect(12.75f * screen.x, 3f * screen.y, 2f * screen.x, 4f * screen.y), scrollPosClass, new Rect(0,0,0,listSize * 0.5f * screen.y));

            //Blank background box
            GUI.Box(new Rect(0, 0, 1.75f * screen.x, listSize * 0.5f * screen.y), "");

            //Loop to populate buttons
            for (int i = 0; i < listSize; i++)
            {
                if (GUI.Button(new Rect(0, 0.5f * screen.y * i, 1.75f * screen.x, 0.5f * screen.y), System.Enum.GetNames(typeof(CharacterClass))[i+1]))
                {
                    SelectClass(i);
                    classDropDisplay = System.Enum.GetNames(typeof(CharacterClass))[i + 1];
                    classDrop = !classDrop;
                }
            }
            GUI.EndScrollView();
        }
        #endregion
        else
        {
            #region Race Select
            if (GUI.Button(new Rect(12.75f * screen.x, 3f * screen.y, 2 * screen.x, 0.5f * screen.y), raceDropDisplay))
            {
                raceDrop = !raceDrop;
            }
            if (raceDrop)
            {
                int listSize = System.Enum.GetNames(typeof(CharacterRace)).Length - 1;
                scrollPosClass = GUI.BeginScrollView(new Rect(12.75f * screen.x, 3.5f * screen.y, 2 * screen.x, 4 * screen.y),scrollPosClass, new Rect(0,0,0, listSize * 0.5f * screen.y));

                GUI.Box(new Rect(0, 0, 1.75f * screen.x, listSize * 0.5f * screen.y), "");

                for (int i = 0; i < listSize; i++)
                {
                    if (GUI.Button(new Rect(0, 0.5f * screen.y * i, 1.75f * screen.x, 0.5f * screen.y), System.Enum.GetNames(typeof(CharacterRace))[i + 1]))
                    {
                        SelectRace(i);
                        classDropDisplay = System.Enum.GetNames(typeof(CharacterRace))[i+1];
                        raceDrop = !raceDrop;
                    }
                }
               GUI.EndScrollView();
            }
            #endregion
        }
        #region Add Points
        if (!classDrop && !raceDrop)
        {
            GUI.Box(new Rect(12.75f * screen.x, 3.5f * screen.y, 2 * screen.x, 0.5f * screen.y), "Points: " + bonusStats);

            for (int i = 0; i < characterStats.Length; i++)
            {
                if (GUI.Button(new Rect(12.25f * screen.x, 4 * screen.y + (i * 0.5f * screen.y), 0.5f * screen.x, 0.5f * screen.y), "-"))
                {
                    bonusStats++;
                    characterStats[i].levelTempStatValue--;
                }

                GUI.Box(new Rect(12.75f * screen.x, 4 * screen.y + (i * 0.5f * screen.y), 2 * screen.x, 0.5f * screen.y), statName[i] + ": " + (characterStats[i].statValue + characterStats[i].tempStatValue + characterStats[i].levelTempStatValue));

                if (bonusStats > 0)
                {
                    if(GUI.Button(new Rect(14.75f * screen.x, 4 * screen.y + (i * 0.5f * screen.y), 0.5f * screen.x, 0.5f * screen.y), "+"))
                    {
                        bonusStats--;
                        characterStats[i].levelTempStatValue++;
                    }
                }
            }
        }
        #endregion

        #region Save and Play
        //Display button if Name, Class, Race, and Stats have been selected
        if (true)
        {
            //GUI button called Save and Play
            if (GUI.Button(new Rect(),"Save and Play"))
            {
                //Button will run the save function then load game level
                SaveCharacter();
                SceneManager.LoadScene(2);

            }
        }
        #endregion
    }
    void SaveCharacter()
    {
        //Create a player pref thats an int, give it a name and a value
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        PlayerPrefs.SetInt("HelmIndex", helmIndex);

        //Do the same thing with values that are strings
        PlayerPrefs.SetString("CharacterName", characterName);
        PlayerPrefs.SetString("CharacterClass", characterClass.ToString());
        PlayerPrefs.SetString("CharacterRace", characterRace.ToString());

        //Loop through and add the stats
        for (int i = 0; i < characterStats.Length; i++)
        {
            PlayerPrefs.SetInt(characterStats[i].name, (characterStats[i].statValue + characterStats[i].tempStatValue + characterStats[i].levelTempStatValue));
        }
    }
}


