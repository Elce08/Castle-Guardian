using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayer2 : TurnPlayerBase
{
    GameManager gameManager;
    protected override void Awake()
    {
        base.Awake();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerType = gameManager.player2Type;
    }
    protected override void Start()
    {
        base.Start();
    }
}
