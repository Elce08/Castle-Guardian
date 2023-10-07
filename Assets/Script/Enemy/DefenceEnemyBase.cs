using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceEnemyBase : EnemyBase
{
    bool isMove = true;

    DefencePlayerBase player;

    protected DefenceManager defenceManager;

    public float moveSpeed = 1f;

    enum EnemyState
    {
        Move,
        Attack,
    }

    EnemyState state = EnemyState.Move;

    private EnemyState State
    {
        get => state;
        set
        {
            if(state != value)
            {
                state = value;
                switch(state)
                {
                    case EnemyState.Move:
                        anim.SetTrigger("IsWalk");
                        isMove = true;
                        break;
                    case EnemyState.Attack:
                        isMove = false;
                        break;
                }
            }
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        gameObject.transform.localScale = new(0.3f, 0.3f, 0.3f);
        State = EnemyState.Move;
    }

    void Update()
    {
        if(isMove)
        {
            transform.Translate(Time.deltaTime * moveSpeed * -transform.right);
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (player != null)
            {
                anim.SetTrigger("IsAttack");
                player.Hitted(attackDamage);
                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            }
            else if (player == null)
            {
                isMove = true;
                State = EnemyState.Move;
                break;
            }
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            State = EnemyState.Attack;
            player = other.gameObject.GetComponent<DefencePlayerBase>();
            StartCoroutine(AttackCoroutine());
        }
        if (other.CompareTag("LosingPoint"))
        {
            StopAllCoroutines();
            defenceManager.LosingPoint.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }

    protected override void Die()
    {
        defenceManager.WinPoint.Invoke();
        StopAllCoroutines();
        base.Die();
    }
}
