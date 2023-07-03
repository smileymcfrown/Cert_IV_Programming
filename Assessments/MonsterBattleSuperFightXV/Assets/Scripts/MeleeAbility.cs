using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAbility : PlayerAbility
{
	public int damageMin = 0;
	public int damageMax = 8;
	[Range(0f,1f)]
	public float missChance = .05f;

	public Animator animPlayer;
	public Animator animEnemy;
	public override void UseAbility()
	{
		if(_turnTimer.IsNextTurn())
		{
			animPlayer.SetTrigger("melee");
			if (Random.Range(0f, 1f) >= missChance)
			{
				int damage = Random.Range(damageMin, damageMax);
				if (damage > 0)
				{
					animEnemy.SetTrigger("hurt");
				}
				_enemy.DealDamage(damage);
			}
			EndTurn();
		}
	}
}