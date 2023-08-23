using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerData(PlayerType type = PlayerType.None)
        {
            this.playerType = type;
        }
        public PlayerType playerType;
    }

    public PlayerData playerData;

    public float attackDamage;

    public float AttackDamage
    {
        get => attackDamage;
        set
        {
            // ���� ������ ���ȵ� ���
        }
    }

    public float defence;

    public float Defence
    {
        get => defence;
        set
        {
            // �� ������ ���ȵ� ���
        }
    }

    
}
