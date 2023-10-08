using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UIElements;

public class DefencePlayerBase : PlayerBase
{
    List<DefenceEnemyBase> enemyList = new();

    public bool onAttack = false;

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
                yield return new WaitForSeconds(0.01f);
                foreach (DefenceEnemyBase enemy in enemyList)
                {
                    if (enemy.isAlive)
                    {
                        enemy.Hitted(str);
                    }
                }
                DeleteEnemy(delEnemyList);
                anim.SetTrigger("IsIdle");
                yield return new WaitForSeconds(attackSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyList.Add(other.GetComponent<DefenceEnemyBase>());
            anim.SetTrigger("IsAttack");
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
                Debug.Log("코루틴 종료");
            }
        }
    }

    /// <summary>
    /// 코루틴 중 적을 지울 수 없어서 임시로 지울 적 리스트 작성
    /// </summary>
    List<DefenceEnemyBase> delEnemyList = new();

    void DeleteEnemy(List<DefenceEnemyBase> delList)
    {
        foreach(DefenceEnemyBase enemy in delList)
        {
            enemyList.Remove(enemy);
        }
        delEnemyList.Clear();
    }

    public void Skill()
    {
        // 스킬
    }
}
