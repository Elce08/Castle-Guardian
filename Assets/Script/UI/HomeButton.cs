using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    Button button;
    CheckHome check;

    private void Awake()
    {
        button = GetComponent<Button>();
        Transform child = transform.GetChild(0);
        check = child.GetComponent<CheckHome>();
        button.onClick.AddListener(check.Open);
    }
    private void Start()
    {
        check.Close();
    }
}
