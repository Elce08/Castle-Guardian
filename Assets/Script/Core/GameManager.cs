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
    Head,
    Body,
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

    GameObject[] player1;
    GameObject[] player2;
    GameObject[] player3;

    public GameObject[] playerTypePrefabs;

    private void Start()
    {
        DontDestroyOnLoad(this);
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
        SetPlayerShape();
    }

    void SetPlayerShape()
    {
        player1 = GameObject.FindGameObjectsWithTag("Player1");
        player2 = GameObject.FindGameObjectsWithTag("Player2");
        player3 = GameObject.FindGameObjectsWithTag("Player3");
        for (int i = 0; i < player1.Length; i++)
        {
            PlayerBase player;
            player = player1[i].GetComponent<PlayerBase>();
            SetShape(player1[i], player);
        }
        for (int i = 0; i < player2.Length; i++)
        {
            PlayerBase player;
            player = player2[i].GetComponent<PlayerBase>();
            SetShape(player2[i], player);
        }
        for (int i = 0; i < player3.Length; i++)
        {
            PlayerBase player;
            player = player3[i].GetComponent<PlayerBase>();
            SetShape(player3[i], player);
        }
    }

    void SetShape(GameObject obj, PlayerBase player)
    {
        switch(player.playerData.playerType)
        {
            case PlayerType.None:
                break;
            case PlayerType.Archor:
                GameObject.Instantiate(playerTypePrefabs[0], player.transform.position, Quaternion.identity).transform.parent = obj.transform;
                break;
            case PlayerType.Archor_LongBow:
                GameObject.Instantiate(playerTypePrefabs[1], player.transform.position, Quaternion.identity).transform.parent = obj.transform;
                break;
            case PlayerType.Gunner:
                GameObject.Instantiate(playerTypePrefabs[2], player.transform.position, Quaternion.identity).transform.parent = obj.transform;
                break;
            case PlayerType.Soldier_LongSword:
                GameObject.Instantiate(playerTypePrefabs[3], player.transform.position, Quaternion.identity).transform.parent = obj.transform;
                break;
            case PlayerType.Soldier_ShortSword:
                GameObject.Instantiate(playerTypePrefabs[4], player.transform.position, Quaternion.identity).transform.parent = obj.transform;
                break;
            case PlayerType.Warrior_Hammer:
                GameObject.Instantiate(playerTypePrefabs[5], player.transform.position, Quaternion.identity).transform.parent = obj.transform;
                break;
        }
    }
}
