using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public struct SpawnData
    {
        public SpawnData(PoolObjectType type = PoolObjectType.DefenceGoblinBerserker, float interval = 0.5f)
        {
            this.spawnType = type;
            this.interval = interval;
        }
        public PoolObjectType spawnType;

        public float interval;
    }

    public SpawnData[] spawnDatas;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    DefenceEnemyBase Spawn(PoolObjectType spawnType)
    {
        GameObject obj = Factory.Inst.GetObject(spawnType,this.transform.position);
        DefenceEnemyBase enemy = obj.GetComponent<DefenceEnemyBase>();
        return enemy;
    }

    IEnumerator SpawnCoroutine()
    {
        SpawnData data;
        while (true)
        {
            int i = UnityEngine.Random.Range(0, spawnDatas.Length);
            data = spawnDatas[i];
            yield return new WaitForSeconds(data.interval);
            Spawn(data.spawnType);
        }
    }
}
