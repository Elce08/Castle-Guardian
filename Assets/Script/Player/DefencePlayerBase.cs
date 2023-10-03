using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DefencePlayerBase : PlayerBase
{
    DefenceEnemyBase Enemy;

    public override float Mp
    {
        get => mp;
        set
        {
            if(mp  != value)
            {
                mp = value;
                if(mp > MaxMp)
                {
                    mp = 0.0f;
                    Skill();
                }
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
    }

    protected override void Die()
    {
        base.Die();
    }

    IEnumerator AttackCoroutine(DefenceEnemyBase target)
    {
        if(target != null)
        {
            while(true)
            {
                if (target == null)
                {
                    anim.SetTrigger("IsIdle");
                }
                else
                {
                    anim.SetTrigger("IsAttack");
                    Mp += 5.0f;
                    yield return new WaitForSeconds(0.1f);
                    yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy = other.GetComponent<DefenceEnemyBase>();
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(AttackCoroutine(Enemy));
        }
    }

    public void Skill()
    {
        // ½ºÅ³
    }
}
