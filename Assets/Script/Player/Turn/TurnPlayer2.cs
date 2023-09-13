using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayer2 : TurnPlayerBase
{
    protected override void Awake()
    {
        base.Awake();
        playerType = gameManager.player2Type;
    }
    protected override void Start()
    {
        base.Start();
    }
}
