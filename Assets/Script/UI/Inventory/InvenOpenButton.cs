using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenOpenButton : MonoBehaviour
{
    Button openButton;

    public GameObject inven;

    private void Awake()
    {
        inven.gameObject.SetActive(false);
        openButton = GetComponent<Button>();
        openButton.onClick.AddListener(open);
    }

    private void open()
    {
        inven.gameObject.SetActive(true);
    }
}
