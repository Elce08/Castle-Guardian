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
        startPos = turnManager.playersPosition[2];
        str = gameManager.player3Str;
        def = gameManager.player3Def;
        startHp = gameManager.player3HP;
        MaxMp = gameManager.player3MP;
        Hp = startHp;
        Mp = MaxMp;
    }
}

