using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountUI : MonoBehaviour
{
    TextMeshProUGUI LifeCount;
    TextMeshProUGUI KillCount;

    private void Awake()
    {
        LifeCount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        KillCount = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void LifeCountChange(int life)
    {
        LifeCount.text = $"Life : {life}";
    }

    public void KillCountChange(int kill)
    {
        KillCount.text = $"KillCount : {kill}";
    }
}
