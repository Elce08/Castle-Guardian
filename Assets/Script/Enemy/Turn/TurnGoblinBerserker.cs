using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnGoblinBerserker : TurnEnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 8.0f;
    }
}
