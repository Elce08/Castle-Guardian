using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefencePlayer1 : DefencePlayerBase
{
    GameManager gamemanager;
    protected override void Awake()
    {
        base.Awake();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    protected override void Start()
    {
        playerType = gamemanager.player1Type;
        base.Start();
        str = gameManager.player1Str;
        def = gameManager.player1Def;
        startHp = gameManager.player1HP;
        MaxMp = gameManager.player1MP;
        Hp = startHp;
        Mp = MaxMp;
    }
}
