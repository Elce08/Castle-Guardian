using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public float selectType1Str = 0;
    public float selectType1Def = 0;
    public float selectType1HP = 0;
    public float selectType1MP = 0;
    public float selectType1Speed = 0;

    public float selectType2Str = 0;
    public float selectType2Def = 0;
    public float selectType2HP = 0;
    public float selectType2MP = 0;
    public float selectType2Speed = 0;

    public float selectType3Str = 0;
    public float selectType3Def = 0;
    public float selectType3HP = 0;
    public float selectType3MP = 0;
    public float selectType3Speed = 0;
    
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
        Transform child = getChild.gameObject.transform.GetChild(1);
        resume = child.GetComponent<Button>();
        child = getChild.gameObject.transform.GetChild(2);
        controll = child.GetComponent<Button>();
        child = getChild.gameObject.transform.GetChild(3);
        sound = child.GetComponent<Button>();
        child = getChild.gameObject.transform.GetChild(4);
        quit = child.GetComponent<Button>();
        resume.onClick.AddListener(Setting);
        controll.onClick.AddListener(Controll);
        sound.onClick.AddListener(SoundButton);
        quit.onClick.AddListener(QuitButton);

        PlayerSelectUI.sprites = playerImages;
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
        settingCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.activeSceneChanged += OnSceneChange;
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
                s.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
            spawnPlayer = FindObjectOfType<SpawnPlayer>();
        }
        else if (scene.name == "Turn1" || scene.name == "Turn2")
        {
            currentScene = Scene.Turn;
            foreach (GameObject s in playerTypePrefabs)
            {
                s.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            turnPlayerBase = FindObjectOfType<TurnPlayerBase>();
            //turnPlayerBase.TurnCheck();
        }
        else if(scene.name == "PlayerSelect")
        {
            currentScene = Scene.PlayerSelect;
            inputActions.GameManager.Enable();
            inputActions.GameManager.Esc.performed += GameSetting;
            playerSelectUI = FindObjectOfType<PlayerSelectUI>();
        }
        else if( scene.name == "Village")
        {
            GameObject turn2Button = GameObject.Find("Turn2");
            GameObject defence2Button = GameObject.Find("Defence2");

            // 인벤토리 관련
            inventoryUI = FindObjectOfType<InventoryUI>();
            partSlot = new InvenSlot[Enum.GetValues(typeof(WeaponType)).Length];
            itemDataManager = GetComponent<ItemDataManager>();
            //인벤토리 관련
            inven = new Inventory(this);
            if (GameManager.Inst.InvenUI != null)
            {
                GameManager.Inst.InvenUI.InitializeInventory(inven);
            }
            switch (player1Type)
            {
                case PlayerType.Archor:
                    InvenUI.equipSlotsUI1[0].equipType = WeaponType.Archor;
                    selectType1Str = 4.0f;
                    selectType1Def = 1.0f;
                    selectType1HP = 85.0f;
                    selectType1MP = 100.0f;
                    selectType1Speed = 13.0f;
                    break;
                case PlayerType.Archor_LongBow:
                    InvenUI.equipSlotsUI1[0].equipType = WeaponType.Archor_LongBow;
                    selectType1Str = 5.0f;
                    selectType1Def = 1.0f;
                    selectType1HP = 80.0f;
                    selectType1MP = 100.0f;
                    selectType1Speed = 15.0f;
                    break;
                case PlayerType.Gunner:
                    InvenUI.equipSlotsUI1[0].equipType = WeaponType.Gunner;
                    selectType1Str = 5.0f;
                    selectType1Def = 1.0f;
                    selectType1HP = 90.0f;
                    selectType1MP = 100.0f;
                    selectType1Speed = 11.0f;
                    break;
                case PlayerType.Soldier_LongSword:
                    InvenUI.equipSlotsUI1[0].equipType = WeaponType.Soldier_LongSword;
                    selectType1Str = 7.0f;
                    selectType1Def = 2.0f;
                    selectType1HP = 150.0f;
                    selectType1MP = 100.0f;
                    selectType1Speed = 5.0f;
                    break;
                case PlayerType.Soldier_ShortSword:
                    InvenUI.equipSlotsUI1[0].equipType = WeaponType.Soldier_ShortSword;
                    selectType1Str = 6.0f;
                    selectType1Def = 1.5f;
                    selectType1HP = 120.0f;
                    selectType1MP = 100.0f;
                    selectType1Speed = 9.0f;
                    break;
                case PlayerType.Warrior_Hammer:
                    InvenUI.equipSlotsUI1[0].equipType = WeaponType.Warrior_Hammer;
                    selectType1Str = 7.0f;
                    selectType1Def = 1.5f;
                    selectType1HP = 120.0f;
                    selectType1MP = 100.0f;
                    selectType1Speed = 7.0f;
                    break;
            }
            switch (player2Type)
            {
                case PlayerType.Archor:
                    InvenUI.equipSlotsUI2[0].equipType = WeaponType.Archor;
                    selectType2Str = 4.0f;
                    selectType2Def = 1.0f;
                    selectType2HP = 85.0f;
                    selectType2MP = 100.0f;
                    selectType2Speed = 13.0f;
                    break;
                case PlayerType.Archor_LongBow:
                    InvenUI.equipSlotsUI2[0].equipType = WeaponType.Archor_LongBow;
                    selectType2Str = 5.0f;
                    selectType2Def = 1.0f;
                    selectType2HP = 80.0f;
                    selectType2MP = 100.0f;
                    selectType2Speed = 15.0f;
                    break;
                case PlayerType.Gunner:
                    InvenUI.equipSlotsUI2[0].equipType = WeaponType.Gunner;
                    selectType2Str = 5.0f;
                    selectType2Def = 1.0f;
                    selectType2HP = 90.0f;
                    selectType2MP = 100.0f;
                    selectType2Speed = 11.0f;
                    break;
                case PlayerType.Soldier_LongSword:
                    InvenUI.equipSlotsUI2[0].equipType = WeaponType.Soldier_LongSword;
                    selectType2Str = 7.0f;
                    selectType2Def = 2.0f;
                    selectType2HP = 150.0f;
                    selectType2MP = 100.0f;
                    selectType2Speed = 5.0f;
                    break;
                case PlayerType.Soldier_ShortSword:
                    InvenUI.equipSlotsUI2[0].equipType = WeaponType.Soldier_ShortSword;
                    selectType2Str = 6.0f;
                    selectType2Def = 1.5f;
                    selectType2HP = 120.0f;
                    selectType2MP = 100.0f;
                    selectType2Speed = 9.0f;
                    break;
                case PlayerType.Warrior_Hammer:
                    InvenUI.equipSlotsUI2[0].equipType = WeaponType.Warrior_Hammer;
                    selectType2Str = 7.0f;
                    selectType2Def = 1.5f;
                    selectType2HP = 120.0f;
                    selectType2MP = 100.0f;
                    selectType2Speed = 7.0f;
                    break;
            }
            switch (player3Type)
            {
                case PlayerType.Archor:
                    InvenUI.equipSlotsUI3[0].equipType = WeaponType.Archor;
                    selectType3Str = 4.0f;
                    selectType3Def = 1.0f;
                    selectType3HP = 85.0f;
                    selectType3MP = 100.0f;
                    selectType3Speed = 13.0f;
                    break;
                case PlayerType.Archor_LongBow:
                    InvenUI.equipSlotsUI3[0].equipType = WeaponType.Archor_LongBow;
                    selectType3Str = 5.0f;
                    selectType3Def = 1.0f;
                    selectType3HP = 80.0f;
                    selectType3MP = 100.0f;
                    selectType3Speed = 15.0f;
                    break;
                case PlayerType.Gunner:
                    InvenUI.equipSlotsUI3[0].equipType = WeaponType.Gunner;
                    selectType3Str = 5.0f;
                    selectType3Def = 1.0f;
                    selectType3HP = 90.0f;
                    selectType3MP = 100.0f;
                    selectType3Speed = 11.0f;
                    break;
                case PlayerType.Soldier_LongSword:
                    InvenUI.equipSlotsUI3[0].equipType = WeaponType.Soldier_LongSword;
                    selectType3Str = 7.0f;
                    selectType3Def = 2.0f;
                    selectType3HP = 150.0f;
                    selectType3MP = 100.0f;
                    selectType3Speed = 5.0f;
                    break;
                case PlayerType.Soldier_ShortSword:
                    InvenUI.equipSlotsUI3[0].equipType = WeaponType.Soldier_ShortSword;
                    selectType3Str = 6.0f;
                    selectType3Def = 1.5f;
                    selectType3HP = 120.0f;
                    selectType3MP = 100.0f;
                    selectType3Speed = 9.0f;
                    break;
                case PlayerType.Warrior_Hammer:
                    InvenUI.equipSlotsUI3[0].equipType = WeaponType.Warrior_Hammer;
                    selectType3Str = 7.0f;
                    selectType3Def = 1.5f;
                    selectType3HP = 120.0f;
                    selectType3MP = 100.0f;
                    selectType3Speed = 7.0f;
                    break;
            }
        }
    }

    private void OnSceneChange(UnityEngine.SceneManagement.Scene current, UnityEngine.SceneManagement.Scene next)
    {
        if (current.name == "Defence1" || current.name == "Defence2")
        {
            spawnPlayer = null;
        }
        else if (current.name == "Turn1" || current.name == "Turn2")
        {
            turnPlayerBase = null;
        }
        else if (current.name == "PlayerSelect")
        {
            playerSelectUI = null;
        }
    }

    // 세팅메뉴 =================================================

    SpawnPlayer spawnPlayer;
    TurnPlayerBase turnPlayerBase;
    PlayerSelectUI playerSelectUI;

    private void GameSetting(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        Setting();
    }

    private void Setting()
    {
        if (!gameStop)
        {
            gameStop = true;
            settingCanvas.transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            if (currentScene == Scene.Turn) turnPlayerBase.Stop(true);
            else if (currentScene == Scene.Defence) spawnPlayer.Stop(true);
            else if (currentScene == Scene.PlayerSelect) playerSelectUI.Stop(true);
        }
        else if (gameStop)
        {
            gameStop = false;
            settingCanvas.transform.GetChild(0).gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            if (currentScene == Scene.Turn) turnPlayerBase.Stop(false);
            else if (currentScene == Scene.Defence) spawnPlayer.Stop(false);
            else if (currentScene == Scene.PlayerSelect) playerSelectUI.Stop(false);
        }
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

    ItemDataManager itemDataManager;

    public ItemDataManager ItemData => itemDataManager;

    Inventory inven;

    public Inventory Inventory => inven;

    public InvenSlot[] partSlot;

    public InvenSlot this[WeaponType part] => partSlot[(int)part];

    InventoryUI inventoryUI;
    
    public InventoryUI InvenUI => inventoryUI;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
    }

    public void ResultGetItem()
    {
        inven.AddItem(PlayerWeapon.Armor);
        inven.AddItem(PlayerWeapon.Pants);
        inven.AddItem(PlayerWeapon.Armor);
        inven.AddItem(PlayerWeapon.Pants);
        inven.AddItem(PlayerWeapon.Armor);
        inven.AddItem(PlayerWeapon.Pants);
        inven.AddItem(PlayerWeapon.Archor1);
        inven.AddItem(PlayerWeapon.Archor2);
        inven.AddItem(PlayerWeapon.Archor_LongBow1);
        inven.AddItem(PlayerWeapon.Archor_LongBow2);
        inven.AddItem(PlayerWeapon.Gunner1);
        inven.AddItem(PlayerWeapon.Gunner2);
        inven.AddItem(PlayerWeapon.Soldier_LongSword1);
        inven.AddItem(PlayerWeapon.Soldier_LongSword2);
        inven.AddItem(PlayerWeapon.Soldier_ShortSword1);
        inven.AddItem(PlayerWeapon.Soldier_ShortSword2);
        inven.AddItem(PlayerWeapon.Warrior_Hammer1);
        inven.AddItem(PlayerWeapon.Warrior_Hammer2);
    }
}
