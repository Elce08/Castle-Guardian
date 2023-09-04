using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public float spawnXPosition;

    public float moveSpeed;

    bool isStopped = false;

    public Vector3[] enemysPosition;
    public Vector3[] playersPosition;

    TurnEnemyBase[] enemys;
    TurnPlayerBase[] players;

    private void Start()
    {
        for(int i = 0; i < enemys.Length; i++)
        {
            enemys[i] = EnemySpawn(enemysPosition[i]);
        }
        players[0] = GameObject.Find("Player1").GetComponent<TurnPlayerBase>();
        players[1] = GameObject.Find("Player2").GetComponent<TurnPlayerBase>();
        players[2] = GameObject.Find("Player3").GetComponent<TurnPlayerBase>();
        for(int  i = 0; i < players.Length; i++)
        {
            players[i].transform.position = new(-spawnXPosition, players[i].transform.position.y, players[i].transform.position.z);
        }
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
    private void Update()
    {
        for(int i = 0; i< enemys.Length; i++)
        {
            enemys[i].transform.position = Vector3.MoveTowards(enemysPosition[i], enemys[i].transform.position, moveSpeed);
        }
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = Vector3.MoveTowards(enemysPosition[i], players[i].transform.position, moveSpeed);
        }
        isStopped = Arrived(enemys, players);
    }

    bool Arrived(TurnEnemyBase[] enemys, TurnPlayerBase[] players)
    {
        bool[] num = new bool[6];
        for(int i = 0; i < enemys.Length; i++)
        {
            if((enemys[i].transform.position.x - enemysPosition[i].x) <= 0.001)
            {
                num[i] = true;
            }
        }
        for(int i = 0; i < players.Length;i++)
        {
            if ((players[i].transform.position.x - enemysPosition[i].x) <= 0.001)
            {
                num[i + 3] = true;
            }
        }
        if (num[0] && num[1] && num[2] && num[3] && num[4] && num[5]) return true;
        else return false;
    }
}
