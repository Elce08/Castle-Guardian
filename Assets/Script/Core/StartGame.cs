using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.PressAnything.Enable();
        inputActions.PressAnything.AnyThing.performed += LoadPlayerSelect;
    }

    private void OnDisable()
    {
        inputActions.PressAnything.AnyThing.performed -= LoadPlayerSelect;
        inputActions.PressAnything.Disable();
    }

    private void LoadPlayerSelect(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        if (DataManager.Instance.data.firstGame) AsyncLoad.OnSceneLoad("PlayerSelect");
        else AsyncLoad.OnSceneLoad("Village");
    }
}
