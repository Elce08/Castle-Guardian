using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefencePlayerBase : PlayerBase
{
    private readonly List<DefenceEnemyBase> enemyList = new();

    public bool onAttack = false;

    float attackSpeed;

    public bool fullMp = false;

    public float addMp = 10.0f;

    Vector3 screenPos;
    Transform child;
    UnityEngine.UI.Slider slider;
    bool sliderActive = false;

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
        child = transform.GetChild(0);
        Transform grandChild = child.transform.GetChild(0);
        slider = grandChild.GetComponent<UnityEngine.UI.Slider>();
        slider.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
        attackSpeed = 30.0f / speed;
    }

    private void Update()
    {
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
    /// 코루틴 중 적을 지울 수 없어서 임시로 지울 적 리스트 작성
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
