using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Data : PlayerUIBase
{
    protected override void Awake()
    {
        portrait = transform.GetChild(1).GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        portrait.sprite = gameManager.player2Sprite;
    }
}
