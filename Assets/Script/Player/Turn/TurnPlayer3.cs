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
        startPos += turnManager.playersPosition[2];
        str += gameManager.player3ItemStr;
        def += gameManager.player3ItemDef;
        startHp += gameManager.player3ItemHP;
        MaxMp += gameManager.player3ItemMP;
        Hp += startHp;
        Mp += MaxMp;
    }
}

