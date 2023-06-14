using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class AIController : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public float startWaitTime = 4f;
    public float startRotationTime = 2f;
    public float walkSpeed = 7f;
    public float runSpeed = 10f;

    public float viewRadius = 7f; //was 15f
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    private GameObject playerObj;
    
    public Transform[] waypoints;
    private int currentWaypointIndex;
    
    private Vector3 playerLastPos = Vector3.zero;
    private Vector3 playerPos;

    private float waitTime;
    private float rotationTime;
    private bool playerInRange;
    private bool playerNear;
    private bool patrolling;
    private bool playerCaught;
    public int youChump = 0;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == this.GameObject())
        {
            playerObj = GameObject.Find("Agent");
        }
        Transform waypointGroup = GameObject.Find("Waypoints").transform;
        waypoints = new Transform[waypointGroup.childCount];
        for (int i = 0; i< waypointGroup.childCount; i++)
        {
            waypoints[i] = waypointGroup.GetChild(i);
        }
        
        playerPos = Vector3.zero;
        patrolling = true;
        playerCaught = false;
        playerInRange = false;
        waitTime = 0f; //startWaitTime;
        rotationTime = startRotationTime;

        currentWaypointIndex = Random.Range(0,waypoints.Length -1);
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;
        navAgent.SetDestination(waypoints[currentWaypointIndex].position);

    
}
    
    void Update()
    {
        youChump++;
        //Debug.Log( "Frame: " + youChump);
        AgentVision();

        if (!patrolling)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    void Move(float speed)
    {
        navAgent.isStopped = false;
        navAgent.speed = speed;
    }

    void Stop()
    {
        navAgent.isStopped = true;
        navAgent.speed = 0;
    }

    void MoveNextPoint()
    {
        currentWaypointIndex = Random.Range(0, waypoints.Length - 1); //(currentWaypointIndex + 1) % waypoints.Length;
        navAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    void Patrol()
    {
        Debug.Log("Start Patrol");
        if (playerNear)
        {
            if (rotationTime <= 0)
            {
                Move(walkSpeed);
                LookingPlayer(playerLastPos);
            }
            else
            {
                Stop();
                rotationTime -= Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("Player not near");
            playerNear = false;
            playerLastPos = Vector3.zero;
            navAgent.SetDestination(waypoints[currentWaypointIndex].position);
            Debug.Log("remainingDistance: " + navAgent.remainingDistance +" stoppingDistance: " + navAgent.stoppingDistance);
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                Debug.Log("Not at NavMesh");
                if (waitTime <= 0)
                {
                    MoveNextPoint();
                    Move(walkSpeed);
                    waitTime = startWaitTime;
                }
                else
                {
                    Debug.Log("WaitTime not zero");
                    Stop();
                    waitTime -= Time.deltaTime;
                    youChump++;
                    Debug.Log("Wait Time: " + waitTime);
                }
            }
        }
    }

    void Chase()
    {
        playerNear = false;
        playerLastPos = Vector3.zero;
        if (!playerCaught)
        {
            Move(runSpeed);
            navAgent.SetDestination(playerPos);
        }

        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (waitTime <= 0 && !playerCaught && Vector3.Distance(transform.position,
                    playerObj.transform.position) >= 6f)
            {
                patrolling = true;
                playerNear = false;
                Move(walkSpeed);
                rotationTime = startRotationTime;
                waitTime = startWaitTime;
                navAgent.SetDestination(waypoints[currentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position,
                        playerObj.transform.position) >= 2.5f)
                {
                    Stop();
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
    
    //Not called yet, need to add something that does something
    void CaughtPlayer()
    {
        playerCaught = true;
    }

    void LookingPlayer(Vector3 player)
    {
        navAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (waitTime <= 0)
            {
                playerNear = false;
                Move(walkSpeed);
                navAgent.SetDestination(waypoints[currentWaypointIndex].position);
                waitTime = startWaitTime;
                rotationTime = startRotationTime; 
            }
            else
            {
                Stop();
                waitTime -= Time.deltaTime;
            }
        }
    }

    void AgentVision()
    {
        if (this.GameObject().layer != 6)
        {
            Collider[] checkPlayerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

            for (int i = 0; i < checkPlayerInRange.Length; i++)
            {
                Transform player = checkPlayerInRange[i].transform;
                Vector3 playerDir = (player.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, playerDir) < viewAngle / 2)
                {
                    float playerDist = Vector3.Distance(transform.position, player.position);
                    if (!Physics.Raycast(transform.position, playerDir, playerDist, obstacleMask))
                    {
                        this.playerInRange = true;
                        patrolling = false;
                    }
                    else
                    {
                        this.playerInRange = false;
                    }
                }

                if (Vector3.Distance(transform.position, player.position) > viewRadius)
                {
                    this.playerInRange = false;
                }

                if (this.playerInRange)
                {
                    playerPos = player.transform.position;
                }
            }


        }
    }

}
