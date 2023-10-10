using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefencePlayer2 : DefencePlayerBase
{
    GameManager gamemanager;
    protected override void Awake()
    {
        base.Awake();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    protected override void Start()
    {
        playerType = gamemanager.player2Type;
        base.Start();
        str = gameManager.player2Str;
        def = gameManager.player2Def;
        startHp = gameManager.player2HP;
        MaxMp = gameManager.player2MP;
        Hp = startHp;
        Mp = MaxMp;
    }
}
