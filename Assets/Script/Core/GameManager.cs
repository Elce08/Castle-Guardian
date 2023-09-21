using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject[] playerTypePrefabs;
    bool gameStop = false;
    PlayerInputActions inputActions;
    Canvas settingCanvas;
    Button resume;
    Button setting;
    Button sound;
    Button quit;
    
    public enum Scene
    {
        Menu,
        PlayerSelect,
        Village,
        Turn,
        Defence,
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        settingCanvas = GetComponentInChildren<Canvas>();
        Transform getChild = gameObject.transform.GetChild(0);
        Transform child = getChild.gameObject.transform.GetChild(0);
        resume = child.GetComponent<Button>();
        child = getChild.gameObject.transform.GetChild(1);
        setting = child.GetComponent<Button>();
        child = getChild.gameObject.transform.GetChild(2);
        sound = child.GetComponent<Button>();
        child = getChild.gameObject.transform.GetChild(3);
        quit = child.GetComponent<Button>();
    }


    private void Start()
    {
        DontDestroyOnLoad(this);
        onPlayer1Change = Player1Data;
        onPlayer2Change = Player2Data;
        onPlayer3Change = Player3Data;
        player1Sprite = PlayerImage(player1Type);
        player2Sprite = PlayerImage(player2Type);
        player3Sprite = PlayerImage(player3Type);
        settingCanvas.gameObject.SetActive(false);
        resume.onClick.AddListener(ResumeButton);
        setting.onClick.AddListener(SettingButton);
        sound.onClick.AddListener(SoundButton);
        quit.onClick.AddListener(QuitButton);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        inputActions.GameManager.Enable();
        inputActions.GameManager.Esc.performed += GameSetting;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
        inputActions.GameManager.Disable();
        inputActions.GameManager.Esc.performed -= GameSetting;
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
                s.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
            }
        }
    }

    private void GameSetting(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        if (!gameStop)
        {
            Debug.Log("On");
            gameStop = true;
            settingCanvas.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            TurnPlayerBase.Stop(true);
        }
        else if (gameStop)
        {
            Debug.Log("Off");
            gameStop = false;
            settingCanvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            TurnPlayerBase.Stop(false);
        }
    }

    private void ResumeButton()
    {
        gameStop = false;
        settingCanvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        TurnPlayerBase.Stop(false);
    }

    private void SoundButton()
    {
        // 사운드 창 열리게(미구현 예정)
    }

    private void SettingButton()
    {
        // 세팅 창 열리게(미구현 예정)
    }

    private void QuitButton()
    {
        // 세이브까지 해주면 베스트
        // 게임 종료
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
