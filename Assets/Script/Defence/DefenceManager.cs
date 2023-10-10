using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DefenceManager : MonoBehaviour
{
    GameManager gameManager;

    public System.Action LosingPoint;
    public System.Action WinPoint;

    CountUI countUI;

    public int winPoint = 50;
    public int losingPoint = 30;

    public ResultUI result;

    public System.Action gameEnd;

    public SpawnPlayer spawn;
    public TextMeshProUGUI moneyText;
    public float player1Cost;
    public float player2Cost;
    public float player3Cost;

    public float startMoney = 200.0f;
    float money = 0.0f;

    public float Money
    {
        get => money;
        set
        {
            if(money != value)
            {
                money = value;
                moneyText.text = $"{money} G";
                if(money >= player1Cost)
                {
                    spawn.buttons[0].interactable = true ;
                    spawn.buttons[0].image.color = Color.white;
                }
                if(money >= player2Cost)
                {
                    spawn.buttons[1].interactable = true ;
                    spawn.buttons[1].image.color = Color.white;
                }
                if(money >= player3Cost)
                {
                    spawn.buttons[2].interactable = true ;
                    spawn.buttons[2].image.color = Color.white;
                }
            }
        }
    }

    private void Awake()
    {
        countUI = FindObjectOfType<CountUI>();
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        LosingPoint = Lose;
        WinPoint = Win;
        result.gameObject.SetActive(false);
        foreach(UnityEngine.UI.Button spawnButton in spawn.buttons)
        {
            spawnButton.interactable = false;
            spawnButton.image.color = new(1, 1, 1, 0.5f);
        }
        player1Cost = SetCost(gameManager.player1Type);
        player2Cost = SetCost(gameManager.player2Type);
        player3Cost = SetCost(gameManager.player3Type);
        countUI.LifeCountChange(losingPoint);
        countUI.KillCountChange(winPoint);
        Money = startMoney;
        StartCoroutine(AddMoney());
    }

    void Win()
    {
        winPoint--;
        Money += 10.0f;
        if (winPoint <= 0)
        {
            EndGame(true);
            gameManager.defence1Clear = true;
            winPoint = 0;
        }
        countUI.KillCountChange(winPoint);
    }

    void Lose()
    {
        losingPoint--;
        if( losingPoint <= 0)
        {
            EndGame(false);
            losingPoint = 0;
        }
        countUI.LifeCountChange(losingPoint);
    }

    void EndGame(bool win)
    {
        result.gameObject.SetActive(true);
        Time.timeScale = 0;
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

    public float SetCost(PlayerType type)
    {
        float cost = 0.0f;
        switch (type)
        {
            case PlayerType.None:
                break;
            case PlayerType.Archor:
                cost = 60.0f;
                break;
            case PlayerType.Archor_LongBow:
                cost = 70.0f;
                break;
            case PlayerType.Gunner:
                cost = 50.0f;
                break;
            case PlayerType.Soldier_LongSword:
                cost = 20.0f;
                break;
            case PlayerType.Soldier_ShortSword:
                cost = 40.0f;
                break;
            case PlayerType.Warrior_Hammer:
                cost = 30.0f;
                break;
        }
        return cost;
    }

    IEnumerator AddMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Money += 10.0f;
        }
    }
}
