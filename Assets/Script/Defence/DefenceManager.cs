using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceManager : MonoBehaviour
{
    public System.Action LosingPoint;
    public System.Action WinPoint;

    public int winPoint = 3;
    public int losingPoint = 3;

    public ResultUI result;

    public System.Action gameEnd;

    private void Start()
    {
        LosingPoint = Lose;
        WinPoint = Win;
        result.gameObject.SetActive(false);
    }

    void Win()
    {
        winPoint--;
        if (winPoint <= 0)
        {
            EndGame(true);
        }
    }

    void Lose()
    {
        losingPoint--;
        if( losingPoint <= 0)
        {
            EndGame(false);
        }
    }

    void EndGame(bool win)
    {
        result.gameObject.SetActive(true);
        PooledObject[] pooledObjects = FindObjectsOfType<PooledObject>();
        foreach(PooledObject obj in pooledObjects)
        {
            obj.gameObject.SetActive(false);
        }
        if (win)
        {
            result.Win();
        }
        else
        {
            result.Lose();
        }
        gameEnd?.Invoke();
    }
}
