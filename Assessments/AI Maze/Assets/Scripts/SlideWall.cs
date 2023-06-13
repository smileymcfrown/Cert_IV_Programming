using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideWall : MonoBehaviour
{
    public float waitTime = 20f;
    public float speed = 7f;
    public float startDelay = 10f; 

    public Vector3 moveAmount = Vector3.zero;

    private Vector3 closedPos;
    private Vector3 openPos;
    
    // Start is called before the first frame update
    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + moveAmount;
        StartCoroutine(Slide());
    }

    IEnumerator Slide()
    {
        yield return new WaitForSeconds(startDelay);
        
        Vector3 target = openPos;
        bool isOpen = false;

        while (true)
        {
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                isOpen = !isOpen;
                
                if (isOpen) { target = closedPos; }
                else { target = openPos; }

                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
