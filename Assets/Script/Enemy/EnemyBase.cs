using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : PooledObject
{
    protected Animator anim;

    public float speed;
    public float attackDamage = 5.0f;
    public float def = 1.0f;
    /// <summary>
    /// �������� ������ ���� ���� �ϴ°��� �����ϱ� ���� �ӽ�
    /// </summary>
    float Adef;

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

    protected virtual void Start()
    {
        Adef = 1 / def;
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
}
