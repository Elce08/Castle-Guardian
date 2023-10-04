using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSkeletonWarrior : TurnEnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 6.0f;
    }
}
