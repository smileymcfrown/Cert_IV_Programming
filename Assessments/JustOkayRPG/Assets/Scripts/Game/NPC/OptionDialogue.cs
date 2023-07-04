using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is part of the family of scripts NPC
namespace NPC
{
    //this script can be found in the Component section under the option Game System RPG/NPC/Dialogue

    [AddComponentMenu("Game System RPG/NPC/Dialogue/Option")]
    public class OptionDialogue : DialogueBase
    {       
        public int questionLineIndex;      
       
        private void OnGUI()
        {
            //if our dialogue can be seen on screen
            if (showDlg)
            {
                //the dialogue box takes up the whole bottom 3rd of the screen and displays the NPC's name and current dialogue line
                GUI.Box(new Rect(0, screenScale.y * 6, Screen.width, screenScale.y * 3), npcName + ": " + dlgText[currentLineIndex]);
                //if not at the end of the dialogue or not at the options part
                if (currentLineIndex < dlgText.Length - 1 && currentLineIndex != questionLineIndex)
                {
                    //next button allows us to skip forward to the next line of dialogue
                    if (GUI.Button(new Rect(screenScale.x * 15, screenScale.y * 8.5f, screenScale.x * 1, screenScale.y * 0.5f), "Next"))
                    {
                        //incrementing currentLineIndex by 1 so that we go to next line
                        currentLineIndex++;
                    }
                }
                //else if we are at options
                else if (currentLineIndex == questionLineIndex)
                {
                    //Accept button allows us to skip forward to the next line of dialogue
                    if (GUI.Button(new Rect(screenScale.x * 14, screenScale.y * 8.5f, screenScale.x * 1, screenScale.y * 0.5f), "Accept"))
                    {
                        currentLineIndex++;

                    }
                    //Decline button skips us to the end of the characters dialogue 
                    if (GUI.Button(new Rect(screenScale.x * 15, screenScale.y * 8.5f, screenScale.x * 1, screenScale.y * 0.5f), "Decline"))
                    {
                        currentLineIndex = dlgText.Length - 1;

                    }
                }
                //else we are at the end
                else
                {
                    //the Bye button allows up to end our dialogue
                    if (GUI.Button(new Rect(screenScale.x * 15, screenScale.y * 8.5f, screenScale.x * 1, screenScale.y * 0.5f), "Bye."))
                    {
                        //close the dialogue box
                        CloseDialogue();
                    }
                }
            }
           
        }
    }
}