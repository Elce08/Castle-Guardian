using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Item : TestBase
{
    public uint size = 30;
    public PlayerWeapon code = PlayerWeapon.Gunner1;
    public uint index = 0;

    DetailInfoUI detail;
    ItemData item;
    GameManager gameManager;
    Inventory inven;
    

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        gameManager.AddItem();
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        gameManager.Money += 3000;
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
    }

    protected override void Test4(InputAction.CallbackContext context)
    {
        inven.ClearSlot(index);
    }

    protected override void Test5(InputAction.CallbackContext context)
    {
        inven.ClearInventory();
    }
}
