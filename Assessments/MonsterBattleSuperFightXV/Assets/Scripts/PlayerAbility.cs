using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
	[SerializeField] 
	protected TurnTimer _turnTimer;
	[SerializeField] 
	protected Health _enemy;
	[SerializeField] 
	protected Health _player;

	public abstract void UseAbility();

	protected void EndTurn()
	{
		if(_turnTimer != null)
		{
			_turnTimer.ResetTimer();
		}
	}
}
