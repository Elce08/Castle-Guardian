using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2UI : EnemyUIBase
{
    protected override void Start()
    {
        enemy = turnManager.enemys[1];
        portrait.sprite = turnManager.enemySprites[turnManager.enemyType[1]];
        enemy.OnHpChange += SetHp;
    }

    void SetHp(float hp)
    {
        hpSlider.value = hp * enemy.ReMaxHp;
        hpText.text = $"{hp} / {enemy.startHp}";
    }
}
