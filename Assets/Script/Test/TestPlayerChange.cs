using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerChange : TestBase
{
    PlayerBase player1;

    protected override void Awake()
    {
        base.Awake();
        player1 = GameObject.FindWithTag("Player1").GetComponent<PlayerBase>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        player1.PlayerType = PlayerType.None;
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        player1.PlayerType = PlayerType.Archor_LongBow;
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        player1.PlayerType = PlayerType.Gunner;
    }

    protected override void Test4(InputAction.CallbackContext _)
    {
        player1.PlayerType = PlayerType.Soldier_LongSword;
    }

    protected override void Test5(InputAction.CallbackContext _)
    {
        player1.PlayerType = PlayerType.Warrior_Hammer;
    }
}
