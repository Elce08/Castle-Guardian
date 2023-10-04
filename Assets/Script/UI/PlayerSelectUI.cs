using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelectUI : MonoBehaviour
{
    /// <summary>
    /// 확인버튼 껴졌다 켰을 때 오류 해결 용도
    /// </summary>
    enum CurrentState
    {
        None,
        Player1,
        Player2,
        Player3,
    }

    CurrentState state = CurrentState.None;
    CurrentState State
    {
        get => state;
        set
        {
            if(state != value)
            {
                state = value;
                switch (state)
                {
                    case CurrentState.None:
                        lastCheck.onClick.RemoveAllListeners();
                        break;
                    case CurrentState.Player1:
                        lastCheck.onClick.AddListener(Player1Selected); 
                        break;
                    case CurrentState.Player2:
                        lastCheck.onClick.AddListener(Player2Selected);
                        break;
                    case CurrentState.Player3:
                        lastCheck.onClick.AddListener(Player3Selected);
                        break;
                }
            }
        }
    }

    GameManager gameManager;
    Image image;
    TextMeshProUGUI playerNumber;
    TMP_InputField changeName;
    TextMeshProUGUI explanation;
    Button lastCheck;

    Button archor;
    Button archor_LongBow;
    Button gunner;
    Button soldier_LongSword;
    Button solder_ShorSword;
    Button Warrior;

    public static Sprite[] sprites;

    string playerName;
    public static int type;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Transform grandChild = child.GetChild(0);
        image = grandChild.GetComponent<Image>();
        grandChild = child.GetChild(1);
        playerNumber = grandChild.GetComponent<TextMeshProUGUI>();
        grandChild = child.GetChild(2);
        changeName = grandChild.GetComponentInChildren<TMP_InputField>();
        explanation = grandChild.GetComponent<TextMeshProUGUI>();
        grandChild = child.GetChild(4);
        lastCheck = grandChild.GetComponent<Button>();
        archor = transform.GetChild(2).GetComponent<Button>();
        archor_LongBow = transform.GetChild(3).GetComponent<Button>();
        gunner = transform.GetChild(4).GetComponent<Button>();
        soldier_LongSword = transform.GetChild(5).GetComponent<Button>();
        solder_ShorSword = transform.GetChild(6).GetComponent<Button>();
        Warrior = transform.GetChild(7).GetComponent<Button>();
        lastCheck.gameObject.SetActive(false);
    }

    private void Start()
    {
        Player1Setting();
    }

    void Player1Setting()
    {
        playerNumber.text = "Player1";
        playerName = "Chalie";
        changeName.onEndEdit.AddListener((text) => playerName = text);
        PlayerCharacterSelect();
        State = CurrentState.Player1;
        lastCheck.gameObject.SetActive(false);
    }
    void Player2Setting()
    {
        playerNumber.text = "Player2";
        playerName = "Cloe";
        changeName.onEndEdit.AddListener((text) => playerName = text);
        PlayerCharacterSelect();
        State = CurrentState.Player2;
        lastCheck.gameObject.SetActive(false);
    }
    void Player3Setting()
    {
        playerNumber.text = "Player3";
        playerName = "Hyeba";
        changeName.onEndEdit.AddListener((text) => playerName = text);
        PlayerCharacterSelect();
        State = CurrentState.Player3;
        lastCheck.gameObject.SetActive(false);
    }

    void Player1Selected()
    {
        PlayerType selectedType;
        switch (type)
        {
            case 1:
                selectedType = PlayerType.Archor;
                gameManager.onPlayer1Change.Invoke(selectedType, playerName);
                break;
            case 2:
                selectedType = PlayerType.Archor_LongBow;
                gameManager.onPlayer1Change.Invoke(selectedType, playerName);
                break;
            case 3:
                selectedType = PlayerType.Gunner;
                gameManager.onPlayer1Change.Invoke(selectedType, playerName);
                break;
            case 4:
                selectedType = PlayerType.Soldier_LongSword;
                gameManager.onPlayer1Change.Invoke(selectedType, playerName);
                break;
            case 5:
                selectedType = PlayerType.Soldier_ShortSword;
                gameManager.onPlayer1Change.Invoke(selectedType, playerName);
                break;
            case 6:
                selectedType = PlayerType.Warrior_Hammer;
                gameManager.onPlayer1Change.Invoke(selectedType, playerName);
                break;
        }
        State = CurrentState.None;
        Player2Setting();
        changeName.text = "Add Name";
        image.sprite = sprites[6];
    }

    void Player2Selected()
    {
        PlayerType selectedType;
        switch (type)
        {
            case 1:
                selectedType = PlayerType.Archor;
                gameManager.onPlayer2Change.Invoke(selectedType, playerName);
                break;
            case 2:
                selectedType = PlayerType.Archor_LongBow;
                gameManager.onPlayer2Change.Invoke(selectedType, playerName);
                break;
            case 3:
                selectedType = PlayerType.Gunner;
                gameManager.onPlayer2Change.Invoke(selectedType, playerName);
                break;
            case 4:
                selectedType = PlayerType.Soldier_LongSword;
                gameManager.onPlayer2Change.Invoke(selectedType, playerName);
                break;
            case 5:
                selectedType = PlayerType.Soldier_ShortSword;
                gameManager.onPlayer2Change.Invoke(selectedType, playerName);
                break;
            case 6:
                selectedType = PlayerType.Warrior_Hammer;
                gameManager.onPlayer2Change.Invoke(selectedType, playerName);
                break;
        }
        State = CurrentState.None;
        Player3Setting();
        changeName.text = "Add Name";
        image.sprite = sprites[6];
    }

    void Player3Selected()
    {
        PlayerType selectedType;
        switch (type)
        {
            case 1:
                selectedType = PlayerType.Archor;
                gameManager.onPlayer3Change.Invoke(selectedType, playerName);
                break;
            case 2:
                selectedType = PlayerType.Archor_LongBow;
                gameManager.onPlayer3Change.Invoke(selectedType, playerName);
                break;
            case 3:
                selectedType = PlayerType.Gunner;
                gameManager.onPlayer3Change.Invoke(selectedType, playerName);
                break;
            case 4:
                selectedType = PlayerType.Soldier_LongSword;
                gameManager.onPlayer3Change.Invoke(selectedType, playerName);
                break;
            case 5:
                selectedType = PlayerType.Soldier_ShortSword;
                gameManager.onPlayer3Change.Invoke(selectedType, playerName);
                break;
            case 6:
                selectedType = PlayerType.Warrior_Hammer;
                gameManager.onPlayer3Change.Invoke(selectedType, playerName);
                break;
        }
        LoadScene();
    }

    void PlayerCharacterSelect()
    {
        archor.onClick.AddListener(SelectArchor);
        archor_LongBow.onClick.AddListener(SelectArchor_LongBow);
        gunner.onClick.AddListener(SelectGunner);
        soldier_LongSword.onClick.AddListener(SelectSoldier_LongSword);
        solder_ShorSword.onClick.AddListener(SelectSolder_ShorSword);
        Warrior.onClick.AddListener(SelectWarrior);
    }

    void SelectArchor()
    {
        lastCheck.gameObject.SetActive(true);
        type = 1;
        image.sprite = sprites[0];
    }
    void SelectArchor_LongBow()
    {
        lastCheck.gameObject.SetActive(true);
        type = 2;
        image.sprite = sprites[1];
    }
    void SelectGunner()
    {
        lastCheck.gameObject.SetActive(true);
        type = 3;
        image.sprite = sprites[2];
    }
    void SelectSoldier_LongSword()
    {
        lastCheck.gameObject.SetActive(true);
        type = 4;
        image.sprite = sprites[3];
    }
    void SelectSolder_ShorSword()
    {
        lastCheck.gameObject.SetActive(true);
        type = 5;
        image.sprite = sprites[4];
    }
    void SelectWarrior()
    {
        lastCheck.gameObject.SetActive(true);
        type = 6;
        image.sprite = sprites[5];
    }

    void LoadScene()
    {
        AsyncLoad.OnSceneLoad("Village");
    }

    public void Stop(bool stop)
    {
        if(stop)
        {
            lastCheck.onClick.RemoveAllListeners();
            archor.onClick.RemoveAllListeners();
            archor_LongBow.onClick.RemoveAllListeners();
            gunner.onClick.RemoveAllListeners();
            soldier_LongSword.onClick.RemoveAllListeners();
            solder_ShorSword.onClick.RemoveAllListeners();
            Warrior.onClick.RemoveAllListeners();
        }
        else if (!stop)
        {
            switch (State)
            {
                case CurrentState.None:
                    lastCheck.onClick.RemoveAllListeners();
                    break;
                case CurrentState.Player1:
                    lastCheck.onClick.AddListener(Player1Selected);
                    break;
                case CurrentState.Player2:
                    lastCheck.onClick.AddListener(Player2Selected);
                    break;
                case CurrentState.Player3:
                    lastCheck.onClick.AddListener(Player3Selected);
                    break;
            }
            archor.onClick.AddListener(SelectArchor);
            archor_LongBow.onClick.AddListener(SelectArchor_LongBow);
            gunner.onClick.AddListener(SelectGunner);
            soldier_LongSword.onClick.AddListener(SelectSoldier_LongSword);
            solder_ShorSword.onClick.AddListener(SelectSolder_ShorSword);
            Warrior.onClick.AddListener(SelectWarrior);
        }
    }
}
