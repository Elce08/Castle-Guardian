using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    DetailInfoUI detail;
    Button upgradeButton;
    private void Awake()
    {
        detail = FindObjectOfType<DetailInfoUI>();
        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(detail.Upgrade);
    }
    // ��ȭ �����Ŀ��� DetailInfoUI.Open(itemData);
}
