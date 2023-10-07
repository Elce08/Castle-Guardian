using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DefencePlayerBase : PlayerBase
{
    private Queue<DefenceEnemyBase> enemy = new();

    DefenceEnemyBase Enemy = null;

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
        StopAllCoroutines();
        base.Die();
    }

    IEnumerator AttackCoroutine()
    {
        while(true)
        {
            if (enemy == null)
            {
                anim.SetTrigger("IsIdle");
                break;
            }
            else if (enemy != null)
            {
                Debug.Log("StartAttack");
                if(Enemy == null) Enemy = enemy.Dequeue();
                anim.SetTrigger("IsAttack");
                Enemy.Hitted(str);
                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            }
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(Enemy == null)
            {
                Enemy = other.gameObject.GetComponent<DefenceEnemyBase>();
            }
            else enemy.Enqueue(other.gameObject.GetComponent<DefenceEnemyBase>());
        }
    }

    public override void Hitted(float damage)
    {
        base.Hitted(damage);
    }

    public void Skill()
    {
        // ½ºÅ³
    }
}
