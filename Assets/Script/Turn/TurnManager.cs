using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public float spawnXPosition;

    public float moveSpeed = 5.0f;

    public float attackTerm;

    public Vector3[] enemysPosition;
    public Vector3[] playersPosition;
    private TurnEnemyBase[] enemys;
    private TurnPlayerBase[] players;

    Queue<GameObject> turnQueue = new();

    ITurn[] turnAct;

    private void Start()
    {
        enemys = new TurnEnemyBase[3];
        players = new TurnPlayerBase[3];
        for(int i = 0; i < 3; i++)
        {
            enemys[i] = EnemySpawn(enemysPosition[i]);
        }
        players[0] = GameObject.Find("Player1").GetComponent<TurnPlayerBase>();
        players[1] = GameObject.Find("Player2").GetComponent<TurnPlayerBase>();
        players[2] = GameObject.Find("Player3").GetComponent<TurnPlayerBase>();
        for(int  i = 0; i < players.Length; i++)
        {
            players[i].transform.position = new(-spawnXPosition, playersPosition[i].y, playersPosition[i].z);
        }
        StartCoroutine(StartMove());
    }

    TurnEnemyBase EnemySpawn(Vector3 position)
    {
        Vector3 spawnPosition = new(spawnXPosition, position.y, position.z);
        int random = Random.Range(0, 6);
        GameObject enemy;
        TurnEnemyBase spawnedEnemy = null;
        switch (random)
        {
            case 0:
                enemy = Factory.Inst.GetObject(PoolObjectType.TurnGoblinBerserker, spawnPosition);
                spawnedEnemy = enemy.GetComponent<TurnEnemyBase>();
                break;
            case 1:
                enemy = Factory.Inst.GetObject(PoolObjectType.TurnGoblinmagician, spawnPosition);
                spawnedEnemy = enemy.GetComponent<TurnEnemyBase>();
                break;
            case 2:
                enemy = Factory.Inst.GetObject(PoolObjectType.TurnGoblinwarrior, spawnPosition);
                spawnedEnemy = enemy.GetComponent<TurnEnemyBase>();
                break;
            case 3:
                enemy = Factory.Inst.GetObject(PoolObjectType.TurnSkeletonArcher, spawnPosition);
                spawnedEnemy = enemy.GetComponent<TurnEnemyBase>();
                break;
            case 4:
                enemy = Factory.Inst.GetObject(PoolObjectType.TurnSkeletonWarrior, spawnPosition);
                spawnedEnemy = enemy.GetComponent<TurnEnemyBase>();
                break;
            case 5:
                enemy = Factory.Inst.GetObject(PoolObjectType.TurnSkeletonWizard, spawnPosition);
                spawnedEnemy = enemy.GetComponent<TurnEnemyBase>();
                break;
        }
        return spawnedEnemy;
    }

    IEnumerator StartMove()
    {
        bool[] isStop = new bool[6];
        while (true)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.position = Vector3.MoveTowards(players[i].transform.position, playersPosition[i], moveSpeed * Time.deltaTime);
                if ((players[i].transform.position.x - playersPosition[i].x) >= -0.001)
                {
                    players[i].transform.position = playersPosition[i];
                    isStop[i] = true;
                }
                else isStop[i] = false;
            }
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].transform.position = Vector3.MoveTowards(enemys[i].transform.position, enemysPosition[i], moveSpeed * Time.deltaTime);
                if ((enemys[i].transform.position.x - enemysPosition[i].x) <= 0.001)
                {
                    enemys[i].transform.position = enemysPosition[i];
                    isStop[i + 3] = true;
                }
                else isStop[i + 3] = false;
            }
            if (isStop[0] && isStop[1] && isStop[2] && isStop[3] && isStop[4] && isStop[5])
            {
                StartCoroutine(StartGame());
                break;
            }
            yield return null;
        }
    }

    // °íÄ¡ÀÚ
    IEnumerator StartGame()
    {
        ITurn act = null;
        while (true)
        {
            SetQueue();
            for (int i = 0; i < turnQueue.Count; i++)
            {
                if (act == null)
                {
                    act = turnQueue.Dequeue().GetComponent<ITurn>();
                    if (act.IsAlive) act.OnAttack();
                }
                else if (act != null && act.EndTurn)
                {
                    act = turnQueue.Dequeue().GetComponent<ITurn>();
                    if (act.IsAlive) act.OnAttack();
                }
                yield return null;
            }
            if ((!players[0].IsAlive && !players[1].IsAlive && !players[2].IsAlive) || (!enemys[0].IsAlive && !enemys[1].IsAlive && !enemys[2].IsAlive))
            {
                break;
            }
        }
    }

    private void SetQueue()
    {
        GameObject[] turnObjects = new GameObject[players.Length + enemys.Length];
        float[] turnSpeed = new float[players.Length + enemys.Length];
        for (int i = 0; i< players.Length; i++)
        {
            turnObjects[i] = players[i].gameObject;
            turnSpeed[i] = players[i].speed;
        }
        for(int i = 0; i < enemys.Length; i++)
        {
            turnObjects[i + players.Length] = enemys[i].gameObject;
            turnSpeed[i + players.Length] = enemys[i].speed;
        }
        for(int i = 0; i < turnSpeed.Length; i++)
        {
            for(int j = 1; j < turnSpeed.Length;j++)
            {
                if (turnSpeed[i] < turnSpeed[j])
                {
                    (turnObjects[j], turnObjects[i]) = (turnObjects[i], turnObjects[j]);
                    (turnSpeed[j], turnSpeed[i]) = (turnSpeed[i], turnSpeed[j]);
                }
            }
        }
        for(int i = 0; i < turnObjects.Length; i++)
        {
            turnAct[i] = turnObjects[i].gameObject.GetComponent<ITurn>();
            if (turnAct[i].IsAlive)
            {
                turnQueue.Enqueue(turnObjects[i]);
            }
        }
    }
}
