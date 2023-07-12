using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AgentState : MonoBehaviour
{
    private enum State
    {
        Idle,
        MoveTo,
        Dance,
        DoorWait,
        Freedom
        
    }

    private State currentState;
    
    private delegate void OnFindTarget();
    private OnFindTarget onFindTarget;

    //[SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;

    public List<Transform> waypoints = new List<Transform>();
    private int currentWaypoint;
    
    public NavMeshAgent navAgent;
    public Transform waypointsParent;
    public Transform agentsParentObj;
    public float sandSpeed = 3f;
    public float jumpSpeed = 8f;
    public float runSpeed = 10f;

    public int hasKey;
    public bool triedDoor;
    public bool doorOpen;
    
    private float doorDelay;
    private float danceDelay;
    private float idleTime;
    private float idleDelay;

    public Transform destination;
    
    void Awake()
    {
        currentState = State.Idle;
    }

    private void Start()
    {
        //Agents are randomly assigned and equal number of treasures to collect before going to a door as the point
        //They also have a 50% chance of a key being added to the treasure list before they go to the door (so they don't have to go back)

        int x;
        int goalAmt;

        //Divide the treasures between the number of agents
        goalAmt = Goals.goalList.Count / agentsParentObj.childCount;

        Debug.Log("Agent count: " + agentsParentObj.childCount);
        
        //50-50 chance of a key being added to the treasures
        if (Random.Range(0f, 1f) < .5f)
        {
            Debug.Log("Getting Key");
            goalAmt += 1;
            x = Random.Range(0, goalAmt);
        }
        else
        {
            x = -1;
        }
        
        for (int i = 0; i < goalAmt; i++)
        {
            if (x == i)
            {
                Debug.Log(Goals.keyList.Count);
                x = Random.Range(0, Goals.keyList.Count);
                waypoints.Add(Goals.keyList[x]);
                Goals.keyList.RemoveAt(x);
                x = -1;
            }
            else
            {
                int index = Random.Range(0, Goals.goalList.Count);
                waypoints.Add(Goals.goalList[index]);
                Goals.goalList.RemoveAt(index);
            }
            Debug.Log("Object taken: " + waypoints[i].gameObject.name);
            
        }
        // Add an exit as the final way point
        waypoints.Add(Goals.doors[Random.Range(0, Goals.doors.Length)]);

        Debug.Log("Door: " + waypoints[waypoints.Count -1].gameObject.name);

        hasKey = -1;
        currentWaypoint = 0;
        navAgent.SetDestination(waypoints[currentWaypoint].position);
        
        Debug.Log("Array: " + waypoints[currentWaypoint].position + " / SetDestination: " + navAgent.destination);
        
        if (waypoints[currentWaypoint].gameObject.layer == 11)
        {
            onFindTarget = OnFindTreasure;
        }
        else if (waypoints[currentWaypoint].gameObject.layer == 12)
        {
            onFindTarget = OnFindKey;
        }
        else 
        {
            onFindTarget = OnFindDoor;
        }
        currentState = State.MoveTo;
        animator.SetBool("Running", true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.Idle)
        {
            StandIdle();
        }
        else if (currentState == State.MoveTo)
        {
            MoveTo();
        }
        else if (currentState == State.Dance)
        {
            Dance();
        }
        else if (currentState == State.DoorWait)
        {
            DoorWait();
        }
        else if(currentState == State.Freedom)
        {
            Freedom();
        }
        
        if (destination != null)
        {
            destination.position = navAgent.destination;
            //Debug.Log("Update destination" + navAgent.destination + " / Update array: " + waypoints[currentWaypoint].position);
        }
    }
    private void StandIdle()
    {
        animator.SetBool("Running", false);

        if (idleTime == 0)
        {
            animator.SetTrigger("Default");
            idleTime += Time.deltaTime;
        }
        else if (idleTime >= 3f)
        {
            animator.SetBool("StillWaiting", true);
        }

        if (navAgent.isPathStale || navAgent.pathPending)
        {
            navAgent.isStopped = true;
            navAgent.speed = 0;
            navAgent.SetDestination(waypoints[currentWaypoint].position);
        }
        else
        {
            navAgent.SetDestination(waypoints[currentWaypoint].position);
            currentState = State.MoveTo;
            animator.SetBool("Running", true);
            idleTime = 0;
        }
    }

    private void Freedom()
    {
        Debug.Log("Freedom!!!");
        //animator.SetBool("Running",false);
        navAgent.isStopped = true;
        navAgent.speed = 0;
        if (idleTime == 0)
        {
            animator.SetTrigger("FallFlat");
            idleTime = 1;
        }
    }

    private void DoorWait()
    {
        Debug.Log("DoorWait()");
        animator.SetBool("Running",false);
        
        if (idleTime == 0) 
        {
            animator.SetTrigger("Default");
            idleTime += Time.deltaTime;
        }
        else if (idleTime >= 3f)
        {
            animator.SetBool("StillWaiting",true);
        }
        else if (doorOpen)
        {
            Debug.Log(waypoints[waypoints.Count -1].gameObject.name);
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
            navAgent.SetDestination(waypoints[currentWaypoint].position);
            onFindTarget = Freedom;
            currentState = State.MoveTo;
            animator.SetBool("Running", true);
            idleTime = 0;
        }
    }
    

    private void MoveTo()
    {
        if (navAgent.isPathStale || navAgent.pathPending)
        {
            currentState = State.Idle;
            
        }
        else
        {
            Debug.Log("In MoveTo() - Target: " + waypoints[currentWaypoint].gameObject.name + "Pos: " + waypoints[currentWaypoint].position);
            Debug.Log("Sphere: " + destination.position + " / navAgent Destination: " + navAgent.destination);
            if (!navAgent.pathPending)
            {
                //This should not be here! It should not change!
                //navAgent.SetDestination(waypoints[currentWaypoint].position);

                Debug.Log("RemainingDistance: " + navAgent.remainingDistance + " StoppingDistance: " + navAgent.stoppingDistance);

                if (navAgent.remainingDistance <=
                    navAgent.stoppingDistance + 0.00001f) // && navAgent.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    navAgent.isStopped = true;
                    navAgent.speed = 0;
                    //navAgent.destination = transform.position;
                    onFindTarget();

                }
                else
                {
                    navAgent.isStopped = false;
                    
                    NavMeshHit navHit;
                    navAgent.SamplePathPosition(-1, 0, out navHit);
                    if (navHit.mask == 8)
                    {
                        navAgent.speed = sandSpeed;
                    }
                    else if (navAgent.isOnOffMeshLink)
                    {
                        navAgent.speed = jumpSpeed;
                        animator.SetTrigger("Jump");
                    }
                    else
                    {
                        navAgent.speed = runSpeed;
                    }
                }
            }
        }
        /*
        if (navAgent.destination != waypoints[currentWaypoint].position)
        {
            navAgent.SetDestination(waypoints[currentWaypoint].position);
        }
        */
    }

    private void OnFindTreasure()
    {
        Debug.Log("In OnFindTreasure()");
        //Destroy the treasure
        waypoints[currentWaypoint].transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        
        //Switch state to Dance and turn agent towards the camera
        currentState = State.Dance;
        danceDelay = 0f;
        transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        animator.SetTrigger("Dance1");
        
        //animator.SetBool("Running", false);
    }

    private void OnFindKey()
    {
        Debug.Log("In OnFindKey()");

        // Add code to make something happen when the key is found like it getting bigger before disappearing
        Debug.Log(waypoints[currentWaypoint].name + " : " + waypoints[currentWaypoint].transform.gameObject.GetComponent<MeshRenderer>().enabled); 
        waypoints[currentWaypoint].transform.gameObject.GetComponent<MeshRenderer>().enabled = false;

        currentState = State.Dance;
        danceDelay = 0f;
        animator.SetTrigger("Dance1");
       // animator.SetBool("Running", false);
       if (!triedDoor)
       {
           switch (waypoints[currentWaypoint].transform.gameObject.name)
           {
               case "Green Key":
                   hasKey = 0;
                   break;
               case "Red Key":
                   hasKey = 1;
                   break;
               case "Gold Key":
                   hasKey = 2;
                   break;
           }
       }
       else
       {
           switch (waypoints[currentWaypoint].transform.gameObject.name)
           {
               case "Green Key":
                   hasKey = 0;
                   waypoints.Add(Goals.doors[hasKey]);
                   break;
               case "Red Key":
                   hasKey = 1;
                   waypoints.Add(Goals.doors[hasKey]);
                   break;
               case "Gold Key":
                   hasKey = 2;
                   waypoints.Add(Goals.doors[Random.Range(0,2)]);
                   break;
           }
       }

       //Already in Dance()
       //onFindTarget = OnFindDoor;
       //currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
       //navAgent.SetDestination(waypoints[currentWaypoint].position);
    }

    private void OnFindDoor()
    {
        if (hasKey == -1)
        {
            Debug.Log("No Key!");
            if (Goals.keyList.Count != 0)
            {
                Debug.Log("KeyList: " + Goals.keyList.Count);
                int rndKey = Random.Range(0, Goals.keyList.Count - 1);
                Debug.Log("Random Key: " + rndKey);
                waypoints.Add(Goals.keyList[rndKey]);
                Goals.keyList.RemoveAt(rndKey);
                
                currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
                navAgent.SetDestination(waypoints[currentWaypoint].position);
                currentState = State.MoveTo;
                animator.SetBool("Running", true);
                onFindTarget = OnFindKey;
                
                triedDoor = true;
                
            }
            else {Debug.Log(Goals.keyList.Count);}
        } 
        else if (waypoints[currentWaypoint].transform.gameObject.name == "Green Doors" && hasKey == 1)
        {
            waypoints.Add(Goals.doors[1]);
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
            navAgent.SetDestination(waypoints[currentWaypoint].position);
            currentState = State.MoveTo;
            animator.SetBool("Running", true);
            onFindTarget = OnFindDoor;

        }
        /*else if (!doorOpen)
        {
            currentState = State.Idle;
        }

        //animator.SetBool("Running", false);
        if (doorDelay < 1.5f)
        
            doorDelay += Time.deltaTime;
            */
        
        else
        {
            currentState = State.DoorWait;
        }
    }
    
    private void Dance()
    {
        //Debug.Log("In Dance()");
        if (danceDelay < 5.05f) danceDelay += Time.deltaTime;
        else
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
            if (waypoints[currentWaypoint].gameObject.layer == 11)
            {
                onFindTarget = OnFindTreasure;
            }
            else if (waypoints[currentWaypoint].gameObject.layer == 12)
            {
                onFindTarget = OnFindKey;
            }
            else 
            {
                onFindTarget = OnFindDoor;
            }
            Debug.Log(waypoints[currentWaypoint].gameObject.name + " Pos: " + waypoints[currentWaypoint].position);
            navAgent.SetDestination(waypoints[currentWaypoint].position);
            Debug.Log("Nav Dest: " + navAgent.destination);
            currentState = State.MoveTo;
            animator.SetBool("Running", true);

        }
    }
}
