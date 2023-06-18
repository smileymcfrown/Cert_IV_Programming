using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExitDoors : MonoBehaviour
{
    public Transform key;
    public Transform outPos;
    public Vector3 leftOpenPos;
    public Vector3 rightOpenPos;
    public float speed = 5f;
    public bool redDoor;
    public Transform leftDoor;
    public Transform rightDoor;
    
    private AgentState agentState;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger");
        int door;
        if (redDoor)
        {
            door = 1;
        }
        else
        {
            door = 0;
        }
        
        agentState = other.gameObject.GetComponent<AgentState>();

        if (agentState.hasKey == door || agentState.hasKey == 2)
        {
            StartCoroutine(Open());
        }
    }

    IEnumerator Open()
    {
        yield return new WaitForSeconds(2f);
        Vector3 leftTarget = leftOpenPos;
        Vector3 rightTarget = rightOpenPos;

        while  (Vector3.Distance(rightDoor.position, rightTarget) > 0.001f)
        {
                rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightTarget, speed * Time.deltaTime);
                leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftTarget, speed * Time.deltaTime);
                yield return null;
        }
        
        agentState.waypoints.Add(outPos);
        Debug.Log(agentState.waypoints[agentState.waypoints.Count -1].gameObject.name);
        agentState.doorOpen = true;

    }
}
