using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSkeletonArcher : TurnEnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 14.0f;
    }
}
