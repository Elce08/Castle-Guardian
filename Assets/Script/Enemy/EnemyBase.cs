using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : PooledObject
{
    protected Animator anim;

    public float attackDamage = 10.0f;

    public float speed;

    public float AttackDamage
    {
        get => attackDamage;
        set
        {
            // 무기 얻으면 스탯도 얻게
        }
    }

    public float defence = 50.0f;

    public float Defence
    {
        get => defence;
        set
        {
            // 방어구 얻으면 스탯도 얻게
        }
    }

    public float startHp = 50.0f;

    public float hp;

    public float Hp
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                if (hp <= 0)
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

    protected virtual void Die()
    {
        anim.SetBool("isDie", true);
    }
}
