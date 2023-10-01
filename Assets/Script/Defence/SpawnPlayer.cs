using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    static Button[] buttons;

    /// <summary>
    /// 버튼 갯수
    /// </summary>
    readonly int button = 3;

    public GameManager gameManager;

    static Image[] playerImages;

    static PlayerInputActions inputActions;

    /// <summary>
    /// 선택된 플레이어
    /// </summary>
    static PoolObjectType? player = null;

    private void Awake()
    {
        inputActions = new();
        buttonImages = new Image[button];
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
            buttonImages[i] = buttons[i].gameObject.GetComponent<Image>();
        }
        gameManager = FindObjectOfType<GameManager>();
        playerImages[0].sprite = gameManager.player1Sprite;
        playerImages[1].sprite = gameManager.player2Sprite;
        playerImages[2].sprite = gameManager.player3Sprite;
        buttons[0].onClick.AddListener(Player1Selected);
        buttons[1].onClick.AddListener(Player2Selected);
        buttons[2].onClick.AddListener(Player3Selected);
        blink += Blink;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    public static void Stop(bool stop)
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

    private static void Player1Selected()
    {
        player = PoolObjectType.DefencePlayer1;
        inputActions.Player.Mouse.performed += Mouse;
    }

    private static void Player2Selected()
    {
        player = PoolObjectType.DefencePlayer2;
        inputActions.Player.Mouse.performed += Mouse;
    }

    private static void Player3Selected()
    {
        player = PoolObjectType.DefencePlayer3;
        inputActions.Player.Mouse.performed += Mouse;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private static void Mouse(InputAction.CallbackContext _)
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

    // 깜빡이-------------------------------------------------------------------------------------------
    static Image[] buttonImages;

    /// <summary>
    /// 켜진 버튼 확인용
    /// </summary>
    private enum OnButton
    {
        off,
        player1on,
        player2on,
        player3on,
    }

    static OnButton buttonState = OnButton.off;

    static OnButton ButtonState
    {
        get => buttonState;
        set
        {
            if(buttonState != value)
            {
                buttonState = value;
                blink(ButtonState);
            }
        }
    }

    static Action<OnButton> blink;

    private void Update()
    {
        blink(ButtonState);
    }

    /// <summary>
    /// 깜빡이게 할 함수
    /// </summary>
    /// <param name="buttonImage">대상</param>
    static void Blink(OnButton buttonImage)
    {

    }
}
