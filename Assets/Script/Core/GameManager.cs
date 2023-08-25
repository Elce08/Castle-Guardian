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

    PlayerSelectUI playerSelectUI;

    public void Player1Data(PlayerType selectedType, string selectedName)
    {
        player1Name = selectedName;
        player1Type = selectedType;
        playerSelectUI.type = 0;
        Debug.Log(player1Name);
    }
    public void Player2Data(PlayerType selectedType, string selectedName)
    {
        player1Name = selectedName;
        player1Type = selectedType;
        playerSelectUI.type = 0;
        Debug.Log(player1Name);
    }
    public void Player3Data(PlayerType selectedType, string selectedName)
    {
        player1Name = selectedName;
        player1Type = selectedType;
        playerSelectUI.type = 0;
        Debug.Log(player1Name);
    }
}
