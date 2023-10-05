using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenOpenButton : MonoBehaviour
{
    InventoryUI invenLink;
    Button openButton;

    private void Awake()
    {
        invenLink = FindObjectOfType<InventoryUI>();
        openButton = GetComponent<Button>();
        openButton.onClick.AddListener(invenLink.Open);
    }

    private void Start()
    {
        invenLink.Close();
    }
} 
