using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnGoblinwarrior : TurnEnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 4.0f;
    }
}
