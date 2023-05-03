using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{   
    public FlockAgent agentPrefab;
    public List <FlockAgent> agents;
    [Range(10,500)]
    public int startingCount = 250;
    public float agentDensity = 0.08f;
    public Behaviour behaviour;

    [Range(1f, 10f)]
    public float contextRadius = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < startingCount; i++)
        {
            Vector2 randomLocation = Random.insideUnitCircle * startingCount * agentDensity;

            FlockAgent newAgent = Instantiate(
                agentPrefab, 
                randomLocation, 
                Quaternion.Euler(new Vector3(0,0,Random.Range(0,360f))),
                transform);

            newAgent.name = "Agent " + (i + 1);
            agents.Add(newAgent);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        List<Transform> context = new List<Transform>();
        foreach(FlockAgent agent in agents)
        {
            Vector2 move = behaviour.CalculateMove(agent, context, this);

            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, contextRadius);

        foreach(Collider2D foundCollider in contextColliders)
        {
            if(foundCollider != agent.agentCollider)
            {
                context.Add(foundCollider.transform);
            }
        }

        return context;
    }
}
