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

    PlayerBase[] players;

    PlayerBase target;

    bool endTurn = false;
    public bool EndTurn
    {
        get => endTurn;
        set => endTurn = value;
    }

    bool isAlive = true;
    public bool IsAlive => isAlive;


    protected enum State
    {
        Idle,
        ToTraget,
        Back,
        Attack,
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
                        onMoveUpdate += Update_Idle;
                        break;
                    case State.ToTraget:
                        onMoveUpdate += Update_ToTarget;
                        break;
                    case State.Back:
                        onMoveUpdate += Update_Back;
                        break;
                    case State.Attack:
                        onMoveUpdate += Update_Attack;
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
        int setTarget = UnityEngine.Random.Range(0, players.Length);
        target = players[setTarget];
        CharacterState = State.ToTraget;
        Debug.Log($"{gameObject.name}turn");
    }

    public void GetDamaged(float damage)
    {

    }

    protected override void Die()
    {
        isAlive = false;
        base.Die();
    }

    void Update_Idle()
    {
        anim.SetTrigger("IsIdle");
        transform.position = transform.position;
    }

    void Update_ToTarget()
    {
        anim.SetTrigger("IsWalk");
        onMoveUpdate -= Update_Idle;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime * 2.0f);
        if (transform.position.x < (target.transform.position.x + 3.0f))
        {
            onMoveUpdate -= Update_ToTarget;
            CharacterState = State.Attack;
        }
    }

    void Update_Back()
    {
        anim.SetTrigger("IsWalk");
        transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime * 2.0f);
        if ((transform.position.x - startPos.x) > -0.001)
        {
            transform.position = startPos;
            target = null;
            onMoveUpdate -= Update_Back;
            Debug.Log($"{gameObject.name}turn end");
            endTurn = true;
            CharacterState = State.Idle;
        }
    }

    void Update_Attack()
    {
        anim.SetTrigger("IsAttack");
        StartCoroutine(AttackActionCoroutine());
    }

    IEnumerator AttackActionCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        onMoveUpdate -= Update_Attack;
        CharacterState = State.Back;
    }
}
