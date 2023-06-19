using System.Collections.Generic;
using UnityEngine;
//you will need to change Scenes
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Rendering;
using System.Linq;

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

    public TextSaving textSaving;

    #endregion

    #region Start
    //in start we need to set up the following
    private void Start()
    {
        //Load();
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
    #region Select Class
    void SelectClass(int classIndex)
    {
        //create a switch statment that holds the base stats for each class

        switch (classIndex)
        {
            case 0:
                characterStats[0].statValue = 13;//str
                characterStats[1].statValue = 11;//dex
                characterStats[2].statValue = 12;//con
                characterStats[3].statValue = 10;//int
                characterStats[4].statValue = 6;//wis
                characterStats[5].statValue = 8;//char
                characterClass = CharacterClass.Barbarian;
                break;
            #region the rest of the stats for class
            case 1:
                characterStats[0].statValue = 6;//str
                characterStats[1].statValue = 10;//dex
                characterStats[2].statValue = 8;//con
                characterStats[3].statValue = 12;//int
                characterStats[4].statValue = 11;//wis
                characterStats[5].statValue = 13;//char
                characterClass = CharacterClass.Bard;
                break;
            case 2:
                characterStats[0].statValue = 10;//str
                characterStats[1].statValue = 6;//dex
                characterStats[2].statValue = 11;//con
                characterStats[3].statValue = 8;//int
                characterStats[4].statValue = 13;//wis
                characterStats[5].statValue = 12;//char
                characterClass = CharacterClass.Cleric;
                break;
            case 3:
                characterStats[0].statValue = 6;//str
                characterStats[1].statValue = 12;//dex
                characterStats[2].statValue = 8;//con
                characterStats[3].statValue = 11;//int
                characterStats[4].statValue = 13;//wis
                characterStats[5].statValue = 10;//char
                characterClass = CharacterClass.Druid;
                break;
            case 4:
                characterStats[0].statValue = 13;//str
                characterStats[1].statValue = 11;//dex
                characterStats[2].statValue = 12;//con
                characterStats[3].statValue = 8;//int
                characterStats[4].statValue = 6;//wis
                characterStats[5].statValue = 10;//char
                characterClass = CharacterClass.Fighter;
                break;
            case 5:
                characterStats[0].statValue = 10;//str
                characterStats[1].statValue = 13;//dex
                characterStats[2].statValue = 8;//con
                characterStats[3].statValue = 11;//int
                characterStats[4].statValue = 12;//wis
                characterStats[5].statValue = 6;//char
                characterClass = CharacterClass.Monk;
                break;
            case 6:
                characterStats[0].statValue = 12;//str
                characterStats[1].statValue = 10;//dex
                characterStats[2].statValue = 11;//con
                characterStats[3].statValue = 6;//int
                characterStats[4].statValue = 8;//wis
                characterStats[5].statValue = 13;//char
                characterClass = CharacterClass.Paladin;
                break;
            case 7:
                characterStats[0].statValue = 13;//str
                characterStats[1].statValue = 12;//dex
                characterStats[2].statValue = 8;//con
                characterStats[3].statValue = 10;//int
                characterStats[4].statValue = 11;//wis
                characterStats[5].statValue = 6;//char
                characterClass = CharacterClass.Ranger;
                break;
            case 8:
                characterStats[0].statValue = 6;//str
                characterStats[1].statValue = 13;//dex
                characterStats[2].statValue = 8;//con
                characterStats[3].statValue = 10;//int
                characterStats[4].statValue = 11;//wis
                characterStats[5].statValue = 12;//char
                characterClass = CharacterClass.Rogue;
                break;
            case 9:
                characterStats[0].statValue = 6;//str
                characterStats[1].statValue = 8;//dex
                characterStats[2].statValue = 13;//con
                characterStats[3].statValue = 12;//int
                characterStats[4].statValue = 11;//wis
                characterStats[5].statValue = 10;//char
                characterClass = CharacterClass.Sorcerer;
                break;
            case 10:
                characterStats[0].statValue = 8;//str
                characterStats[1].statValue = 6;//dex
                characterStats[2].statValue = 12;//con
                characterStats[3].statValue = 13;//int
                characterStats[4].statValue = 11;//wis
                characterStats[5].statValue = 10;//char
                characterClass = CharacterClass.Warlock;
                break;
            case 11:
                characterStats[0].statValue = 8;//str
                characterStats[1].statValue = 10;//dex
                characterStats[2].statValue = 8;//con
                characterStats[3].statValue = 13;//int
                characterStats[4].statValue = 12;//wis
                characterStats[5].statValue = 11;//char
                characterClass = CharacterClass.Wizard;
                break;
                #endregion
        }
    }
    #endregion
    #region Select Race
    void SelectRace(int raceIndex)
    {
        switch (raceIndex)
        {
            case 0:
                characterStats[0].tempStatValue = 3;//str
                characterStats[1].tempStatValue = 0;//dex
                characterStats[2].tempStatValue = 0;//con
                characterStats[3].tempStatValue = 0;//int
                characterStats[4].tempStatValue = 0;//wis
                characterStats[5].tempStatValue = 3;//char
                characterRace = CharacterRace.Dragonborn;
                break;
            #region the rest of the stats for race
            case 1:
                characterStats[0].tempStatValue = 3;//str
                characterStats[1].tempStatValue = 0;//dex
                characterStats[2].tempStatValue = 3;//con
                characterStats[3].tempStatValue = 0;//int
                characterStats[4].tempStatValue = 0;//wis
                characterStats[5].tempStatValue = 0;//char
                characterRace = CharacterRace.Dwarf;
                break;
            case 2:
                characterStats[0].tempStatValue = 0;//str
                characterStats[1].tempStatValue = 4;//dex
                characterStats[2].tempStatValue = 0;//con
                characterStats[3].tempStatValue = 1;//int
                characterStats[4].tempStatValue = 0;//wis
                characterStats[5].tempStatValue = 1;//char
                characterRace = CharacterRace.Elf;
                break;
            case 3:
                characterStats[0].tempStatValue = 1;//str
                characterStats[1].tempStatValue = 1;//dex
                characterStats[2].tempStatValue = 0;//con
                characterStats[3].tempStatValue = 4;//int
                characterStats[4].tempStatValue = 0;//wis
                characterStats[5].tempStatValue = 0;//char
                characterRace = CharacterRace.Gnome;
                break;
            case 4:
                characterStats[0].tempStatValue = 0;//str
                characterStats[1].tempStatValue = 2;//dex
                characterStats[2].tempStatValue = 1;//con
                characterStats[3].tempStatValue = 0;//int
                characterStats[4].tempStatValue = 0;//wis
                characterStats[5].tempStatValue = 3;//char
                characterRace = CharacterRace.HalfElf;
                break;
            case 5:
                characterStats[0].tempStatValue = 0;//str
                characterStats[1].tempStatValue = 3;//dex
                characterStats[2].tempStatValue = 1;//con
                characterStats[3].tempStatValue = 0;//int
                characterStats[4].tempStatValue = 1;//wis
                characterStats[5].tempStatValue = 1;//char
                characterRace = CharacterRace.Halfling;
                break;
            case 6:
                characterStats[0].tempStatValue = 6;//str
                characterStats[1].tempStatValue = 0;//dex
                characterStats[2].tempStatValue = 3;//con
                characterStats[3].tempStatValue = 0;//int
                characterStats[4].tempStatValue = -2;//wis
                characterStats[5].tempStatValue = -1;//char
                characterRace = CharacterRace.HalfOrc;
                break;
            case 7:
                characterStats[0].tempStatValue = 1;//str
                characterStats[1].tempStatValue = 1;//dex
                characterStats[2].tempStatValue = 1;//con
                characterStats[3].tempStatValue = 1;//int
                characterStats[4].tempStatValue = 1;//wis
                characterStats[5].tempStatValue = 1;//char
                characterRace = CharacterRace.Human;
                break;
            case 8:
                characterStats[0].tempStatValue = 0;//str
                characterStats[1].tempStatValue = 0;//dex
                characterStats[2].tempStatValue = 0;//con
                characterStats[3].tempStatValue = 2;//int
                characterStats[4].tempStatValue = 1;//wis
                characterStats[5].tempStatValue = 3;//char
                characterRace = CharacterRace.Tiefling;
                break;
                #endregion
        }
    }
    #endregion
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
        //create 2 buttons one Random and one Reset
        //Random will feed a random amount to the direction 
        if (GUI.Button(new Rect(0.25f * screen.x, 2.5f * screen.y + (materialNames.Length * 0.5f * screen.y), 1.25f * screen.x, 0.5f * screen.y), "Random"))
        {
            skinIndex = Random.Range(0, skinMax);
            mouthIndex = Random.Range(0, mouthMax);
            eyesIndex = Random.Range(0, eyesMax);
            hairIndex = Random.Range(0, hairMax);
            clothesIndex = Random.Range(0, clothesMax);
            armourIndex = Random.Range(0, armourMax);
            helmIndex = Random.Range(0, armourMax);

            for (int i = 0; i < materialNames.Length; i++)
            {
                SetTexture(materialNames[i], 0);
            }
        }
        //reset will set all to 0 both use SetTexture
        if (GUI.Button(new Rect(1.5f * screen.x, 2.5f * screen.y + (materialNames.Length * 0.5f * screen.y), 1.25f * screen.x, 0.5f * screen.y), "Reset"))
        {
            // Way 1
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Clothes", clothesIndex = 0);
            SetTexture("Armour", armourIndex = 0);
            SetTexture("Helm", helmIndex = 0);
            //way 2
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
        //name of our character equals a GUI TextField that holds our character name and limit of characters
        //move down the screen or place somewhere else
        characterName = GUI.TextArea(new Rect(0.25f * screen.x, 2.5f * screen.y + ((materialNames.Length + 1) * 0.5f * screen.y), 2.5f * screen.x, 0.5f * screen.y), characterName, 32);
        #endregion
        #region Class Select
        //button for toggling dropdown
        if (GUI.Button(new Rect(12.75f * screen.x, 2.5f * screen.y, 2 * screen.x, 0.5f * screen.y), classDropDisplay))
        {
            classDrop = !classDrop;
        }
        //if dropdown - scroll view that displays our classes as selectable buttons
        if (classDrop)
        {
            int listSize = System.Enum.GetNames(typeof(CharacterClass)).Length - 1;
            scrollPosClass = GUI.BeginScrollView(new Rect(12.75f * screen.x, 3 * screen.y, 2 * screen.x, 4 * screen.y), scrollPosClass, new Rect(0, 0, 0, listSize * 0.5f * screen.y));
            //blank background box
            GUI.Box(new Rect(0, 0, 1.75f * screen.x, listSize * 0.5f * screen.y), "");
            //loof for the button options
            for (int i = 0; i < listSize; i++)
            {
                if (GUI.Button(new Rect(0, 0.5f * screen.y * i, 1.75f * screen.x, 0.5f * screen.y), System.Enum.GetNames(typeof(CharacterClass))[i + 1]))
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
            //button for toggling dropdown
            if (GUI.Button(new Rect(12.75f * screen.x, 3f * screen.y, 2 * screen.x, 0.5f * screen.y), raceDropDisplay))
            {
                raceDrop = !raceDrop;
            }
            //if dropdown - scroll view that displays our races as selectable buttons
            if (raceDrop)
            {
                int listSize = System.Enum.GetNames(typeof(CharacterRace)).Length - 1;
                scrollPosRace = GUI.BeginScrollView(new Rect(12.75f * screen.x, 3.5f * screen.y, 2 * screen.x, 4 * screen.y), scrollPosRace, new Rect(0, 0, 0, listSize * 0.5f * screen.y));
                //blank background box
                GUI.Box(new Rect(0, 0, 1.75f * screen.x, listSize * 0.5f * screen.y), "");
                //loof for the button options
                for (int i = 0; i < listSize; i++)
                {
                    if (GUI.Button(new Rect(0, 0.5f * screen.y * i, 1.75f * screen.x, 0.5f * screen.y), System.Enum.GetNames(typeof(CharacterRace))[i + 1]))
                    {
                        SelectRace(i);
                        raceDropDisplay = System.Enum.GetNames(typeof(CharacterRace))[i + 1];
                        raceDrop = !raceDrop;
                    }
                }
                GUI.EndScrollView();
            }
            #endregion
        }
        #region Add Points
        // stats - display stats
        if (!classDrop && !raceDrop)
        {
            //Box for points to spend
            GUI.Box(new Rect(12.75f * screen.x, 3.5f * screen.y, 2 * screen.x, 0.5f * screen.y), "Points: " + bonusStats);
            // + and - buttons on either side of a box/label
            for (int i = 0; i < characterStats.Length; i++)
            {
                //remove points from level temp and add points to bonus stats
                if (bonusStats < 6 && characterStats[i].levelTempStatValue > 0)
                {
                    if (GUI.Button(new Rect(12.25f * screen.x, 4 * screen.y + (i * 0.5f * screen.y), 0.5f * screen.x, 0.5f * screen.y), "-"))
                    {

                        bonusStats++;
                        characterStats[i].levelTempStatValue--;
                    }
                }
                //type
                //display total stats and stat name
                GUI.Box(new Rect(12.75f * screen.x, 4 * screen.y + (i * 0.5f * screen.y), 2 * screen.x, 0.5f * screen.y), statName[i] + ": " + (characterStats[i].statValue + characterStats[i].tempStatValue + characterStats[i].levelTempStatValue));
                //+
                //if bonus stats are above 0
                if (bonusStats > 0)
                {
                    //remove points from bonus stats and add points to level temp
                    if (GUI.Button(new Rect(14.75f * screen.x, 4 * screen.y + (i * 0.5f * screen.y), 0.5f * screen.x, 0.5f * screen.y), "+"))
                    {
                        bonusStats--;
                        characterStats[i].levelTempStatValue++;
                    }
                }
            }

        }
        #endregion
        #region Save and Play
        // display button if named/Class/Race/Points
        if (characterName != "" && classDropDisplay != "Select Class" && raceDropDisplay != "Select Race" && bonusStats == 0)
        {
            //GUI Button called Save and Play 
            if (GUI.Button(new Rect(7f * screen.x, 7.5f * screen.y, 2f * screen.x, 0.5f * screen.y), "Save and Play"))
            {
                //this button will run the save function 
                SaveCharacter();
                //and also load into the game level
                SceneManager.LoadScene(2);
            }
        }
        #endregion
    }
   public string SaveData()
   {
        string temp = 
            skinIndex+"\n" + 
            mouthIndex + "\n" +
            eyesIndex + "\n"+ 
            hairIndex + "\n" + 
            clothesIndex + "\n" + 
            armourIndex + "\n"+
            helmIndex+"\n"+ 
            characterName+"\n"+ 
            characterClass + "\n" + 
            characterRace;
        //you can use this one
        for (int i = 0; i < characterStats.Length; i++)
        {
            temp = temp.Insert(temp.Length, "\n" + (characterStats[i].statValue + characterStats[i].tempStatValue + characterStats[i].levelTempStatValue));
        }
        ////or this one 
        //for (int i = 0; i < characterStats.Length; i++)
        //{
        //    temp += "\n" + (characterStats[i].statValue + characterStats[i].tempStatValue + characterStats[i].levelTempStatValue);
        //}
        
        // it will need this regardless 
        return temp;
   }
    void SaveCharacter()
    {
        string path = Application.persistentDataPath + "/SaveSlot1";

        #region Text Saving
        textSaving.CharacterSaveSlot(path, SaveData());
        #endregion
        #region PlayerPrefs
        ////SetInt for skins
        //PlayerPrefs.SetInt("SkinIndex", skinIndex);
        //PlayerPrefs.SetInt("HairIndex", mouthIndex);
        //PlayerPrefs.SetInt("MouthIndex", hairIndex);
        //PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        //PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        //PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        //PlayerPrefs.SetInt("HelmIndex", helmIndex);
        ////SetString CharacterName, class, race
        //PlayerPrefs.SetString("CharacterName", characterName);
        //PlayerPrefs.SetString("CharacterClass", characterClass.ToString());
        //PlayerPrefs.SetString("CharacterRace", characterRace.ToString());
        ////int loop stats
        //for (int i = 0; i < characterStats.Length; i++)
        //{
        //    PlayerPrefs.SetInt(characterStats[i].name, (characterStats[i].statValue + characterStats[i].tempStatValue+ characterStats[i].levelTempStatValue));
        //}
        #endregion
    }
    #region PlayerPrefs Load Example
    //void Load()
    //{
    //    //if the registry contains a file with the string name of CharacterName
    //    if (PlayerPrefs.HasKey("CharacterName"))
    //    {
    //        //Set the character name to the registry file data from the file CharacterName
    //        characterName = PlayerPrefs.GetString("CharacterName");
    //    }
    //    else 
    //    {
    //        Debug.LogWarning("OH NO! NO SAVE INFO IS STORED! AHHHHHH");
    //    }
    //}
    #endregion
}
