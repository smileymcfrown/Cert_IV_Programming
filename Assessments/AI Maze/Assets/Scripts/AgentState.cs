using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AgentState : MonoBehaviour
{
    private enum State
    {
        Idle,
        MoveTo,
        Dance,
        
    }

    private State currentState;

    private delegate void OnFindTarget();
    private OnFindTarget onFindTarget;

    //[SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;

    public Transform[] waypoints;
    private int currentWaypoint;
    
    public NavMeshAgent navAgent;
    public Transform waypointsParent;
    public float walkSpeed = 7f;
    public float runSpeed = 10f;

    private float doorDelay;
    private float danceDelay;

    public Transform destination;
    
    void Awake()
    {
        currentState = State.Idle;
    }

    private void Start()
    {
        waypoints = new Transform[waypointsParent.childCount];
        for (int i = 0; i< waypointsParent.childCount; i++)
        {
            waypoints[i] = waypointsParent.GetChild(i);
        }

        currentWaypoint = 0;
        navAgent.SetDestination(waypoints[currentWaypoint].position);
        Debug.Log("Array: " + waypoints[currentWaypoint].position + " / SetDestination: " + navAgent.destination);
        onFindTarget = OnFindTreasure;
        currentState = State.MoveTo;
        animator.SetBool("Running", true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.MoveTo)
        {
            MoveTo();
        }
        else if (currentState == State.Dance)
        {
            Dance();
        }

        if (destination != null)
        {
            destination.position = navAgent.destination;
            Debug.Log("Update destination" + navAgent.destination + " / Update array: " + waypoints[currentWaypoint].position);
        }
    }

    private void MoveTo()
    {
        Debug.Log("In MoveTo() - Target: " + waypoints[currentWaypoint].gameObject.name + "Pos: " + waypoints[currentWaypoint].position);
        Debug.Log("Sphere: " + destination.position +" / navAgent Destination: " + navAgent.destination);
        if (!navAgent.pathPending)
        {
            //This should not be here! It should not change!
            //navAgent.SetDestination(waypoints[currentWaypoint].position);
            
            Debug.Log("RemainingDistance: " + navAgent.remainingDistance + " StoppingDistance: " + navAgent.stoppingDistance);
            
            if (navAgent.remainingDistance <= navAgent.stoppingDistance + 0.00001f) // && navAgent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                navAgent.isStopped = true;
                navAgent.speed = 0;
                //navAgent.destination = transform.position;
                onFindTarget();

            }
            else
            {
                navAgent.isStopped = false;
                navAgent.speed = runSpeed;
                if (navAgent.isOnOffMeshLink)
                {
                    animator.SetTrigger("Jump");
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
        //This if statement is to stop the first object being destroyed immediately... BUT WHY?!
        //Vector3 pos = transform.position - waypoints[currentWaypoint].position;
        //if (pos.sqrMagnitude < 0.5f)
        //{
            waypoints[currentWaypoint].transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //}

        //Switch state to Dance
        currentState = State.Dance;
        danceDelay = 0f;
        animator.SetTrigger("Dance1");
        //animator.SetBool("Running", false);
        
        //Look at the camera.. maybe?
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        
        
    }

    private void OnFindKey()
    {
        Debug.Log("In OnFindKey()");

        // Add code to make something happen when the key is found like it getting bigger before disappearing
        waypoints[currentWaypoint].transform.gameObject.GetComponent<MeshRenderer>().enabled = false;

        currentState = State.Dance;
        danceDelay = 0f;
        animator.SetTrigger("Dance1");
       // animator.SetBool("Running", false);
       
       
       //Already in Dance()
         //onFindTarget = OnFindDoor;
         //currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
         //navAgent.SetDestination(waypoints[currentWaypoint].position);
    }

    private void OnFindDoor()
    {
        Debug.Log("In OnFindDoor()");
        currentState = State.Idle;
        //animator.SetBool("Running", false);
        if (doorDelay < 1.5f)
        
            doorDelay += Time.deltaTime;
        
        else
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navAgent.SetDestination(waypoints[currentWaypoint].position);
        }
    }
    
    private void Dance()
    {
        Debug.Log("In Dance()");
        if (danceDelay < 2f) danceDelay += Time.deltaTime;
        else
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            Debug.Log(waypoints[currentWaypoint].gameObject.name + " Pos: " + waypoints[currentWaypoint].position);
            navAgent.SetDestination(waypoints[currentWaypoint].position);
            Debug.Log("Nav Dest: " + navAgent.destination);
            currentState = State.MoveTo;
            if (onFindTarget == OnFindTreasure)
            { 
                onFindTarget = OnFindKey; 
            }
            else {
                onFindTarget = OnFindDoor;
            }
            animator.SetBool("Running", true);

        }
    }
}
