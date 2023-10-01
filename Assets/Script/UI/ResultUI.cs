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

    private void Awake()
    {
        goldAmount = transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
        item1Image = transform.GetChild(3).GetComponent<Image>();
        item2Image = transform.GetChild(4).GetComponent<Image>();
        resultText = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        inputActions = new();
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
        Vector3 mousePos = Input.mousePosition;
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        inputActions.ResultUI.Disable();
        if (hit.collider == null)
        {
            AsyncLoad.OnSceneLoad("Village");
        }
        else if (!hit.collider.CompareTag("Item")) AsyncLoad.OnSceneLoad("Village");
    }

    private void Space_performed(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        AsyncLoad.OnSceneLoad("Village");
        inputActions.ResultUI.Disable();
    }

    public void Win()
    {
        resultText.text = "Victory!!!";
        // 골드 드랍
        // 템 드랍
    }

    public void Lose()
    {
        resultText.text = "Lose...";
        // 골드 조금 드랍
        item1Image.gameObject.SetActive(false);
        item2Image.gameObject.SetActive(false);
    }
}
