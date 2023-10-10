using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenceEnemyBase : EnemyBase
{
    bool isMove = true;

    DefencePlayerBase player;

    protected DefenceManager defenceManager;

    public float moveSpeed = 1f;

    public float attackSpeed = 3.0f;

    Transform child;
    RectTransform sliderParentTransform;
    Slider slider;

    Vector3 screenPos;

    bool sliderActive = false;

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

    public override float Hp
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                if (slider != null)
                {
                    slider.value = hp / startHp;
                }
                if (hp <= 0)
                {
                    hp = 0;
                    isAlive = false;
                    Die();
                }
            }
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        child = transform.GetChild(2);
        Transform grandChild = child.transform.GetChild(0);
        slider = grandChild.GetComponent<Slider>();
        slider.gameObject.SetActive(false);

    }

    protected override void Start()
    {
        base.Start();
        gameObject.transform.localScale = new(0.3f, 0.3f, 0.3f);
        State = EnemyState.Move;
        defenceManager = FindObjectOfType<DefenceManager>();
    }

    void Update()
    {
        if (isMove)
        {
            transform.Translate(Time.deltaTime * moveSpeed * -transform.right);


            screenPos = Camera.main.WorldToScreenPoint(transform.position);
            // 스크린 좌표를 캔버스 좌표로 변환
            RectTransformUtility.ScreenPointToLocalPointInRectangle(child.GetComponent<RectTransform>(), screenPos, null, out Vector2 localPos);

            // Slider의 위치를 업데이트
            localPos.y += -50;
            localPos.x += 5;
            if (!sliderActive)
            {
                slider.gameObject.SetActive(true);
            }
            slider.transform.localPosition = localPos;
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
                yield return new WaitForSeconds(attackSpeed);
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
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }

    public override void Hitted(float damage)
    {
        Hp -= damage / def;
        if (isAlive) StartCoroutine(base.HittedCoroutine());
    }

    protected override void Die()
    {
        defenceManager.WinPoint.Invoke();
        StopAllCoroutines();
        base.Die();
        gameObject.SetActive(false);
    }
}
