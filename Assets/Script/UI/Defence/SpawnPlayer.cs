using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    Button[] buttons;

    /// <summary>
    /// ��ư ����
    /// </summary>
    readonly int button = 3;

    GameManager gameManager;
    DefenceManager defenceManager;

    Image[] playerImages;

    PlayerInputActions inputActions;

    /// <summary>
    /// ���õ� �÷��̾�
    /// </summary>
    static PoolObjectType? player = null;

    private void Awake()
    {
        inputActions = new();
        defenceManager = FindObjectOfType<DefenceManager>();
    }

    private void Start()
    {
        buttons = new Button[button];
        playerImages = new Image[button];
        for(int i = 0; i < button;  i++)
        {
            Transform child = transform.GetChild(i);
            buttons[i] = child.GetComponent<Button>();
            Transform grandChild = child.GetChild(0);
            playerImages[i] = grandChild.GetComponent<Image>();
        }
        gameManager = FindObjectOfType<GameManager>();
        playerImages[0].sprite = gameManager.player1Sprite;
        playerImages[1].sprite = gameManager.player2Sprite;
        playerImages[2].sprite = gameManager.player3Sprite;
        buttons[0].onClick.AddListener(Player1Selected);
        buttons[1].onClick.AddListener(Player2Selected);
        buttons[2].onClick.AddListener(Player3Selected);
        defenceManager.gameEnd += GameEnd;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    public void Stop(bool stop)
    {
        if (stop)
        {
            inputActions.Player.Disable();
            foreach(Button button in buttons)
            {
                button.onClick.RemoveAllListeners();
            }

        }
        else if (!stop)
        {
            inputActions.Player.Enable();
            buttons[0].onClick.AddListener(Player1Selected);
            buttons[1].onClick.AddListener(Player2Selected);
            buttons[2].onClick.AddListener(Player3Selected);
        }
    }

    private void Player1Selected()
    {
        player = PoolObjectType.DefencePlayer1;
        inputActions.Player.Mouse.performed += Mouse;
    }

    private void Player2Selected()
    {
        player = PoolObjectType.DefencePlayer2;
        inputActions.Player.Mouse.performed += Mouse;
    }

    private void Player3Selected()
    {
        player = PoolObjectType.DefencePlayer3;
        inputActions.Player.Mouse.performed += Mouse;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
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
                Factory.Inst.GetObject(player, hit.collider.gameObject.transform.position - new Vector3());
                Tile tile = hit.collider.gameObject.GetComponent<Tile>();
                tile.State = Tile.TIleState.PlayerOn;
            }
        }
        inputActions.Player.Mouse.performed -= Mouse;
        player = null;
    }

    private void GameEnd()
    {
        inputActions.Player.Disable();
        foreach(Button button in buttons) button.onClick.RemoveAllListeners();
    }
}