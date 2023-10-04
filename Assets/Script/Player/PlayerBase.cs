using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerBase : PooledObject
{
    protected Animator anim;

    public float speed = 0.0f;

    public float str = 0.0f;
    public float def = 1.0f;
    /// <summary>
    /// 방어력으로 나누는 것을 자주 하는것을 방지하기 위한 임시
    /// </summary>
    float Adef;

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
                UI.hpSlider.value = hp * ReMaxHP;
                UI.hpText.text = $"{hp} / {startHp}";
                if(hp <= 0)
                {
                    Die();
                }
            }
        }
    }

    public float MaxMp = 100.0f;

    public float mp;

    public virtual float Mp
    {
        get => mp;
        set
        {
            if (mp != value)
            {
                mp = value;
                UI.hpSlider.value = mp * ReMaxMP;
                UI.hpText.text = $"{mp} / {MaxMp}";
            }
        }
    }

    // 피통 나누기 자주 안하게 하기 위한 임시
    float ReMaxHP;
    float ReMaxMP;

    public GameManager gameManager;

    public PlayerType playerType;

    protected virtual void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    protected virtual void Start()
    {
        switch (playerType)
        {
            case PlayerType.None:
                GameObject.Instantiate(gameManager.playerTypePrefabs[0], transform.position, Quaternion.identity).transform.parent = this.transform;
                break;
            case PlayerType.Archor:
                GameObject.Instantiate(gameManager.playerTypePrefabs[1], transform.position, Quaternion.identity).transform.parent = this.transform;
                speed = 13.0f;
                str = 4.0f;
                def = 1.0f;
                startHp = 85.0f;
                break;
            case PlayerType.Archor_LongBow:
                GameObject.Instantiate(gameManager.playerTypePrefabs[2], transform.position, Quaternion.identity).transform.parent = this.transform;
                speed = 15.0f;
                str = 5.0f;
                def = 1.0f;
                startHp = 80.0f;
                break;
            case PlayerType.Gunner:
                GameObject.Instantiate(gameManager.playerTypePrefabs[3], transform.position, Quaternion.identity).transform.parent = this.transform;
                speed = 11.0f;
                str = 5.0f;
                def = 1.0f;
                startHp = 90.0f;
                break;
            case PlayerType.Soldier_LongSword:
                GameObject.Instantiate(gameManager.playerTypePrefabs[4], transform.position, Quaternion.identity).transform.parent = this.transform;
                speed = 5.0f;
                str = 7.0f;
                def = 2.0f;
                startHp = 150.0f;
                break;
            case PlayerType.Soldier_ShortSword:
                GameObject.Instantiate(gameManager.playerTypePrefabs[5], transform.position, Quaternion.identity).transform.parent = this.transform;
                speed = 9.0f;
                str = 6.0f;
                def = 1.5f;
                startHp = 120.0f;
                break;
            case PlayerType.Warrior_Hammer:
                GameObject.Instantiate(gameManager.playerTypePrefabs[6], transform.position, Quaternion.identity).transform.parent = this.transform;
                speed = 7.0f;
                str = 7.0f;
                def = 1.5f;
                startHp = 120.0f;
                break;
        }
        anim = GetComponentInChildren<Animator>();
        hp = startHp;
        Mp = MaxMp;
        Adef = 1 / def;
        ReMaxHP = 1 / startHp;
        ReMaxMP = 1 / MaxMp;
    }

    public void Hitted(float damage)
    {
        Hp -= damage * Adef;
        StartCoroutine(HittedCoroutine());
    }

    protected virtual void Die()
    {
        anim.SetTrigger("IsDie");
    }

    IEnumerator HittedCoroutine()
    {
        anim.SetTrigger("IsHitted");
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetTrigger("IsIdle");
    }

    // --------------피통마나통 UI

    public PlayerUIBase UI;
}
