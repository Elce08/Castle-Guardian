using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DefencePlayerBase : PlayerBase
{
    DefenceEnemyBase Enemy;

    public float attackSpeed = 5.0f;

    Animator animator;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
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
                    animator.SetBool("isAttack", false);
                    animator.SetBool("isIdle", true);
                    break;
                }
                animator.SetBool("isAttack", true);
                yield return new WaitForSeconds(attackSpeed);
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
}
