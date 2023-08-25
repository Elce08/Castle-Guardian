using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectUI : MonoBehaviour
{
    GameManager gameManager;
    Image image;
    TextMeshProUGUI playerNumber;
    InputField changeName;
    TextMeshProUGUI explanation;
    Button lastCheck;

    Button archor;
    Button archor_LongBow;
    Button gunner;
    Button soldier_LongSword;
    Button solder_ShorSword;
    Button Warrior;

    public Sprite[] sprites;

    string name;
   public  int type;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Transform grandChild = child.GetChild(0);
        image = grandChild.GetComponent<Image>();
        grandChild = child.GetChild(1);
        playerNumber = grandChild.GetComponent<TextMeshProUGUI>();
        grandChild = child.GetChild(2);
        changeName = grandChild.GetComponent<InputField>();
        grandChild = child.GetChild(3);
        explanation = grandChild.GetComponent<TextMeshProUGUI>();
        grandChild = child.GetChild(4);
        lastCheck = grandChild.GetComponent<Button>();
        child = transform.GetChild(1);
        archor = child.GetComponent<Button>();
        child = transform.GetChild(2);
        archor_LongBow = child.GetComponent<Button>();
        child = transform.GetChild(3);
        gunner = child.GetComponent<Button>();
        child = transform.GetChild(4);
        soldier_LongSword = child.GetComponent<Button>();
        child = transform.GetChild(5);
        solder_ShorSword = child.GetComponent<Button>();
        child = transform.GetChild(6);
        Warrior = child.GetComponent<Button>();
    }

    private void Start()
    {
        lastCheck.gameObject.SetActive(false);
        Player1Setting();
    }

    void Player1Setting()
    {
        playerNumber.text = "Player1";
        name = changeName.text;
        PlayerCharacterSelect();
        if(type != 0 && name.Length != 0)
        {
            lastCheck.gameObject.SetActive(true);
            lastCheck.onClick.AddListener(Player1Selected);
        }
    }
    void Player2Setting()
    {
        playerNumber.text = "Player2";
        name = changeName.text;
        PlayerCharacterSelect();
        if(type != 0 && name.Length != 0)
        {
            lastCheck.gameObject.SetActive(true);
            lastCheck.onClick.AddListener(Player2Selected);
        }
    }
    void Player3Setting()
    {
        playerNumber.text = "Player3";
        name = changeName.text;
        PlayerCharacterSelect();
        if(type != 0 && name.Length != 0)
        {
            lastCheck.gameObject.SetActive(true);
            lastCheck.onClick.AddListener(Player3Selected);
        }
    }

    void Player1Selected()
    {
        PlayerType selectedType = PlayerType.None;
        switch(type)
        {
            case 1:
                selectedType = PlayerType.Archor;
                gameManager.onPlayer1Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 2:
                selectedType = PlayerType.Archor_LongBow;
                gameManager.onPlayer1Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 3:
                selectedType = PlayerType.Gunner;
                gameManager.onPlayer1Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 4:
                selectedType = PlayerType.Soldier_LongSword;
                gameManager.onPlayer1Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 5:
                selectedType = PlayerType.Soldier_ShortSword;
                gameManager.onPlayer1Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 6:
                selectedType = PlayerType.Warrior_Hammer;
                gameManager.onPlayer1Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
        }
        Player2Setting();
    }

    void Player2Selected()
    {
        PlayerType selectedType = PlayerType.None;
        switch(type)
        {
            case 1:
                selectedType = PlayerType.Archor;
                gameManager.onPlayer2Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 2:
                selectedType = PlayerType.Archor_LongBow;
                gameManager.onPlayer2Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 3:
                selectedType = PlayerType.Gunner;
                gameManager.onPlayer2Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 4:
                selectedType = PlayerType.Soldier_LongSword;
                gameManager.onPlayer2Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 5:
                selectedType = PlayerType.Soldier_ShortSword;
                gameManager.onPlayer2Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 6:
                selectedType = PlayerType.Warrior_Hammer;
                gameManager.onPlayer2Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
        }
        Player3Setting();
    }

    void Player3Selected()
    {
        PlayerType selectedType = PlayerType.None;
        switch(type)
        {
            case 1:
                selectedType = PlayerType.Archor;
                gameManager.onPlayer3Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 2:
                selectedType = PlayerType.Archor_LongBow;
                gameManager.onPlayer3Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 3:
                selectedType = PlayerType.Gunner;
                gameManager.onPlayer3Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 4:
                selectedType = PlayerType.Soldier_LongSword;
                gameManager.onPlayer3Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 5:
                selectedType = PlayerType.Soldier_ShortSword;
                gameManager.onPlayer3Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
            case 6:
                selectedType = PlayerType.Warrior_Hammer;
                gameManager.onPlayer3Change(selectedType, name);
                lastCheck.gameObject.SetActive(false);
                break;
        }
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
        type = 1;
        image.sprite = sprites[0];
    }
    void SelectArchor_LongBow()
    {
        type = 2;
        image.sprite = sprites[1];
    }
    void SelectGunner()
    {
        type = 3;
        image.sprite = sprites[2];
    }
    void SelectSoldier_LongSword()
    {
        type = 4;
        image.sprite = sprites[3];
    }
    void SelectSolder_ShorSword()
    {
        type = 5;
        image.sprite = sprites[4];
    }
    void SelectWarrior()
    {
        type = 6;
        image.sprite = sprites[5];
    }
}
