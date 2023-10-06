using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public bool firstGame = true;

    public string player1Name;
    public string player2Name;
    public string player3Name;

    public PlayerType player1Type;
    public PlayerType player2Type;
    public PlayerType player3Type;

    public Sprite player1Sprite;
    public Sprite player2Sprite;
    public Sprite player3Sprite;

    public int money;

    InvenSlot[] inven;

    public void OnInvenChange()
    {
        inven = Inventory.slots;
    }

    // 플레이어 타입, 이름, 돈, 몇번째 실행인지
}
