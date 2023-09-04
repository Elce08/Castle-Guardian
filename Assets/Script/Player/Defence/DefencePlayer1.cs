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
    }
}
