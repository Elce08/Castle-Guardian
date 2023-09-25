using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerBase : PooledObject
{
    protected Animator anim;

    public float speed;

    public float str;
    public float wis;
    public float def;

    public float startHp = 100.0f;

    public float hp;

    public float Hp
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                if(hp <= 0)
                {
                    Die();
                }
            }
        }
    }

    public float startMp = 100.0f;

    public float mp;

    public float Mp
    {
        get => mp;
        set
        {
            if (mp != value)
            {
                mp = value;
            }
        }
    }

    public GameManager gameManager;

    public PlayerType playerType;

    protected virtual void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    protected virtual void Start()
    {
        hp = startHp;
        Mp = startMp;
        switch (playerType)
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
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Die()
    {
        anim.SetBool("isDied", true);
    }
}
