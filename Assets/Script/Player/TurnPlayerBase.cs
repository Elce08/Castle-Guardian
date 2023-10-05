using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngineInternal;

public class TurnPlayerBase : PlayerBase, ITurn
{
    PlayerInputActions inputActions;

    bool endTurn = false;

    public bool EndTurn
    {
        get => endTurn;
        set => endTurn = value;
    }

    bool isAlive = true;
    public bool IsAlive =>isAlive;

    public float moveSpeed = 5.0f;

    public Vector3 startPos;

    GameObject buttons;

    Button attack;
    Button skill;

    public TurnManager turnManager;

    TurnEnemyBase[] enemys;

    TurnEnemyBase target;

    protected enum State
    {
        Idle,
        ToTraget,
        Back,
        Attack,
        Hitted,
    }

    protected State state = State.Idle;

    protected State CharacterState
    {
        get => state;
        set
        {
            if(state != value)
            {
                state = value;
                switch (state)
                {
                    case State.Idle:
                        anim.SetTrigger("IsIdle");
                        onMoveUpdate = Update_Idle;
                        break;
                    case State.ToTraget:
                        anim.SetTrigger("IsRun");
                        onMoveUpdate = Update_ToTarget;
                        break;
                    case State.Back:
                        anim.SetTrigger("IsRun");
                        onMoveUpdate = Update_Back;
                        break;
                    case State.Attack:
                        anim.SetTrigger("IsAttack");
                        onMoveUpdate = Update_Attack;
                        break;
                    case State.Hitted:
                        onMoveUpdate = null;
                        break;
                }
            }
        }
    }

    Action onMoveUpdate;

    public override float Mp
    {
        get => mp;
        set
        {
            if (mp != value)
            {
                if (value > MaxMp) mp = MaxMp;
                else mp = value;
            }
        }
    }

    protected override void Awake()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        enemys = new TurnEnemyBase[3];
        base.Awake();
        inputActions = new PlayerInputActions();
        buttons = GameObject.Find("Buttons");
        Transform child = buttons.transform.GetChild(0);
        attack = child.GetComponent<Button>();
        child = buttons.transform.GetChild(1);
        skill = child.GetComponent<Button>();
        onMoveUpdate += Update_Idle;
    }

    protected override void Start()
    {
        base.Start();
        buttons.SetActive(false);
        target = null;
        CharacterState = State.Idle;
    }

    private void Update()
    {
        onMoveUpdate?.Invoke();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.NumberPad.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        inputActions.NumberPad.Disable();
    }

    public void Stop(bool stop)
    {
        if (stop)
        {
            inputActions.NumberPad.Disable();
        }
        else if (!stop)
        {
            inputActions.NumberPad.Enable();
        }
    } 

    public void OnAttack()
    {
        Debug.Log($"{gameObject.name}turn");
        SetTarget();
    }

    void SetTarget()
    {
        enemys = turnManager.enemys;
        inputActions.NumberPad._1.performed += _1_performed;
        inputActions.NumberPad._2.performed += _2_performed;
        inputActions.NumberPad._3.performed += _3_performed;
        inputActions.NumberPad.Mouse.performed += Mouse_performed;
    }

    void ChooseAction()
    {
        if(target != null)
        {
            buttons.SetActive(true);
            attack.onClick.AddListener(Attack);
            skill.onClick.AddListener(Skill);
            inputActions.NumberPad._1.performed += _1_Attack;
            inputActions.NumberPad._2.performed += _2_Skill;
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
        if (target != null)
        {
            attack.onClick.RemoveAllListeners();
            skill.onClick.RemoveAllListeners();
            inputActions.NumberPad._1.performed -= _1_Attack;
            inputActions.NumberPad._2.performed -= _2_Skill;
            buttons.SetActive(false);
            CharacterState = State.ToTraget;
        }
    }

    void Skill()
    {
        Debug.Log("Skill");
        if (target != null)
        {
            attack.onClick.RemoveAllListeners();
            skill.onClick.RemoveAllListeners();
            inputActions.NumberPad._1.performed -= _1_Attack;
            inputActions.NumberPad._2.performed -= _2_Skill;
            buttons.SetActive(false);
            CharacterState = State.ToTraget;
        }
    }

    public void GetDamaged(float damage)
    {
        Hitted(damage);
    }

    protected override void Die()
    {
        isAlive = false;
        base.Die();
    }

    private void _1_performed(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        target = enemys[0];
        if (target != null)
        {
            inputActions.NumberPad._1.performed -= _1_performed;
            inputActions.NumberPad._2.performed -= _2_performed;
            inputActions.NumberPad._3.performed -= _3_performed;
            inputActions.NumberPad.Mouse.performed -= Mouse_performed;
            ChooseAction();
        }
    }
    private void _2_performed(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        target = enemys[1];
        if (target != null)
        {
            inputActions.NumberPad._1.performed -= _1_performed;
            inputActions.NumberPad._2.performed -= _2_performed;
            inputActions.NumberPad._3.performed -= _3_performed;
            inputActions.NumberPad.Mouse.performed -= Mouse_performed;
            ChooseAction();
        }
    }
    private void _3_performed(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        target = enemys[2];
        if (target != null)
        {
            inputActions.NumberPad._1.performed -= _1_performed;
            inputActions.NumberPad._2.performed -= _2_performed;
            inputActions.NumberPad._3.performed -= _3_performed;
            inputActions.NumberPad.Mouse.performed -= Mouse_performed;
            ChooseAction();
        }
    }
    private void Mouse_performed(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if(hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                inputActions.NumberPad._1.performed -= _1_performed;
                inputActions.NumberPad._2.performed -= _2_performed;
                inputActions.NumberPad._3.performed -= _3_performed;
                inputActions.NumberPad.Mouse.performed -= Mouse_performed;
                target = hit.collider.GetComponent<TurnEnemyBase>();
                ChooseAction();
            }
        }
    }
    private void _1_Attack(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        Attack();
    }
    private void _2_Skill(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        Skill();
    }

    void Update_Idle()
    {
        transform.position = transform.position;
    }

    void Update_ToTarget()
    {
        onMoveUpdate -= Update_Idle;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime * 2.0f);
        if (transform.position.x > (target.transform.position.x -3.0f))
        {
            CharacterState = State.Attack;
        }
    }

    void Update_Back()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime * 2.0f);
        if ((transform.position.x - startPos.x) < 0.001)
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
        Mp += 5.0f;
        StartCoroutine(AttackActionCoroutine());
    }

    IEnumerator AttackActionCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
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
        StartCoroutine( base.HittedCoroutine());
        CharacterState = State.Idle;
        yield return null;
    }
}
