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
    public float beforeStr = 0;           // �������� ��ȭ �� Strenth ��ġ
    public float beforeWis = 0;           // �������� ��ȭ �� Wisdom ��ġ
    public float beforeDef = 0;           // �������� ��ȭ �� Defence ��ġ
    public float beforeHP = 0;            // �������� ��ȭ �� HP ��ġ
    public float beforeMP = 0;            // �������� ��ȭ �� MP ��ġ
    public int beforeValue = 0;         // �������� ��ȭ �� ��ȭ ��ġ

    [Header("After Upgrade")]
    public float afterStr = 0;            // �������� ��ȭ �� Strength ��ġ
    public float afterWis = 0;            // �������� ��ȭ �� Wisdom ��ġ
    public float afterDef = 0;            // �������� ��ȭ �� Defence ��ġ
    public float afterHP = 0;             // �������� ��ȭ �� HP ��ġ
    public float afterMP = 0;             // �������� ��ȭ �� MP ��ġ
    public int afterValue = 0;          // �������� ��ȭ �� ��ȭ ��ġ

    [Header("Value of Upgrade")]
    public float risingStr = 0;           // �������� ��ȭ �� ��� Strength ��ġ
    public float risingWis = 0;           // �������� ��ȭ �� ��� Wisdom ��ġ
    public float risingDef = 0;           // �������� ��ȭ �� ��� Int ��ġ
    public float risingHP = 0;            // �������� ��ȭ �� ��� HP ��ġ
    public float risingMP = 0;            // �������� ��ȭ �� ��� MP ��ġ

    public int price = 0;              // ������ ��ġ
    public int cost = 0;                // �������� ��ȭ �� �Ҹ� ���


    public virtual void ItemStatus()
    {
    }

    public virtual void ItemUpgrade()
    {
    }
}
