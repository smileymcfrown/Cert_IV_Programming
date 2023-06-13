using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnWall : MonoBehaviour
{
    public float waitTime = 10f;
    public float duration = 20f;
    public float swingAmount = 90f;

    private float startAngle;
    private float endAngle;
    private bool isOpen = true;
    private Quaternion startPos;
    void Start()
    {
        startAngle = transform.localEulerAngles.y;
        endAngle = startAngle + swingAmount;
        startPos = Quaternion.identity;
        StartCoroutine(Swing());
        
    }

    private void Update()
    {
        //transform.Rotate(new Vector3(0, -(Time.deltaTime * speed),0));
    }

    IEnumerator Swing()
    {
        //Debug.Log("First" + isOpen);
        //Debug.Log("Start coroutine");
        //float targetAngle = endAngle;
        float time = 0;
        Vector3 eulerAngles;
        
        if (isOpen) { swingAmount = swingAmount * -1; }
        else { swingAmount = swingAmount * -1; }
        Quaternion rotAmount = Quaternion.Euler(0,swingAmount,0);
        Quaternion lastRot = Quaternion.identity;
        while (time < duration)
        {
            Quaternion newRot = Quaternion.Lerp(startPos, rotAmount,time / duration);
            transform.rotation *= newRot * Quaternion.Inverse(lastRot);
            //transform.Rotate(Vector3.up,Mathf.Lerp(startAngle,endAngle,duration));
            //eulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.Lerp(startAngle,endAngle,duration), transform.localEulerAngles.z);
            //transform.localEulerAngles = eulerAngles;
            time += Time.deltaTime;
            yield return null;
            /*
            if (isOpen)
            {
                
                if (transform.localEulerAngles.y <= endAngle)
                {
                    //transform.Rotate(new Vector3(0, (Time.deltaTime * speed), 0));
                    //transform.rotation = Quaternion.AngleAxis(90,Vector3.up);
                    transform.Rotate(Vector3.up,);
                    yield return null;
                }
                else
                {
                    isOpen = !isOpen;
                    yield return new WaitForSeconds(waitTime);
                }
                
                
                
            }
            else
            {
                if (transform.localEulerAngles.y >= startAngle && transform.localEulerAngles.y < 91f)
                {
                    //Debug.Log(transform.localEulerAngles.y + " StartAngle: " + startAngle);
                    transform.Rotate(new Vector3(0, -(Time.deltaTime * duration), 0));
                    yield return null;
                }
                else
                {
                    isOpen = !isOpen;
                    yield return new WaitForSeconds(waitTime);
                }
            }
            */
        }

        //eulerAngles = new Vector3(transform.localEulerAngles.x,endAngle,transform.localEulerAngles.z);
        //transform.localEulerAngles = eulerAngles;
        
        isOpen = !isOpen;
        
        yield return new WaitForSeconds(waitTime);

        /*
        while (true)
        {
            if (transform.localEulerAngles.y != (targetAngle - 0.01f))
            {
                //isOpen = !isOpen;
                
                //if (isOpen) { targetAngle = startAngle; }
                //else { targetAngle = endAngle; }

                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                Debug.Log(transform.localEulerAngles);
                while (transform.localEulerAngles.y <= targetAngle)
                {
                    if (isOpen)
                    {
                        transform.Rotate(new Vector3(0, -(Time.deltaTime * speed),
                            0)); //Vector3.left * Time.deltaTime * speed);
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0,Time.deltaTime * speed,0));
                    }
                    // transform.localEulerAngles = new Vector3(0, Mathf.LerpAngle(transform.localEulerAngles.y, targetAngle, speed * Time.deltaTime), 0);
                    yield return null;
                }
            }
        }
        */
    }
}
