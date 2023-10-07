using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public bool firstGame = true;
    public bool turn1Clear = false;
    public bool defence1Clear = false;

    public string player1Name;
    public string player2Name;
    public string player3Name;

    public PlayerType player1Type;
    public PlayerType player2Type;
    public PlayerType player3Type;

    public Sprite player1Sprite;
    public Sprite player2Sprite;
    public Sprite player3Sprite;

    public void Player1Data(PlayerType selectedType, string selectedName, Sprite selectedImage)
    {
        player1Name = selectedName;
        player1Type = selectedType;
        player1Sprite = selectedImage;
    }
    public void Player2Data(PlayerType selectedType, string selectedName, Sprite selectedImage)
    {
        player2Name = selectedName;
        player2Type = selectedType;
        player2Sprite = selectedImage;
    }
    public void Player3Data(PlayerType selectedType, string selectedName, Sprite selectedImage)
    {
        player3Name = selectedName;
        player3Type = selectedType;
        player3Sprite = selectedImage;
    }

    public int money;

    InvenSlot[] inven;

    public void OnInvenChange()
    {
        inven = Inventory.slots;
    }

    // 인벤 저장
}
