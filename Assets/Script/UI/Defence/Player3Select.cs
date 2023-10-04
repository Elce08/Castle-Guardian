using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player3Select : PlayerSelectBase
{
    public Action<bool> player3Selected;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        playerImage.sprite = gameManager.player3Sprite;
    }

    protected override void Selected()
    {
        base.Selected();
        player3Selected?.Invoke(true);
    }

    protected override void Mouse(InputAction.CallbackContext _)
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
