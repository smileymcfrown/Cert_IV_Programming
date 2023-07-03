using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAbility : PlayerAbility
{
    public int damageMin = 5;
    public int damageMax = 20;
    [Range(0f,1f)]
    public float dblDmgChance = 0.05f;

    public Animator animPlayer;
    public Animator animEnemy;
    public override void UseAbility()
    {
        if (_turnTimer.IsNextTurn())
        {
            animPlayer.SetTrigger("range");
            
            int damage = Random.Range(damageMin, damageMax);
            if (Random.Range(0f, 1f) <= dblDmgChance)
            {
                damage++;
            }
            if (damage > 0)
            {
                animEnemy.SetTrigger("hurt");
            }
            _enemy.DealDamage(damage);

            EndTurn();
        }
    }
}