using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckHome : MonoBehaviour
{
    CheckHome check;
    Button yes;
    Button no;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        check = GetComponent<CheckHome>();

        Transform child = transform.GetChild(0);
        yes = child.GetComponent<Button>();
        yes.onClick.AddListener(gameManager.HomeButton);

        child = transform.GetChild(1);
        no = child.GetComponent<Button>();
        no.onClick.AddListener(Close);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Close()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
