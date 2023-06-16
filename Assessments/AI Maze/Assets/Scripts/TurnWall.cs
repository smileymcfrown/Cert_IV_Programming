using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnWall : MonoBehaviour
{
    public float waitTime = 10f;
    public float duration = 20f;
    public float speed = 10f;
    public float swingAmount = 90f;

    private float startAngle;
    private float endAngle;
    private bool isOpen = true;
    private Quaternion startPos;
    
    void Start()
    {
        startAngle = transform.localEulerAngles.y;
        endAngle = startAngle + swingAmount;
        startPos = transform.rotation;
        StartCoroutine(Swing());
        
    }

    IEnumerator Swing()
    {
        while (true)
        {
            Quaternion rot = Quaternion.Euler(0, 90, 0);

            if (isOpen)
            {
                if (transform.rotation == startPos * rot)
                {
                    isOpen = !isOpen;
                    yield return new WaitForSeconds(waitTime);
                }
                else
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, startPos * rot, speed * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                if (transform.rotation == startPos)
                {
                    isOpen = !isOpen;
                    yield return new WaitForSeconds(waitTime);
                }
                else
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, startPos, speed * Time.deltaTime);
                    yield return null;
                }
            }
        }

    }
}
