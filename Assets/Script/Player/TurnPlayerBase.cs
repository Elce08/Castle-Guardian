using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayerBase : PlayerBase, ITurn
{
    bool isAlive = true;
    public bool IsAlive =>isAlive;

    protected override void Start()
    {
        base.Start();
    }

    public void Attack()
    {

    }

    public void GetDamaged(float damage)
    {

    }

    protected override void Die()
    {
        isAlive = false;
        base.Die();
    }
}
