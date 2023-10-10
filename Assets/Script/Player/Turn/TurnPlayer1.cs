using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayer1 : TurnPlayerBase
{
    public bool isturn = false;


    protected override void Awake()
    {
        base.Awake();
        playerType = gameManager.player1Type;
    }
    protected override void Start()
    {
        base.Start();
        startPos = turnManager.playersPosition[0];
        str = gameManager.player1Str;
        def = gameManager.player1Def;
        startHp = gameManager.player1HP;
        MaxMp = gameManager.player1MP;
        Hp = startHp;
        Mp = MaxMp;
    }
}
