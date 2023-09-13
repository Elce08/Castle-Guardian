using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayer3 : TurnPlayerBase
{
    protected override void Awake()
    {
        base.Awake();
        playerType = gameManager.player3Type;
    }
    protected override void Start()
    {
        base.Start();
    }
}

