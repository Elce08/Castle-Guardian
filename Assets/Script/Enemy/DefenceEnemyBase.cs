using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceEnemyBase : EnemyBase
{
    bool isMove = true;

    DefencePlayerBase player;

    protected DefenceManager defenceManager;

    public float moveSpeed = 1f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
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
        if (target != null)
        {
            while (true)
            {
                anim.SetTrigger("IsAttack");
                yield return new WaitForSeconds(0.1f);
                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
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
