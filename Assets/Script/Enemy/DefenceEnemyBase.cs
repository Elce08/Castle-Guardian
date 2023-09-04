using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceEnemyBase : EnemyBase
{
    bool isMove = true;

    DefencePlayerBase player;

    DefenceManager defenceManager;

    public float moveSpeed = 1f;

    public float attackPeriod = 2f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        gameObject.transform.localScale = new(0.4f, 0.4f, 0.4f);
    }

    void Update()
    {
        if (isMove == true)
        {
            transform.Translate(Time.deltaTime * moveSpeed * -transform.right);
        }
    }

    IEnumerator AttackCoroutine(DefencePlayerBase target)
    {
        anim.SetBool("isWalk", false);
        if (target != null)
        {
            while (true)
            {
                anim.SetBool("isAttack", true);
                yield return new WaitForSeconds(attackPeriod);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("in");
            isMove = false;
            player = other.gameObject.GetComponent<DefencePlayerBase>();
            StartCoroutine(AttackCoroutine(player));
        }
        if (other.CompareTag("LosingPoint"))
        {
            StopAllCoroutines();
            // defenceManager.LosingPoint.Invoke();
            gameObject.SetActive(false);
        }
    }

    protected override void Die()
    {
        defenceManager.WinPoint.Invoke();
        base.Die();
    }
}
