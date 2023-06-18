using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{
    public static List<Transform> goalList = new List<Transform>();
    public static List<Transform> keyList = new List<Transform>();

    public static Transform[] doors;

    
    // Start is called before the first frame update
    void Start()
    {
        Transform treasure = transform.Find("Treasure");
        Transform keys = transform.Find("Keys");
        Transform doorObj = transform.Find("Doors");
        doors = new Transform[doorObj.childCount];
        
        Debug.Log("Treasures: " + treasure.childCount);
       
        Debug.Log("Keys: " + keys.childCount);
        Debug.Log("Doors: " + doors.Length + " / Count: " + doorObj.childCount);
        
        for (int i = 0; i < treasure.childCount; i++)
        {
            goalList.Add(treasure.GetChild(i));
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
