using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : PlayerAbility
{
    public override void UseAbility()
    {
        if(_turnTimer.IsNextTurn())
        {
            _player.Heal();
            EndTurn();
        }
    }
}
