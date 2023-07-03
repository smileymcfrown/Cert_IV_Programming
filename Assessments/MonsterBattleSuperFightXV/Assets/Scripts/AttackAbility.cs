using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : PlayerAbility
{
	public override void UseAbility()
	{
		if(_turnTimer.IsNextTurn())
		{
			int damage = Random.Range(20, 30);
			_enemy.DealDamage(damage);
			EndTurn();
		}
	}
}
