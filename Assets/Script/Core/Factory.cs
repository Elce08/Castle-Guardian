using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public enum PoolObjectType
{
    DefencePlayer1,
    DefencePlayer2,
    DefencePlayer3,
    DefenceGoblinBerserker,
    DefenceGoblinmagician,
    DefenceGoblinwarrior,
    DefenceSkeletonArcher,
    DefenceSkeletonWarrior,
    DefenceSkeletonWizard,
    TurnGoblinBerserker,
    TurnGoblinmagician,
    TurnGoblinwarrior,
    TurnSkeletonArcher,
    TurnSkeletonWarrior,
    TurnSkeletonWizard,
}

public class Factory : Singleton<Factory>
{
    DefencePlayer1Pool defencePlayer1Pool;
    DefencePlayer2Pool defencePlayer2Pool;
    DefencePlayer3Pool defencePlayer3Pool;
    DefenceGoblinBerserkerPool defenceGoblinBerserkerPool;
    DefenceGoblinmagicianPool defenceGoblinmagicianPool;
    DefenceGoblinwarriorPool defenceGoblinwarriorPool;
    DefenceSkeletonArcherPool defenceSkeletonArcherPool;
    DefenceSkeletonWarriorPool defenceSkeletonWarriorPool;
    DefenceSkeletonWizardPool defenceSkeletonWizardPool;
    TurnGoblinBerserkerPool turnGoblinBerserkerPool;
    TurnGoblinmagicianPool turnGoblinmagicianPool;
    TurnGoblinwarriorPool turnGoblinwarriorPool;
    TurnSkeletonArcherPool turnSkeletonArcherPool;
    TurnSkeletonWarriorPool turnSkeletonWarriorPool;
    TurnSkeletonWizardPool turnSkeletonWizardPool;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        defencePlayer1Pool = GetComponentInChildren<DefencePlayer1Pool>();
        defencePlayer2Pool = GetComponentInChildren<DefencePlayer2Pool>();
        defencePlayer3Pool = GetComponentInChildren<DefencePlayer3Pool>();
        defenceGoblinBerserkerPool = GetComponentInChildren<DefenceGoblinBerserkerPool>();
        defenceGoblinmagicianPool = GetComponentInChildren<DefenceGoblinmagicianPool>();
        defenceGoblinwarriorPool = GetComponentInChildren<DefenceGoblinwarriorPool>();
        defenceSkeletonArcherPool = GetComponentInChildren<DefenceSkeletonArcherPool>();
        defenceSkeletonWarriorPool = GetComponentInChildren<DefenceSkeletonWarriorPool>();
        defenceSkeletonWizardPool = GetComponentInChildren<DefenceSkeletonWizardPool>();
        turnGoblinBerserkerPool = GetComponentInChildren<TurnGoblinBerserkerPool>();
        turnGoblinmagicianPool = GetComponentInChildren<TurnGoblinmagicianPool>();
        turnGoblinwarriorPool = GetComponentInChildren<TurnGoblinwarriorPool>();
        turnSkeletonArcherPool = GetComponentInChildren<TurnSkeletonArcherPool>();
        turnSkeletonWarriorPool = GetComponentInChildren<TurnSkeletonWarriorPool>();
        turnSkeletonWizardPool = GetComponentInChildren<TurnSkeletonWizardPool>();

        defencePlayer1Pool?.Initialize();
        defencePlayer2Pool?.Initialize();
        defencePlayer3Pool?.Initialize();
        defenceGoblinBerserkerPool?.Initialize();
        defenceGoblinmagicianPool?.Initialize();
        defenceGoblinwarriorPool?.Initialize();
        defenceSkeletonArcherPool?.Initialize();
        defenceSkeletonWarriorPool?.Initialize();
        defenceSkeletonWizardPool?.Initialize();
        turnGoblinBerserkerPool?.Initialize();
        turnGoblinmagicianPool?.Initialize();
        turnGoblinwarriorPool?.Initialize();
        turnSkeletonArcherPool?.Initialize();
        turnSkeletonWarriorPool?.Initialize();
        turnSkeletonWizardPool?.Initialize();
    }

    public GameObject GetObject(PoolObjectType? type)
    {
        if(type != null)
        {
            GameObject result = type switch
            {
                PoolObjectType.DefencePlayer1 => defencePlayer1Pool?.GetObject()?.gameObject,
                PoolObjectType.DefencePlayer2 => defencePlayer2Pool?.GetObject()?.gameObject,
                PoolObjectType.DefencePlayer3 => defencePlayer3Pool?.GetObject()?.gameObject,
                PoolObjectType.DefenceGoblinBerserker => defenceGoblinBerserkerPool?.GetObject()?.gameObject,
                PoolObjectType.DefenceGoblinmagician => defenceGoblinmagicianPool?.GetObject()?.gameObject,
                PoolObjectType.DefenceGoblinwarrior => defenceGoblinwarriorPool?.GetObject()?.gameObject,
                PoolObjectType.DefenceSkeletonArcher => defenceSkeletonArcherPool?.GetObject()?.gameObject,
                PoolObjectType.DefenceSkeletonWarrior => defenceSkeletonWarriorPool?.GetObject()?.gameObject,
                PoolObjectType.DefenceSkeletonWizard => defenceSkeletonWizardPool?.GetObject()?.gameObject,
                PoolObjectType.TurnGoblinBerserker => turnGoblinBerserkerPool?.GetObject()?.gameObject,
                PoolObjectType.TurnGoblinmagician => turnGoblinmagicianPool?.GetObject()?.gameObject,
                PoolObjectType.TurnGoblinwarrior => turnGoblinwarriorPool?.GetObject()?.gameObject,
                PoolObjectType.TurnSkeletonArcher => turnSkeletonArcherPool?.GetObject()?.gameObject,
                PoolObjectType.TurnSkeletonWarrior => turnSkeletonWarriorPool?.GetObject()?.gameObject,
                PoolObjectType.TurnSkeletonWizard => turnSkeletonWizardPool?.GetObject()?.gameObject,
                _ => new GameObject(),
            };
            return result;
        }
        else return null;
    }

    public GameObject GetObject(PoolObjectType? type, Vector3 position)
    {
        GameObject obj = GetObject(type);
        obj.transform.position = position;
        return obj;
    }
}
