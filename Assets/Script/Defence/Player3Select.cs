using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player3Select : MonoBehaviour
{
    Button button;

    GameManager gameManager;

    Image playerImage;

    PlayerInputActions inputActions;

    public Action <bool> player3Selected;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        inputActions = new PlayerInputActions();
        Transform child = transform.GetChild(0);
        playerImage = child.GetComponent<Image>();
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Selected);
        playerImage.sprite = gameManager.player3Sprite;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Selected()
    {
        player3Selected?.Invoke(true);
        inputActions.Player.Mouse.performed += Mouse;
    }

    private void Mouse(InputAction.CallbackContext _)
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                Factory.Inst.GetObject(PoolObjectType.DefencePlayer3, hit.collider.gameObject.transform.position - new Vector3());
                player3Selected?.Invoke(false);
                Tile tile = hit.collider.gameObject.GetComponent<Tile>();
                tile.State = Tile.TIleState.PlayerOn;
                inputActions.Player.Mouse.performed -= Mouse;
            }
            else
            {
                player3Selected?.Invoke(false);
                inputActions.Player.Mouse.performed -= Mouse;
            }
        }
        else
        {
            player3Selected?.Invoke(false);
            inputActions.Player.Mouse.performed -= Mouse;
        }
    }
}
