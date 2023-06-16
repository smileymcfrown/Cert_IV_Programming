using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 leftPos, rightPos;
    
    private Vector3 target;
    private bool isLeft;
    
    void Start()
    {
        target = leftPos;
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            isLeft = !isLeft;
                
            if (isLeft) { target = leftPos; }
            else { target = rightPos; }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}
