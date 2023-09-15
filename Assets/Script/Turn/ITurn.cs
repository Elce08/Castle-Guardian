using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurn
{
    bool EndTurn { get; set; }

    bool IsAlive { get; }

    public void OnAttack()
    {
        
    }

    public void GetDamaged(float damage)
    {

    }
}
