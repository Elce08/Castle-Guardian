using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : PooledObject
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
            // 무기 얻으면 스탯도 얻게
        }
    }

    public float defence;

    public float Defence
    {
        get => defence;
        set
        {
            // 방어구 얻으면 스탯도 얻게
        }
    }

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        switch (playerData.playerType)
        {
            case PlayerType.None:
                GameObject.Instantiate(gameManager.playerTypePrefabs[0], transform.position, Quaternion.identity).transform.parent = this.transform;
                break;
            case PlayerType.Archor:
                GameObject.Instantiate(gameManager.playerTypePrefabs[1], transform.position, Quaternion.identity).transform.parent = this.transform;
                break;
            case PlayerType.Archor_LongBow:
                GameObject.Instantiate(gameManager.playerTypePrefabs[2], transform.position, Quaternion.identity).transform.parent = this.transform;
                break;
            case PlayerType.Gunner:
                GameObject.Instantiate(gameManager.playerTypePrefabs[3], transform.position, Quaternion.identity).transform.parent = this.transform;
                break;
            case PlayerType.Soldier_LongSword:
                GameObject.Instantiate(gameManager.playerTypePrefabs[4], transform.position, Quaternion.identity).transform.parent = this.transform;
                break;
            case PlayerType.Soldier_ShortSword:
                GameObject.Instantiate(gameManager.playerTypePrefabs[5], transform.position, Quaternion.identity).transform.parent = this.transform;
                break;
            case PlayerType.Warrior_Hammer:
                GameObject.Instantiate(gameManager.playerTypePrefabs[6], transform.position, Quaternion.identity).transform.parent = this.transform;
                break;
        }
    }
}
