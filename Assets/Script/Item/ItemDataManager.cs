using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ������ ������ ���� ������ ������ �ִ� ������ Ŭ����(���� �޴����� ���� ���� ����)

public class ItemDataManager : MonoBehaviour
{
    /// <summary>
    /// ��� ������ ������ ���� �迭
    /// </summary>
    public ItemData[] itemDatas = null;
    // public ItemData[] itemDatas = new ItemData[Enum.GetValues(typeof(WeaponType)).Length];

    /// <summary>
    /// ������ ������ ������ ���� �ε���
    /// </summary>
    /// <param name="code">������ �������� �ڵ�</param>
    /// <returns>������ ������</returns>
    public ItemData this[PlayerWeapon code] => itemDatas[(int)code];

    /// <summary>
    /// ������ ������ ������ ���� �ε���(�׽�Ʈ��)
    /// </summary>
    /// <param name="index">������ �������� �ε���</param>
    /// <returns></returns>
    public ItemData this[int index] => itemDatas[index];

    /// <summary>
    /// ���� �����ϴ� ������ ������ ��� ����
    /// </summary>
    public int Length => itemDatas.Length;

    private void Start()
    {
        for(int i = 0; i < Length; i++)
        {
            itemDatas[i].ItemStatus();
        }
    }
}
