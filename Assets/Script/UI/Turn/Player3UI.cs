using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player3UI : PlayerUIBase
{
    protected override void Awake()
    {
        portrait = transform.GetChild(1).GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        portrait.sprite = gameManager.player3Sprite;
    }
}
