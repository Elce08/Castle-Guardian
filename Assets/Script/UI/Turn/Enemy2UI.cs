using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2UI : EnemyUIBase
{
    public override void GameOn()
    {
        enemy = turnManager.enemys[1];
        portrait.sprite = turnManager.enemySprites[turnManager.enemyType[1]];
        enemy.OnHpChange += SetHp;
    }

    void SetHp(float hp)
    {
        hpSlider.value = hp * 0.02f;
        hpText.text = $"{hp} / {enemy.startHp}";
    }
}
