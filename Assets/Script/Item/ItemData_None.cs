using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data - None", menuName = "Scriptable Object/Item Data - None", order = 0)]
public class ItemData_None : ItemData
{
    [Header("°©¿Ê µ¥ÀÌÅÍ")]
    ItemData data;


    public override WeaponType equipPart => WeaponType.None;
}
