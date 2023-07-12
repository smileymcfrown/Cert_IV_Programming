using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{
    public static List<Transform> goalList = new List<Transform>();
    public static List<Transform> keyList = new List<Transform>();
    public static int goalTotal;
    public static Transform[] doors;
    
    void Awake()
    {
        goalTotal = 0;
        Transform treasure = transform.Find("Treasure");
        Transform keys = transform.Find("Keys");
        Transform doorObj = transform.Find("Doors");
        
        Debug.Log("Treasures: " + treasure.childCount);
        Debug.Log("Keys: " + keys.childCount);

        //if(doors == null)
        //{
            doors = new Transform[doorObj.childCount];
        //}
        
        Debug.Log("Doors: " + doors.Length + " / Count: " + doorObj.childCount);
        
        
        for (int i = 0; i < treasure.childCount; i++)
        {
            goalList.Add(treasure.GetChild(i));
            goalTotal++;
        }
        
        Debug.Log("Position 0: " + goalList[0]);
        
        for (int i = 0; i < keys.childCount; i++)
        {
            keyList.Add(keys.GetChild(i));
        }
        
        for (int i = 0; i < doorObj.childCount; i++)
        {
            doors[i] = doorObj.GetChild(i);
        }
    }
}
