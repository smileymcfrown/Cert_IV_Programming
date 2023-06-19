using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is part of the family of scripts Player
namespace Player
{
    //this script can be found in the Component section Player
    [AddComponentMenu("Game System RPG/Player/Interact")]
    public class Interact : MonoBehaviour
    {

        void Update()
        {
            //if our interact key is pressed (E)
            if (Input.GetKeyDown(KeyCode.E))
            {
                //RAY - A ray is an infinite line starting at origin and going in some direction.
                //create a ray
                Ray interact;
                //this ray is shooting out from the main cameras screen point center of screen
                interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                //RAYCASTHIT - Structure used to get information back from a raycast
                //create hit info
                RaycastHit hitInfo;
                //RAYCASTING - Casts a ray, from point origin, in direction direction, of length maxDistance, against all colliders in the Scene.
                //if this physics raycast hits something within 10 units
                if (Physics.Raycast(interact, out hitInfo, 10))
                {
                    #region NPC tag
                    //if that hits info is tagged NPC
                    if (hitInfo.collider.tag == "NPC")
                    {
                        //Debug that we hit a NPC  
                        Debug.Log("Our Interact ray hit an NPC");
                        //if the raycast info that is passed back to this script via hitting a collider
                        //finds a component called Dialogue on the game object
                        if (hitInfo.collider.GetComponent<NPC.DialogueBase>())
                        {
                            //then from that component run Open Dialogue
                            hitInfo.collider.GetComponent<NPC.DialogueBase>().OpenDialogue();
                        }                     
                    }
                    #endregion
                    #region Item tag
                    //if that hits info is tagged NPC
                    if (hitInfo.collider.CompareTag("Item"))
                    {
                        //Debug that we hit a NPC  
                        Debug.Log("Our Interact ray hit an Item");
                    }
                    #endregion
                    #region Chest tag
                    //if that hits info is tagged NPC
                    if (hitInfo.collider.CompareTag("Chest"))
                    {
                        //Debug that we hit a NPC  
                        Debug.Log("Our Interact ray hit a Chest");
                    }
                    #endregion
                }
            }
        }
    }

}