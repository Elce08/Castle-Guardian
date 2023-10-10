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
        startPos = turnManager.playersPosition[1];
        str = gameManager.player2Str;
        def = gameManager.player2Def;
        startHp = gameManager.player2HP;
        MaxMp = gameManager.player2MP;
        Hp = startHp;
        Mp = MaxMp;
    }
}
