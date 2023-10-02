using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("Item Base Data")]
    public PlayerWeapon code;
    public string itemName = "Item";
    public Sprite itemIcon;
    public uint maxStackCount = 1;

    public virtual WeaponType equipPart => WeaponType.None;

    public int upgrade = 0;
    public float speed = 0;

    [Header("Before Upgrade")]
    public float beforeStr = 0;           // 아이템의 강화 전 Strenth 수치
    public float beforeWis = 0;           // 아이템의 강화 전 Wisdom 수치
    public float beforeDef = 0;           // 아이템의 강화 전 Defence 수치
    public float beforeHP = 0;            // 아이템의 강화 전 HP 수치
    public float beforeMP = 0;            // 아이템의 강화 전 MP 수치
    public int beforeValue = 0;         // 아이템의 강화 전 강화 수치

    [Header("After Upgrade")]
    public float afterStr = 0;            // 아이템의 강화 후 Strength 수치
    public float afterWis = 0;            // 아이템의 강화 후 Wisdom 수치
    public float afterDef = 0;            // 아이템의 강화 후 Defence 수치
    public float afterHP = 0;             // 아이템의 강화 후 HP 수치
    public float afterMP = 0;             // 아이템의 강화 후 MP 수치
    public int afterValue = 0;          // 아이템의 강화 후 강화 수치

    [Header("Value of Upgrade")]
    public float risingStr = 0;           // 아이템의 강화 시 상승 Strength 수치
    public float risingWis = 0;           // 아이템의 강화 시 상승 Wisdom 수치
    public float risingDef = 0;           // 아이템의 강화 시 상승 Int 수치
    public float risingHP = 0;            // 아이템의 강화 시 상승 HP 수치
    public float risingMP = 0;            // 아이템의 강화 시 상승 MP 수치

    public int price = 0;              // 아이템 가치
    public int cost = 0;                // 아이템의 강화 시 소모 비용


    public virtual void ItemStatus()
    {
    }

    public virtual void ItemUpgrade()
    {
    }
}
