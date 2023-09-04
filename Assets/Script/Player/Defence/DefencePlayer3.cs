using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefencePlayer3 : DefencePlayerBase
{
    GameManager gamemanager;
    protected override void Awake()
    {
        base.Awake();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    protected override void Start()
    {
        playerType = gamemanager.player3Type;
        base.Start();
    }
}
