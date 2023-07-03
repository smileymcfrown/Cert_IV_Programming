using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTimer : MonoBehaviour
{
	public float currentTime = 0;
	public float turnTime = 4;

	public bool _nextTurn = true;

	[SerializeField] private Bar _timerBar;

	public bool IsNextTurn()
	{
		if (Time.timeScale == 0)
		{
			return false;
		}
		
		return _nextTurn;
	}

	public void ResetTimer()
	{
		_nextTurn = false;
		currentTime = 0;
	}

	void Update()
	{
		if(_nextTurn) return;

		currentTime += Time.deltaTime;
		
		if(currentTime >= turnTime)
		{
			_nextTurn = true;
		}
		_timerBar.SetBar(currentTime/turnTime,1);
	}
}
