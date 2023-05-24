using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Creating a menu item to load the behaviour
[CreateAssetMenu(menuName = "Flock/Behaviour/Composite", fileName = "Composite")]

public class CompositeBehaviour : Behaviour
{
    // Making a struct to hold the moultiple data values
    [System.Serializable]
    public struct BehaviourGroup
    {
        public Behaviour behaviour;
        public float weights;
    }

    public BehaviourGroup[] behaviours;

    //Doing something that I haven't looked at yet
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 move = Vector2.zero;

        //Looping through somehting and doing something
        foreach (BehaviourGroup behaviour in behaviours)
        {
            Vector2 partialMove = behaviour.behaviour.CalculateMove(agent, context, flock);

            if (partialMove == Vector2.zero) continue;

            partialMove.Normalize();
            partialMove *= behaviour.weights;

            move += partialMove;

        }

        return move.normalized;
    }
}
