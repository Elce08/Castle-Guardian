using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data - Hammer1", menuName = "Scriptable Object/Item Data - Hammer1", order = 12)]
public class ItemData_Warrior_Hammer1 : ItemData
{
    [Header("망치1 데이터")]
    ItemData data;

    public override WeaponType equipPart => WeaponType.Warrior_Hammer;

    public override void ItemStatus()
    {
        speed = 5;
        price = 1000;      // 아이템 가치

        beforeStr = (upgrade + 1) * 5;                 // 아이템의 강화 전 Str 수치
        beforeWis = (upgrade + 1) * 5;                 // 아이템의 강화 전 Agi 수치
        beforeDef = (upgrade + 1) * 5;                 // 아이템의 강화 전 Int 수치
        beforeHP = (upgrade + 1) * 10;                 // 아이템의 강화 전 HP 수치
        beforeMP = (upgrade + 1) * 10;                 // 아이템의 강화 전 MP 수치
        beforeValue = upgrade;                         // 아이템의 강화 전 강화 수치

        afterStr = (upgrade + 2) * 5;                  // 아이템의 강화 후 Str 수치
        afterWis = (upgrade + 2) * 5;                  // 아이템의 강화 후 Agi 수치
        afterDef = (upgrade + 2) * 5;                  // 아이템의 강화 후 Int 수치
        afterHP = (upgrade + 2) * 10;                  // 아이템의 강화 후 HP 수치
        afterMP = (upgrade + 2) * 10;                  // 아이템의 강화 후 MP 수치
        afterValue = Mathf.Min(upgrade + 1, 5);        // 아이템의 강화 후 강화 수치

        risingStr = afterStr - beforeStr;    // 아이템의 강화 시 상승 Str 수치
        risingWis = afterWis - beforeWis;    // 아이템의 강화 시 상승 Agi 수치
        risingDef = afterDef - beforeDef;    // 아이템의 강화 시 상승 Int 수치
        risingHP = afterHP - beforeHP;       // 아이템의 강화 시 상승 HP 수치
        risingMP = afterMP - beforeMP;       // 아이템의 강화 시 상승 MP 수치

        cost = (upgrade + 1) * 500;                          // 아이템의 강화 시 소모 비용
    }
}
