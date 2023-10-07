using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DefencePlayerBase : PlayerBase
{
    List<DefenceEnemyBase> enemyList = new();

    bool onAttack = false;

    float attackSpeed;

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
        attackSpeed = 30.0f / speed;
    }

    protected override void Die()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        base.Die();
    }

    IEnumerator AttackCoroutine()
    {
        onAttack = true;
        while(true)
        {
            if(enemyList.Count > 0)
            {
                foreach (DefenceEnemyBase enemy in enemyList)
                {
                    enemy.Hitted(str);
                }
                new WaitForSeconds(attackSpeed);
            }
            else
            {
                onAttack = false;
                break;
            }
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyList.Add(other.GetComponent<DefenceEnemyBase>());
            if(!onAttack) StartCoroutine(AttackCoroutine());
            Debug.Log(enemyList.Count);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyList.Remove(other.GetComponent<DefenceEnemyBase>());
            Debug.Log(enemyList.Count);
        }
    }

    public void Skill()
    {
        // ½ºÅ³
    }
}
