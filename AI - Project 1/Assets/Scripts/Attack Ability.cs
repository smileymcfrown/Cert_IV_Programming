using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : PlayerAbility
{
    public override void UseAbility()
    {
        if(_turnTimer.IsNextTurn())
        {
            Debug.Log("ATTACK!");

            int damage = Random.Range(20,30);
            _enemy.DealDamage(damage);
            EndTurn();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
