using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : Behaviour
{
	public float speed;
	public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
	{
		return agent.transform.up * speed;
	}
}
