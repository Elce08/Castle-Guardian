using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnGoblinmagician : TurnEnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 10.0f;
    }
}
