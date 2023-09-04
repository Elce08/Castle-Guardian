using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayerBase : PlayerBase, ITurn
{
    protected override void Start()
    {
        base.Start();
        Speed = Fast(speed);
    }

    public float Fast(float speed)
    {
        return speed;
    }
}
