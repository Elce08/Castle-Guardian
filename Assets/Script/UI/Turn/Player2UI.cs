using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2UI : PlayerUIBase
{
    protected override void Start()
    {
        base.Start();
        portrait.sprite = gameManager.player2Sprite;
    }
}
