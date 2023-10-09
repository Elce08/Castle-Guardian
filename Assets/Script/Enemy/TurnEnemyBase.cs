using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurnEnemyBase : EnemyBase,ITurn
{
    public Vector3 startPos;

    public TurnManager turnManager;

    public float moveSpeed = 5.0f;

    TurnPlayerBase[] players;

    TurnPlayerBase target;

    bool endTurn = false;
    public bool EndTurn
    {
        get => endTurn;
        set => endTurn = value;
    }
    public bool IsAlive => isAlive;

    public System.Action<float> OnHpChange;

    public override float Hp
    {
        get => base.Hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                if (hp <= 0)
                {
                    hp = 0;
                    Die();
                }
                OnHpChange.Invoke(hp);
            }
        }
    }


    protected enum State
    {
        Idle,
        ToTraget,
        Back,
        Attack,
        Hitted,
        Die,
    }

    protected State state = State.Idle;

    protected State CharacterState
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;
                switch (state)
                {
                    case State.Idle:
                        onMoveUpdate = Update_Idle;
                        anim.SetTrigger("IsIdle");
                        break;
                    case State.ToTraget:
                        anim.SetTrigger("IsWalk");
                        onMoveUpdate = Update_ToTarget;
                        break;
                    case State.Back:
                        anim.SetTrigger("IsWalk");
                        onMoveUpdate = Update_Back;
                        break;
                    case State.Attack:
                        anim.SetTrigger("IsAttack");
                        onMoveUpdate = Update_Attack;
                        break;
                    case State.Hitted:
                        onMoveUpdate = null;
                        break;
                    case State.Die:
                        onMoveUpdate = null;
                        break;
                }
            }
        }
    }

    Action onMoveUpdate;

    protected override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        players = turnManager.players;
        onMoveUpdate += Update_Idle;
        CharacterState = State.Idle;
        target = null;
    }

    private void Update()
    {
        onMoveUpdate?.Invoke();
    }

    public void OnAttack()
    {
        while (true)
        {
            int setTarget = UnityEngine.Random.Range(0, players.Length);
            target = players[setTarget];
            if (target.IsAlive) break;
        }
        CharacterState = State.ToTraget;
    }

    protected override void Die()
    {
        CharacterState = State.Die;
        base.Die();
    }

    void Update_Idle()
    {
        transform.position = transform.position;
    }

    void Update_ToTarget()
    {
        Vector3 detination = new Vector3(target.transform.position.x + 2.0f, target.transform.position.y, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, detination, moveSpeed * Time.deltaTime * 2.0f);
        if (transform.position.x < (target.transform.position.x + 2.001f))
        {
            transform.position = detination;
            CharacterState = State.Attack;
        }
    }

    void Update_Back()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime * 2.0f);
        if ((transform.position.x - startPos.x) > -0.001)
        {
            transform.position = startPos;
            target = null;
            Debug.Log($"{gameObject.name}turn end");
            endTurn = true;
            CharacterState = State.Idle;
        }
    }

    void Update_Attack()
    {
        StartCoroutine(AttackActionCoroutine());
    }

    IEnumerator AttackActionCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        target.Hitted(attackDamage);
        CharacterState = State.Back;
        StopAllCoroutines();
    }

    public override void Hitted(float damage)
    {
        CharacterState = State.Hitted;
        base.Hitted(damage);
    }

    protected override IEnumerator HittedCoroutine()
    {
        CharacterState = State.Idle;
        StartCoroutine(base.HittedCoroutine());
        yield return null;
    }
}
