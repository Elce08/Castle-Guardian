using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurn
{
    bool IsAlive { get; }

    public void Attack()
    {

    }

    public void GetDamaged(float damage)
    {

    }
}
