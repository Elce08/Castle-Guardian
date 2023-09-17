using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayer1 : TurnPlayerBase
{
    protected override void Awake()
    {
        base.Awake();
        playerType = gameManager.player1Type;
    }
    protected override void Start()
    {
        base.Start();
        startPos = turnManager.playersPosition[0];
    }
}
