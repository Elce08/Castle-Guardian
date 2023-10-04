using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIBase : MonoBehaviour
{
    public GameManager gameManager;
    public Image portrait;
    public Slider hpSlider;
    public Slider mpSlider;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;

    private void Awake()
    {
        portrait = transform.GetChild(1).GetComponent<Image>();
        hpSlider = transform.GetChild(2).GetComponent<Slider>();
        mpSlider = transform.GetChild(3).GetComponent<Slider>();
        hpText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        mpText = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
    }

    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
}
