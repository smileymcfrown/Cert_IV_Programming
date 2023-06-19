using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is part of the family of scripts NPC
namespace NPC 
{
    //this script can be found in the Component section
    [AddComponentMenu("Game System RPG/NPC/Dialogue/Approval")]
    //Inherit from DialogueBase
    public class ApprovalDialogue : DialogueBase
    {        
        // NPC gives different responses depending on an approval system
        /*
         Approval (can be an int or enum) has at least 3 tiers of response:
            - Dislike  -1
            - Neutral   0
            - Like      1
        */
        [Space(25)]
        [Header("Approval Specific Variables")]
        public int approvalValue = 0;
        public int questionLineIndex = 0;
        //3 extra arrays of strings one for each approval type
        public string[] dislikeText = new string[5];
        public string[] neutralText = new string[5];
        public string[] likeText = new string[5];
        /*
         Dialogue changes based on Approval rating

        Approval changes based on player interactions
             - have a way to ask a yes or no question
         */
        private void Start()
        {
            ChangeDisplayText();
        }
        void ChangeDisplayText()
        {
            approvalValue = Mathf.Clamp(approvalValue, -1, 1);
            if (approvalValue == 1)
            {
                dlgText = likeText;
            }
            else if (approvalValue == -1)
            {
                dlgText = dislikeText;
            }
            else 
            {
                dlgText = neutralText;
            }
        }
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
                        approvalValue++;
                        ChangeDisplayText();    
                    }
                    //Decline button skips us to the end of the characters dialogue 
                    if (GUI.Button(new Rect(screenScale.x * 15, screenScale.y * 8.5f, screenScale.x * 1, screenScale.y * 0.5f), "Decline"))
                    {
                        currentLineIndex = dlgText.Length - 1;
                       approvalValue--;
                        ChangeDisplayText();                       
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
