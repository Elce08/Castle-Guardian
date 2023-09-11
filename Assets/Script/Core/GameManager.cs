using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayerType
{
    None,
    Archor,
    Archor_LongBow,
    Gunner,
    Soldier_LongSword,
    Soldier_ShortSword,
    Warrior_Hammer,
}

public enum PlayerWeapon
{
    None,
    Bow,
    LongBow,
    Gun,
    LongSword,
    ShortSword,
    Hammer,
}

public enum Head
{
    None,
}

public enum Body
{
    None,
}

public class GameManager : MonoBehaviour
{
    public enum Scene
    {
        Menu,
        PlayerSelect,
        Village,
        Turn,
        Defence,
    }

    public GameObject[] playerTypePrefabs;


    private void Start()
    {
        DontDestroyOnLoad(this);
        onPlayer1Change = Player1Data;
        onPlayer2Change = Player2Data;
        onPlayer3Change = Player3Data;
        player1Sprite = PlayerImage(player1Type);
        player2Sprite = PlayerImage(player2Type);
        player3Sprite = PlayerImage(player3Type);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void OnSceneLoad(UnityEngine.SceneManagement.Scene scene, LoadSceneMode sceneMode)
    {
        if(scene.name == "Defence")
        {
            foreach(GameObject s in playerTypePrefabs)
            {
                s.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
        }
        if(scene.name == "Turn")
        {
            foreach (GameObject s in playerTypePrefabs)
            {
                s.transform.localScale = new Vector3(1f,1f,1f);
            }
        }
    }

    //플레이어 선택=============================================================

    public System.Action<PlayerType,string> onPlayer1Change;
    public System.Action<PlayerType,string> onPlayer2Change;
    public System.Action<PlayerType,string> onPlayer3Change;

    public string player1Name;
    public string player2Name;
    public string player3Name;

    public PlayerType player1Type;
    public PlayerType player2Type;
    public PlayerType player3Type;

    public Sprite[] playerImages;

    public Sprite player1Sprite;
    public Sprite player2Sprite;
    public Sprite player3Sprite;

    public void Player1Data(PlayerType selectedType, string selectedName)
    {
        Debug.Log("Sent");
        player1Name = selectedName;
        player1Type = selectedType;
        player1Sprite = PlayerImage(player1Type);
    }
    public void Player2Data(PlayerType selectedType, string selectedName)
    {
        player2Name = selectedName;
        player2Type = selectedType;
        player2Sprite = PlayerImage(player2Type);
    }
    public void Player3Data(PlayerType selectedType, string selectedName)
    {
        player3Name = selectedName;
        player3Type = selectedType;
        player3Sprite = PlayerImage(player3Type);
    }

    Sprite PlayerImage(PlayerType type)
    {
        Sprite result = null;
        switch (type)
        {
            case PlayerType.None:
                result = null;
                break;
            case PlayerType.Archor:
                result = playerImages[0];
                break;
            case PlayerType.Archor_LongBow:
                result = playerImages[1];
                break;
            case PlayerType.Gunner:
                result = playerImages[2];
                break;
            case PlayerType.Soldier_LongSword:
                result = playerImages[3];
                break;
            case PlayerType.Soldier_ShortSword:
                result = playerImages[4];
                break;
            case PlayerType.Warrior_Hammer:
                result = playerImages[5];
                break;
        }
        return result;
    }
}
