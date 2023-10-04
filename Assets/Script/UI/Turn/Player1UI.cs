using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1UI : PlayerUIBase
{
    protected override void Start()
    {
        base.Start();
        portrait.sprite = gameManager.player1Sprite;
    }
}
