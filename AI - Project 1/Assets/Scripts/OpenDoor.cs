using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public int WaitTime = 3;
    public int Speed = 2;
    public Vector3 PositionDelta = Vector3.zero;

    private Vector3 _closedPosition;
    private Vector3 _openPosition;

    
// Start is called before the first frame update
    void Start()
    {
        _closedPosition = transform.position;
        _openPosition = _closedPosition + PositionDelta;

        Debug.Log("Before Coroutine");
        StartCoroutine(OpenClose());
        Debug.Log("After Coroutine");
    }

    IEnumerator OpenClose()
    {
       Vector3 goal = _openPosition;
       bool isOpen = false;
       
       while(true)
       {
            if(Vector3.Distance(transform.position, goal) < 0.1f)
            {
                isOpen = !isOpen;
                if(isOpen) { goal = _closedPosition; } else { goal = _openPosition; }
                
                yield return new WaitForSeconds(WaitTime);
            }
            
       }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
