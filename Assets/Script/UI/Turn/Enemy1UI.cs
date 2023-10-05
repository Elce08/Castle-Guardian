using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1UI : EnemyUIBase
{
    public override void GameOn()
    {
        enemy = turnManager.enemys[0];
        portrait.sprite = turnManager.enemySprites[turnManager.enemyType[0]];
        enemy.OnHpChange += SetHp;
    }

    void SetHp(float hp)
    {
        hpSlider.value = hp * enemy.ReMaxHp;
        hpText.text = $"{hp} / {enemy.startHp}";
    }
}
