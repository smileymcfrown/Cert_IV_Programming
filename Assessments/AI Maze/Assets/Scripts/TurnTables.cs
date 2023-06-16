using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTables : MonoBehaviour
{
    public float speed;
    public bool antiClockwise;
    
    // Start is called before the first frame update
    void Start()
    {
        if (antiClockwise)
        {
            speed = speed * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,speed * Time.deltaTime,0);
       
    }
}
