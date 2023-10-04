using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIBase : MonoBehaviour
{
    public TurnManager turnManager;
    public Image portrait;
    public Slider hpSlider;
    public Slider mpSlider;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TurnEnemyBase enemy;

    private void Awake()
    {
        portrait = transform.GetChild(1).GetComponent<Image>();
        hpSlider = transform.GetChild(2).GetComponent<Slider>();
        hpText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    protected virtual void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }
}
