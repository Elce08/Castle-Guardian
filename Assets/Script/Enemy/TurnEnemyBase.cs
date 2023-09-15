using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnemyBase : EnemyBase,ITurn
{
    bool endTurn = false;
    public bool EndTurn
    {
        get => endTurn;
        set => endTurn = value;
    }

    bool isAlive = true;
    public bool IsAlive => isAlive;
    protected virtual void Start()
    {

    }

    public void OnAttack()
    {
        Debug.Log($"{gameObject.name}turn");
        endTurn = true;
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
