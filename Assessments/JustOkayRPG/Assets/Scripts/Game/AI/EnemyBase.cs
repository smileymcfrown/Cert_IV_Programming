using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : Attributes
{
    #region Health Variables
    [Header("Health Display")]
    public Gradient healthColour;
    public Canvas myHealthCanvas;
    Transform _cam;
    #endregion
    #region AI Variables
    [Header("AI")]
    public Transform player;
    public enum AIStates
    {
        Patrol,
        Seek,
        Attack,
        Die
    }
    public AIStates state;
    [Header("AI Movement")]
    public Transform wayPointParent;
    public Transform[] wayPoints;
    public int curPoint;
    public float distanceToPoint, changePoint;
    public float walkSpeed, runSpeed;
    public Animator anim;
    public NavMeshAgent agent;
    public float stopFromPlayer, turnSpeed;
    [Header("AI Level")]
    public int difficulty;
    public int maxDifficulty;
    public Material[] enemyMats;
    public Renderer rend;
    [Header("AI Attack")]
    public float attackSpeed;
    public float attackRange, sightRange, baseDamage;
    #endregion
    #region Health Override
    public override void SetHealth()
    {
        base.SetHealth();
        attributes[0].displayImage.color = healthColour.Evaluate(attributes[0].displayImage.fillAmount);
        myHealthCanvas.transform.LookAt(myHealthCanvas.transform.position + _cam.forward);
    }
    #endregion
    #region AI Behaviours
    public virtual void Patrol()
    {
        //DO NOT CONTINUE IF NO WAYPOINTS, dead, player in range
        if (wayPoints.Length <= 0 || isUnAlived || Vector3.Distance(player.position, transform.position)<=sightRange)
        {
            //return throws us out of the behaviour
            return;
        }
        //Set State
        state = AIStates.Patrol;
        //Set Animation
        anim.SetBool("Walk",true);
        //Set Speed
        agent.speed = walkSpeed;
        //Set Stopping Distance
        agent.stoppingDistance = 0;
        //Set agent to target
        agent.destination = wayPoints[curPoint].position;
        //check distance to waypoint
        distanceToPoint = Vector3.Distance(transform.position,wayPoints[curPoint].position);
        //change waypoint if in range of current point
        if (distanceToPoint <= changePoint)
        {
            //if so go to next waypoint
            if (curPoint < wayPoints.Length-1)
            {
                curPoint++;
            }
            //if at end of patrol go to start
            else
            {
                curPoint = 1;
            }
        }
    }
    public virtual void Seek()
    {
        //temp variable distance
        float distance = Vector3.Distance(player.position,transform.position);
        //if the player is out of our sight range or inside our attack range
        if (distance > sightRange || distance < attackRange||isUnAlived||player.GetComponent<PlayerHandler>().isUnAlived)
        {
            //stop seeking
            return;
        }
        //Set AI state
        state = AIStates.Seek;
        //Set animation
        anim.SetBool("Run", true);
        //Set Stopping Distance
        agent.stoppingDistance = stopFromPlayer;
        //Change speed
        agent.speed = runSpeed;
        //Target is player
        agent.destination = player.position;
    }
    public virtual void Attack()
    {
        distanceToPoint = Vector3.Distance(player.position, transform.position);
        //if player out of attack range attack
        if (distanceToPoint > attackRange || isUnAlived || player.GetComponent<PlayerHandler>().isUnAlived)
        {
            //stop Attacking
            return;
        }
        //Set AI state
        state = AIStates.Attack;
        //Set animation
        anim.SetBool("Attack", true);
        //Set Stopping Distance
        agent.stoppingDistance = stopFromPlayer;
        //Change speed
        agent.speed = 0;
        //hurt player - should be triggered by the Animation event tho - inherited script will handle this

    }
    public virtual void Die()
    {
        //if we are alive
        if (attributes[0].currentValue > 0||isUnAlived)
        {
            //dont run this
            return;
        }
        //Set AI state
        state = AIStates.Die;
        //Set animation
        anim.SetTrigger("Die");
        //stop moving
        agent.destination = transform.position;
        agent.speed = 0;
        agent.enabled = false;
        //Drop Loot/Quest Item
        //is dead
        isUnAlived = true;
    }
    #endregion
    #region Dificulty
    public void Difficulty()
    {
        difficulty = Random.Range(1, maxDifficulty + 1);
        rend.material = enemyMats[difficulty - 1];
    }
    #endregion
    #region Unity Event Methods/Functions
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //attach the camera
        _cam = Camera.main.transform;
        //Get Animator from self
        anim = GetComponent<Animator>();
        //get navMeshAgent from self
        agent = GetComponent<NavMeshAgent>();
        //Set speed of agent
        agent.speed = walkSpeed;
        //Get waypoints array from waypoint parent
        wayPoints = wayPointParent.GetComponentsInChildren<Transform>();
        //Set target waypoint;
        curPoint = 1;
        //Set Patrol as Default
        Patrol();
    }
    public override void Update()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);
        Patrol();
        Seek();
        Attack();
        Die();

        base.Update();
    }
    #endregion
}
