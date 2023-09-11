using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TurnPlayerBase : PlayerBase, ITurn
{
    PlayerInputActions inputActions;

    bool endTurn = true;

    public bool EndTurn => endTurn;

    bool isAlive = true;
    public bool IsAlive =>isAlive;

    public float moveSpeed = 10.0f;

    Vector3 startPos;

    GameObject buttons;

    Button attack;
    Button skill;

    TurnEnemyBase[] enemys;

    TurnEnemyBase target;

    protected override void Awake()
    {
        enemys = new TurnEnemyBase[3];
        base.Awake();
        inputActions = new PlayerInputActions();
        buttons = GameObject.Find("Buttons");
        Transform child = buttons.transform.GetChild(0);
        attack = child.GetComponent<Button>();
        child = buttons.transform.GetChild(1);
        skill = child.GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();
        buttons.SetActive(false);
        target = null;
        startPos = transform.position;
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

    public void OnAttack()
    {
        enemys[0] = GameObject.Find("Enemy1").GetComponent<TurnEnemyBase>();
        enemys[1] = GameObject.Find("Enemy2").GetComponent<TurnEnemyBase>();
        enemys[2] = GameObject.Find("Enemy3").GetComponent<TurnEnemyBase>();
        endTurn = false;
        SetTarget();
    }

    void SetTarget()
    {
        inputActions.NumberPad._1.performed += _1_performed;
        inputActions.NumberPad._2.performed += _2_performed;
        inputActions.NumberPad._3.performed += _3_performed;
        inputActions.NumberPad.Mouse.performed += Mouse_performed;
        // 마우스로 누르는 것 추가
    }

    void ChooseAction()
    {
        if(target != null)
        {
            attack.onClick.AddListener(Attack);
            skill.onClick.AddListener(Skill);
            inputActions.NumberPad._1.performed += _1_Attack;
            inputActions.NumberPad._2.performed += _2_Skill;
        }
    }

    void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        if(target != null)
        {
            attack.onClick.RemoveAllListeners();
            skill.onClick.RemoveAllListeners();
            inputActions.NumberPad._1.performed -= _1_Attack;
            inputActions.NumberPad._2.performed -= _2_Skill;
            buttons.SetActive(false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRun", true);
            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.gameObject.transform.position, moveSpeed);
                if ((transform.position.x - target.gameObject.transform.position.x) > 0.001)
                {
                    anim.SetBool("isRun", false);
                    anim.SetBool("isAttack", true);
                    // 공격 함수
                    yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
                    anim.SetBool("isRun", true);
                    anim.SetBool("isAttack", false);
                    transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed);
                    if ((transform.position.x - startPos.x) < 0.001)
                    {
                        anim.SetBool("isRun", false);
                        anim.SetBool("isIdle", true);
                        endTurn = true;
                        break;
                    }
                }
            }
        }
    }

    void Skill()
    {
        StartCoroutine(SkillCoroutine());
    }

    IEnumerator SkillCoroutine()
    {
        if (target != null)
        {
            attack.onClick.RemoveAllListeners();
            skill.onClick.RemoveAllListeners();
            inputActions.NumberPad._1.performed -= _1_Attack;
            inputActions.NumberPad._2.performed -= _2_Skill;
            buttons.SetActive(false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRun", true);
            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.gameObject.transform.position, moveSpeed);
                if ((transform.position.x - target.gameObject.transform.position.x) > 0.001)
                {
                    anim.SetBool("isRun", false);
                    anim.SetBool("isAttack", true);
                    // 스킬 함수
                    yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
                    anim.SetBool("isRun", true);
                    anim.SetBool("isAttack", false);
                    transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed);
                    if ((transform.position.x - startPos.x) < 0.001)
                    {
                        anim.SetBool("isRun", false);
                        anim.SetBool("isIdle", true);
                        endTurn = true;
                        break;
                    }
                }
            }
        }
    }

    public void GetDamaged(float damage)
    {

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
    private void Mouse_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector3 mousePos = obj.action.ReadValue<Vector3>();
        Vector2  pos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

    }
    private void _1_Attack(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        Attack();
    }
    private void _2_Skill(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        Skill();
    }
}
