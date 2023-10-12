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
        startPos += turnManager.playersPosition[1];
        str += gameManager.player2ItemStr;
        def += gameManager.player2ItemDef;
        startHp += gameManager.player2ItemHP;
        MaxMp += gameManager.player2ItemMP;
        Hp = startHp;
        Mp = MaxMp;
    }
}
