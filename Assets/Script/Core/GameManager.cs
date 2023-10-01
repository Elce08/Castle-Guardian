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

public enum WeaponType
{
    None,
    Armor,
    Pants,
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
    Armor,
    Pants,
    Archor1,
    Archor2,
    Archor_LongBow1,
    Archor_LongBow2,
    Gunner1,
    Gunner2,
    Soldier_LongSword1,
    Soldier_LongSword2,
    Soldier_ShortSword1,
    Soldier_ShortSword2,
    Warrior_Hammer1,
    Warrior_Hammer2,
}

public class GameManager : Singleton<GameManager>
{
    public GameObject[] playerTypePrefabs;
    bool gameStop = false;
    PlayerInputActions inputActions;
    Canvas settingCanvas;
    Button resume;
    Button controll;
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

    Scene currentScene;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        settingCanvas = GetComponentInChildren<Canvas>();
        Transform getChild = gameObject.transform.GetChild(0).GetChild(0);
        Transform child = getChild.gameObject.transform.GetChild(0);
        resume = child.GetComponent<Button>();
        child = getChild.gameObject.transform.GetChild(1);
        controll = child.GetComponent<Button>();
        child = getChild.gameObject.transform.GetChild(2);
        sound = child.GetComponent<Button>();
        child = getChild.gameObject.transform.GetChild(3);
        quit = child.GetComponent<Button>();

        // 인벤토리 관련
        /*inventoryUI = FindObjectOfType<InventoryUI>();
        partSlot = new InvenSlot[Enum.GetValues(typeof(WeaponType)).Length];*/

        PlayerSelectUI.sprites = playerImages;
    }


    private void Start()
    {
        // 인벤토리 관련
        /*inven = new Inventory(this);
        if (GameManager.Inst.InvenUi != null)
        {
            GameManager.Inst.InvenUi.InitializeInventory(inven);
        }*/

        DontDestroyOnLoad(this);
        onPlayer1Change = Player1Data;
        onPlayer2Change = Player2Data;
        onPlayer3Change = Player3Data;
        player1Sprite = PlayerImage(player1Type);
        player2Sprite = PlayerImage(player2Type);
        player3Sprite = PlayerImage(player3Type);
        settingCanvas.transform.GetChild(0).gameObject.SetActive(false);
        resume.onClick.AddListener(ResumeButton);
        controll.onClick.AddListener(Controll);
        sound.onClick.AddListener(SoundButton);
        quit.onClick.AddListener(QuitButton);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
        inputActions.GameManager.Disable();
        inputActions.GameManager.Esc.performed -= GameSetting;
    }

    void OnSceneLoad(UnityEngine.SceneManagement.Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "Defence1" || scene.name == "Defence2")
        {
            currentScene = Scene.Defence;
            foreach (GameObject s in playerTypePrefabs)
            {
                s.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
        }
        else if (scene.name == "Turn1" || scene.name == "Turn2")
        {
            currentScene = Scene.Turn;
            foreach (GameObject s in playerTypePrefabs)
            {
                s.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }
        else if(scene.name == "PlayerSelect")
        {
            currentScene = Scene.PlayerSelect;
            inputActions.GameManager.Enable();
            inputActions.GameManager.Esc.performed += GameSetting;
        }
    }

    // 세팅메뉴 =================================================

    private void GameSetting(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        if (!gameStop)
        {
            gameStop = true;
            settingCanvas.transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            if(currentScene == Scene.Turn) TurnPlayerBase.Stop(true);
            else if(currentScene == Scene.Defence) SpawnPlayer.Stop(true);
            else if(currentScene == Scene.PlayerSelect) PlayerSelectUI.Stop(true);
        }
        else if (gameStop)
        {
            gameStop = false;
            settingCanvas.transform.GetChild(0).gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            if (currentScene == Scene.Turn) TurnPlayerBase.Stop(false);
            else if (currentScene == Scene.Defence) SpawnPlayer.Stop(false);
            else if (currentScene == Scene.PlayerSelect) PlayerSelectUI.Stop(false);
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

    private void Controll()
    {
        // 키세팅 창 열리게(미구현 예정)
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

    // 아이템------------------------------------------

    int money = 0;

    public int Money
    {
        get => money;
        set
        {
            if(money != value)
            {
                money = value;
                onMoneyChange?.Invoke(money);
            }
        }
    }

    public Action<int> onMoneyChange;

    PlayerWeapon playerWeapon;

    ItemDataManager itemDataManager;

    public ItemDataManager ItemData => itemDataManager;

    /*Inventory inven;

    public Inventory Inventory => inven;

    public InvenSlot[] partSlot;

    public InvenSlot this[WeaponType part] => partSlot[(int)part];

    InventoryUI inventoryUI;
    
    public InventoryUI InvenUi => inventoryUI;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void ResultGetItem()
    {
        int getItem = UnityEngine.Random.Range(0, 2);

        switch (getItem)
        {
            case 0:
                playerWeapon = PlayerWeapon.None;
                break;
            case 1:
                playerWeapon = PlayerWeapon.Armor;
                break;
            case 2:
                playerWeapon = PlayerWeapon.Pants;
                break;
            case 3:
                playerWeapon = PlayerWeapon.Archor1;
                break;
            case 4:
                playerWeapon = PlayerWeapon.Archor2;
                break;
            case 6:
                playerWeapon = PlayerWeapon.Archor_LongBow1;
                break;
            case 7:
                playerWeapon = PlayerWeapon.Archor_LongBow2;
                break;
            case 8:
                playerWeapon = PlayerWeapon.Gunner1;
                break;
            case 9:
                playerWeapon = PlayerWeapon.Gunner2;
                break;
            case 10:
                playerWeapon = PlayerWeapon.Soldier_LongSword1;
                break;
            case 11:
                playerWeapon = PlayerWeapon.Soldier_LongSword2;
                break;
            case 12:
                playerWeapon = PlayerWeapon.Soldier_ShortSword1;
                break;
            case 13:
                playerWeapon = PlayerWeapon.Soldier_ShortSword2;
                break;
            case 14:
                playerWeapon = PlayerWeapon.Warrior_Hammer1;
                break;
            case 15:
                playerWeapon = PlayerWeapon.Warrior_Hammer2;
                break;
        }
        inven.AddItem(playerWeapon);     // 즉시 소비가능한 아이템이 아니면 아이템 추가 시도
    }*/
}
