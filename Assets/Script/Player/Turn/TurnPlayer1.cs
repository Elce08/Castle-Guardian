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
        startPos += turnManager.playersPosition[0];
        str += gameManager.player1ItemStr;
        def += gameManager.player1ItemDef;
        startHp += gameManager.player1ItemHP;
        MaxMp += gameManager.player1ItemMP;
        Hp = startHp;
        Mp = MaxMp;
    }
}
