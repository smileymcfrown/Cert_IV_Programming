using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : Behaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }


        Vector2 avoidanceMove = Vector2.zero;

        int count = 0;

        foreach (Transform item in context)
        {
            Vector2 directionToItem = agent.transform.position - item.position;
            if (directionToItem.sqrMagnitude <= flock.squareAvoidanceRadius)
            {
                avoidanceMove += (Vector2)directionToItem;
                count++;
            }
    
        }

        if(count != 0)
        {
            avoidanceMove /= count;
        }
        
        return avoidanceMove;
    }
}
