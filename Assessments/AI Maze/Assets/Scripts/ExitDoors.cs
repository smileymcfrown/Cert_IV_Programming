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
    private bool doorOpen;
    private bool doorOpening;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger");
        agentState = other.gameObject.GetComponent<AgentState>();
        if (doorOpen)
        {
            agentState.waypoints.Add(outPos);
            Debug.Log(agentState.waypoints[agentState.waypoints.Count -1].gameObject.name);
            agentState.doorOpen = true;
        }
        else
        {
            int door;
            if (redDoor)
            {
                door = 1;
            }
            else
            {
                door = 0;
            }

            if (agentState.hasKey == door || agentState.hasKey == 2 || agentState.triedDoor || doorOpening)
            {
                if (doorOpening)
                {
                    agentState.waypoints.Add(outPos);
                    Debug.Log(agentState.waypoints[agentState.waypoints.Count -1].gameObject.name);
                    agentState.doorOpen = true;
                }
                else
                {
                    StartCoroutine(Open());
                }
            }
        }
    }

    IEnumerator Open()
    {
        doorOpening = true;
        yield return new WaitForSeconds(2f);
        Vector3 leftTarget = leftOpenPos;
        Vector3 rightTarget = rightOpenPos;

        while  (Vector3.Distance(rightDoor.position, rightTarget) > 0.001f)
        {
                rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightTarget, speed * Time.deltaTime);
                leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftTarget, speed * Time.deltaTime);
                yield return null;
        }

        doorOpen = true;
        agentState.waypoints.Add(outPos);
        Debug.Log(agentState.waypoints[agentState.waypoints.Count -1].gameObject.name);
        agentState.doorOpen = true;

    }
}
