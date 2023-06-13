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

    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;

    public Transform[] waypoints;
    private int currentWaypoint;
    
    public NavMeshAgent navAgent;
    public Transform waypointsParent;
    public float walkSpeed = 7f;
    public float runSpeed = 10f;

    private float doorDelay;
    private float danceDelay;
    
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
        onFindTarget = OnFindTreasure;
        currentState = State.MoveTo;
        //animator.SetBool("Running", true);

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
        
    }

    private void MoveTo()
    {
        Debug.Log("In MoveTo()");
        
        if (!navAgent.pathPending)
        {
            Debug.Log("RemainingDistance: " + navAgent.remainingDistance + " StoppingDistance: " + navAgent.stoppingDistance);
            if (navAgent.remainingDistance <= navAgent.stoppingDistance + 0.5f)
            {
                navAgent.isStopped = true;
                navAgent.speed = 0;
                onFindTarget();

            }
            else
            {
                navAgent.isStopped = false;
                navAgent.speed = runSpeed;
            }
        }
    }

    private void OnFindTreasure()
    {
        Debug.Log("In OnFindTreasure()");
        //Destroy the treasure
        //This if statement is to stop the first object being destroyed immediately... BUT WHY?!
        Vector3 pos = transform.position - waypoints[currentWaypoint].position;
        if (pos.sqrMagnitude < 0.5f)
        {
            Destroy(waypoints[currentWaypoint].transform.GameObject());
        }

        //Switch state to Dance
        currentState = State.Dance;
        danceDelay = 0f;
        //animator.SetTrigger("Dance");
       // animator.SetBool("Running", false);
        
        //Look at the camera.. maybe?
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        
        
    }

    private void OnFindKey()
    {
        Debug.Log("In OnFindKey()");
        
        // Add code to make something happen when the key is found like it getting bigger before disappearing
        
        currentState = State.Dance;
        danceDelay = 0f;
       // animator.SetTrigger("Dance");
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
        if (danceDelay < 10f) danceDelay += Time.deltaTime;
        else
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navAgent.SetDestination(waypoints[currentWaypoint].position);
            currentState = State.MoveTo;
            if (onFindTarget == OnFindTreasure) onFindTarget = OnFindKey;
            else onFindTarget = OnFindDoor;
            danceDelay = 0f;
            //  animator.SetBool("Running", true);

        }
    }
}
