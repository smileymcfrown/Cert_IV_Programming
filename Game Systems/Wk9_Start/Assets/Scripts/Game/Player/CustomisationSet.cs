using System.Collections.Generic;
using UnityEngine;
//you will need to change Scenes
using UnityEngine.SceneManagement;
public class CustomisationSet : Stats
{
    #region Variables   
    [Header("Character Name")]
    public string characterName;
    [Header("Texture List")]
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    [Header("Index")]
    public int skinIndex;
    public int mouthIndex, eyesIndex, hairIndex, clothesIndex, armourIndex, helmIndex;
    [Header("Renderer")]
    public Renderer character;
    public Renderer helm;
    [Header("Max Index")]
    public int skinMax;
    public int mouthMax, eyesMax, hairMax, clothesMax, armourMax;

    public string[] materialNames = new string[7] { "Skin", "Mouth", "Eyes", "Hair", "Clothes", "Armour", "Helm" };
    public Vector2 screen;

    //index numbers for our current skin, hair, mouth, eyes, clothes and armour textures
    //renderer for our character mesh so we can reference a material list
    //max amount of skin, hair, mouth, eyes, clothes and armour textures that our lists are filling with
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
        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            mouth.Add(temp);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            eyes.Add(temp);
        }
        for (int i = 0; i < hairMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            hair.Add(temp);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            clothes.Add(temp);
        }
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            armour.Add(temp);
        }

        #endregion
        //connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer 
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
    //we need variables that exist only within this function

    void SetTexture(string type, int dir)
    {
        //these are ints index numbers, max numbers, material index and Texture2D array of textures and our renderer    
        int index = 0, max = 0, matIndex = 0;
        Renderer curRend = new Renderer();
        Texture2D[] textures = new Texture2D[0];

        #region Switch Material
        //inside a switch statement that is swapped by the string name of our material   
        switch (type)
        {
            #region Skin
            case "Skin":
                index = skinIndex;
                //index is the same as our skin index
                max = skinMax;
                //max is the same as our skin max
                textures = skin.ToArray();
                //textures is our skin list .ToArray()
                matIndex = 1;
                //material index element number
                curRend = character;
                //current renderer is the mesh renderer that we are getting the materials from
                break;
            //end case
            #endregion
            #region Mouth
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 1;
                curRend = character;
                break;
            #endregion
            #region Eyes
            case "Eyes":
                index = eyesIndex;
                max = eyesMax;
                textures = eyes.ToArray();
                matIndex = 1;
                curRend = character;
                break;
            #endregion
            #region Hair
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 1;
                curRend = character;
                break;
            #endregion
            #region Clothes
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 1;
                curRend = character;
                break;
            #endregion
            #region Helm
            case "Helm":
                index = helmIndex;
                max = armourMax;
                //textures = helm.ToArray();
                matIndex = 1;
                curRend = character;
                break;
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
            index = int.MaxValue - 1;
        }
        if (index > int.MaxValue - 1)
        {
            index = 0;
        }
        //Material array is equal to our characters material list
        Material[] mat = curRend.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        //our characters materials are equal to the material array
        #endregion
        //create another switch that is goverened by the same string name of our material
        #region Set Material Switch
        switch (type)
        {
            //case skin
            //skin index equals our index
            //break
            case "Skin":
            skinIndex = index;
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

    public override void Update()
    {

    }

    private void OnGUI()
    {
        screen.x = Screen.width / 16;
        screen.y = Screen.height / 9;
        for (int i = 0; i < materialNames.Length; i++)
        {
            if(GUI.Button(new Rect(0.25f*screen.x,2.5f*screen.y+(i*.5f*screen.y),0.5f * screen.x,0.5f * screen.y),"<"))
            {
                SetTexture(materialNames[i], -1);
            }

            GUI.Box(new Rect(0.75f * screen.x, 2.5f * screen.y + (i * .5f * screen.y), 1.5f * screen.x, 0.5f * screen.y), materialNames[i]);

            if (GUI.Button(new Rect(2.25f * screen.x, 2.5f * screen.y + (i * .5f * screen.y), 0.5f * screen.x, 0.5f * screen.y), ">"))
            {
                SetTexture(materialNames[i], 1);
            }
        }
    }
}
