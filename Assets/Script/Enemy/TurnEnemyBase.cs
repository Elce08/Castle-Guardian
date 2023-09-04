using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnemyBase : EnemyBase,ITurn
{
    protected virtual void Start()
    {
        Speed = Fast(speed);
    }

    public float Fast(float speed)
    {
        return speed;
    }
}
