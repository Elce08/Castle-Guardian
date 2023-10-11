using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public GameManager gameManager;

    TextMeshProUGUI goldAmount;

    Image item1Image;
    Image item2Image;

    TextMeshProUGUI resultText;

    PlayerInputActions inputActions;

    ItemData itemData;

    ItemDataManager itemDataManager;

    public PlayerWeapon resultItem;
    public PlayerWeapon resultItem2;
    public int resultGold;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        goldAmount = transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
        item1Image = transform.GetChild(3).GetComponent<Image>();
        item2Image = transform.GetChild(4).GetComponent<Image>();
        resultText = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        inputActions = new();
        itemDataManager = new ItemDataManager();
        itemDataManager = GameManager.Inst.ItemData;
        item2Image.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        inputActions.ResultUI.Enable();
        inputActions.ResultUI.Space.performed += Space_performed;
        inputActions.ResultUI.LeftClick.performed += LeftClick_performed;
    }

    private void OnDisable()
    {
        inputActions.ResultUI.Space.performed -= Space_performed;
        inputActions.ResultUI.LeftClick.performed -= LeftClick_performed;
        inputActions.ResultUI.Disable();
    }

    private void LeftClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        /*Vector3 mousePos = Input.mousePosition;
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        inputActions.ResultUI.Disable();
        if (hit.collider == null)
        {
            AsyncLoad.OnSceneLoad("Village");
        }*/
        AsyncLoad.OnSceneLoad("Village");
        // else if (!hit.collider.CompareTag("Item")) AsyncLoad.OnSceneLoad("Village");
    }

    private void Space_performed(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        AsyncLoad.OnSceneLoad("Village");
        inputActions.ResultUI.Disable();
    }

    public void Win()
    {
        resultText.text = "Victory!!!";
        resultGold = Random.Range(500, 1000);
        goldAmount.text = $"X{resultGold}";

        gameManager.addGold = resultGold;
        gameManager.resultGold = true;

        int getItem = Random.Range(1, 9);
        switch (getItem)
        {
            case 1:
                resultItem = PlayerWeapon.Armor;
                break;
            case 2:
                resultItem = PlayerWeapon.Pants;
                break;
            case 3:
                resultItem = PlayerWeapon.Archor1;
                break;
            case 4:
                resultItem = PlayerWeapon.Archor_LongBow1;
                break;
            case 5:
                resultItem = PlayerWeapon.Gunner1;
                break;
            case 6:
                resultItem = PlayerWeapon.Soldier_LongSword1;
                break;
            case 7:
                resultItem = PlayerWeapon.Soldier_ShortSword1;
                break;
            case 8:
                resultItem = PlayerWeapon.Warrior_Hammer1;
                break;
        }
        itemData = itemDataManager[resultItem];
        item1Image.sprite = itemData.itemIcon;

        gameManager.addItem = resultItem;
        gameManager.resultItem = true;

        float Itemcount = Random.value;
        if (Itemcount <= 0.2)
        {
            item2Image.color = Color.white;
            getItem = Random.Range(1, 9);
            switch (getItem)
            {
                case 1:
                    resultItem2 = PlayerWeapon.Armor;
                    break;
                case 2:
                    resultItem2 = PlayerWeapon.Pants;
                    break;
                case 3:
                    resultItem2 = PlayerWeapon.Archor1;
                    break;
                case 4:
                    resultItem2 = PlayerWeapon.Archor_LongBow1;
                    break;
                case 5:
                    resultItem2 = PlayerWeapon.Gunner1;
                    break;
                case 6:
                    resultItem2 = PlayerWeapon.Soldier_LongSword1;
                    break;
                case 7:
                    resultItem2 = PlayerWeapon.Soldier_ShortSword1;
                    break;
                case 8:
                    resultItem2 = PlayerWeapon.Warrior_Hammer1;
                    break;
            }
            itemData = itemDataManager[resultItem2];
            item2Image.sprite = itemData.itemIcon;

            gameManager.addItem2 = resultItem2;
            gameManager.resultItem2 = true;
        }
        else
        {
            item2Image.gameObject.SetActive(false);
        }
    }

    public void Lose()
    {
        resultText.text = "Lose...";
        resultGold = Random.Range(0, 100);
        goldAmount.text = $"X{resultGold}";
        gameManager.resultGold = true;
        // 골드 조금 드랍
        item1Image.gameObject.SetActive(false);
        item2Image.gameObject.SetActive(false);
    }
}
