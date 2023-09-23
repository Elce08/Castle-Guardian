using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceManager : MonoBehaviour
{
    public System.Action LosingPoint;
    public System.Action WinPoint;

    public int winPoint = 50;
    public int losingPoint = 3;

    public ResultUI result;

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
        if (win)
        {
            result.Win();
        }
        else
        {
            result.Lose();
        }
    }
}
