using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UIElements;

public class DefencePlayerBase : PlayerBase
{
    private readonly List<DefenceEnemyBase> enemyList = new();

    public bool onAttack = false;

    float attackSpeed;

    public bool fullMp = false;

    public float addMp = 10.0f;

    public override float Mp
    {
        get => mp;
        set
        {
            if(mp  != value)
            {
                mp = value;
                if(mp >= MaxMp)
                {
                    mp = MaxMp;
                    fullMp = true;
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
                anim.SetTrigger("IsAttack");
                if(!fullMp)
                {
                    foreach (DefenceEnemyBase enemy in enemyList)
                    {
                        if (enemy.isAlive)
                        {
                            enemy.Hitted(str);
                        }
                    }
                    Mp += addMp;
                }
                else if(fullMp)
                {
                    foreach (DefenceEnemyBase enemy in enemyList)
                    {
                        if (enemy.isAlive)
                        {
                            enemy.Hitted(str * 2);
                        }
                    }
                    Mp = 0.0f;
                    fullMp = false;
                }
                DeleteEnemy(delEnemyList);
                yield return new WaitForSeconds(attackSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyList.Add(other.GetComponent<DefenceEnemyBase>());
            if(!onAttack) StartCoroutine(AttackCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            delEnemyList.Add(other.AddComponent<DefenceEnemyBase>());
            if (enemyList.Count == delEnemyList.Count)
            {
                StopAllCoroutines();
                anim.SetTrigger("IsIdle");
                onAttack = false;
            }
        }
    }

    /// <summary>
    /// �ڷ�ƾ �� ���� ���� �� ��� �ӽ÷� ���� �� ����Ʈ �ۼ�
    /// </summary>
    private readonly List<DefenceEnemyBase> delEnemyList = new();

    void DeleteEnemy(List<DefenceEnemyBase> delList)
    {
        foreach(DefenceEnemyBase enemy in delList)
        {
            enemyList.Remove(enemy);
        }
        delEnemyList.Clear();
    }
}
