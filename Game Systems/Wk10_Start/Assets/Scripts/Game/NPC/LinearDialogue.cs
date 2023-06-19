using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NPC
{
    [AddComponentMenu("Game System RPG/NPC/Dialogue/Linear")]
    public class LinearDialogue : DialogueBase
    {
       
        private void OnGUI()
        {
            //if our dialogue can be seen on screen
            if (showDlg)
            {
                //the dialogue box takes up the whole bottom 3rd of the screen and displays the NPC's name and current dialogue line
                GUI.Box(new Rect(0, screenScale.y * 6, Screen.width, screenScale.y * 3), npcName + ": " + dlgText[currentLineIndex]);
                //if not at the end of the dialogue 
                if (currentLineIndex < dlgText.Length-1)
                {
                    //next button allows us to skip forward to the next line of dialogue
                    if (GUI.Button(new Rect(screenScale.x * 15, screenScale.y * 8.5f, screenScale.x * 1, screenScale.y * 0.5f),"Next"))
                    {
                        //incrementing currentLineIndex by 1 so that we go to next line
                        currentLineIndex++;                       
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

