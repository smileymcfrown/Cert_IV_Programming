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

    private void Update()
    {
        //transform.Rotate(new Vector3(0, -(Time.deltaTime * speed),0));
    }

    IEnumerator Swing()
    {

        float time = 0;
        Vector3 eulerAngles;


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

        //eulerAngles = new Vector3(transform.localEulerAngles.x,endAngle,transform.localEulerAngles.z);
        //transform.localEulerAngles = eulerAngles;
        
        isOpen = !isOpen;
        
        yield return new WaitForSeconds(waitTime);

    }
}
