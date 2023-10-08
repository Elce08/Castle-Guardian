using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2UI : PlayerUIBase
{
    protected override void Awake()
    {
        base.Awake();
        gameManager = FindObjectOfType<GameManager>();
        portrait.sprite = gameManager.player2Sprite;
    }
}
