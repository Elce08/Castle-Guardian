using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceManager : MonoBehaviour
{
    public System.Action LosingPoint;
    public System.Action WinPoint;

    public int winPoint;
    public int losingPoint;

    private void Start()
    {
        LosingPoint = Lose;
        WinPoint = Win;
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
        if (win)
        {
            // 승리 창
            // 아이템 드랍
        }
        else
        {
            // 패배 창
        }
    }
}
