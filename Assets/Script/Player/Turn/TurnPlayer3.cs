using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayer3 : TurnPlayerBase
{
    GameManager gameManager;
    protected override void Awake()
    {
        base.Awake();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerType = gameManager.player3Type;
    }
    protected override void Start()
    {
        base.Start();
    }
}

