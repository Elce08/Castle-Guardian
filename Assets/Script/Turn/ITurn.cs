using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurn
{
    bool IsAlive { get; }

    public void OnAttack()
    {
        
    }

    public void GetDamaged(float damage)
    {

    }
}
