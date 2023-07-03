using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSpin : MonoBehaviour
{
    public float speed = 50;

    void Update()
    {
        //Simply keep rotating the blade sprite
        this.transform.Rotate(new Vector3(0,0,speed) * Time.deltaTime);
    }
}
