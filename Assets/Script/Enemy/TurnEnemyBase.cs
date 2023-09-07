using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnemyBase : EnemyBase,ITurn
{
    bool endTurn = true;
    public bool EndTurn => endTurn;

    bool isAlive = true;
    public bool IsAlive => isAlive;
    protected virtual void Start()
    {

    }

    public void OnAttack()
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
