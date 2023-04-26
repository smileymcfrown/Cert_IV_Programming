using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity
[AddComponentMenu("3D Game/ NPC Scripts/ Dialogue"]
//13 errors

private class DialogScript1 : MonoBehaviour
{
    [Header("References")]
    public string isDialogOpen;
    public string dialogeText;
    public int dialogIndex; // index of current line of dialogue
    public string npcName;
    public Vector2 screen;
    int screen.y = Screen.height / 9;
    int screen.x = Screen.width / 16;

    private void onGUI()
    {

        if (isDialogOpen)
        {
            GUI.Box(new Rect(0, screen.y * 6 ,Screen.width ,screen.y * 3), npcName+ ": " + dialogText[dialogIndex]);

            if (dialogIndex < dialogText.length - 1)
            {
                if (GUI.Button(new Rect(screen.x * 15, screen.y * 8.5f, screen.x, screen.y * 0.5f), "Next"))
                {
                    dialogIndex++;
                }
            }

            else if
            {
                if (GUI.Button(new Rect(screen.x * 15, screen.y * 8.5f, screen.x, screen.y * 0.5f), "Bye"))
                {

                    isDialogOpen = false;

                    dialogIndex = 0;

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;

                }
            }
        }
    }


    void Start()
    {


    void Update()
    {
        
    }
}
