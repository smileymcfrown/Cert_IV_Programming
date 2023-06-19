using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    [AddComponentMenu("Game System RPG/NPC/Dialogue/Base Dont attach to NPC")]
    public class DialogueBase : MonoBehaviour
    {
        [Header("Generic Dialogue Base Variables")]

        //boolean to toggle if we can see a characters dialogue box
        public bool showDlg;
        //name of this specific NPC
        public string npcName;
        //array for text for our dialogue
        public string[] dlgText;
        //index for our current line of dialogue
        public int currentLineIndex;
        //screen Scale will hold our x and y float values
        public Vector2 screenScale;

        public void OpenDialogue()
        {
            //set show DLG to true
            showDlg = true;
            //stop players movement and mouselook
            GameManager.Instance.gameState = GameState.AnyOtherScreen;
            //set index to 0
            currentLineIndex = 0;
            //set the 16:9 screen shiz just incase we need it 
            screenScale.x = Screen.width / 16;
            screenScale.y = Screen.height / 9;
        }
        public void CloseDialogue()
        {
            //set show DLG to false
            showDlg = false;
            //allow player to have movement and mouselook
            GameManager.Instance.gameState = GameState.Alive;
            //set index to 0
            currentLineIndex = 0;
        }
    }
}
