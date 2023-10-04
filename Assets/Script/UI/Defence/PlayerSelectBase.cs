using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSelectBase : MonoBehaviour
{
    static Button button;

    public GameManager gameManager;

    protected Image playerImage;

    protected static PlayerInputActions inputActions;

    protected virtual void Awake()
    {
        inputActions = new();
        Transform child = transform.GetChild(0);
        playerImage = child.GetComponent<Image>();
    }
    protected virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Selected);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    protected virtual void Selected()
    {
        inputActions.Player.Mouse.performed += Mouse;
    }

    protected virtual void Mouse(InputAction.CallbackContext _)
    {
    }
}
