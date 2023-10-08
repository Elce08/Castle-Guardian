using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Stat_3 : MonoBehaviour
{
    TextMeshProUGUI[] status;
    InvenSlotUI[] slots;
    GameManager gameManager;

    float[] Str;
    float[] Def;
    float[] HP;
    float[] MP;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        status = new TextMeshProUGUI[6];
        Str = new float[3];
        Def = new float[3];
        HP = new float[3];
        MP = new float[3];
        Transform child = transform.GetChild(0);
        Transform grandChild = child.transform.GetChild(0);
        status[0] = grandChild.GetComponent<TextMeshProUGUI>();    // Str Text

        child = transform.GetChild(1);
        grandChild = child.transform.GetChild(0);
        status[1] = grandChild.GetComponent<TextMeshProUGUI>();    // Def Text

        child = transform.GetChild(2);
        grandChild = child.transform.GetChild(0);
        status[2] = grandChild.GetComponent<TextMeshProUGUI>();    // HP Text

        child = transform.GetChild(3);
        grandChild = child.transform.GetChild(0);
        status[3] = grandChild.GetComponent<TextMeshProUGUI>();    // MP Text

        child = transform.GetChild(4);
        grandChild = child.transform.GetChild(0);
        status[4] = grandChild.GetComponent<TextMeshProUGUI>();    // Speed Text

        slots = new InvenSlotUI[3];  // 장비 슬롯 찾기
        Transform parent = transform.parent;
        child = parent.transform.GetChild(2);
        slots[0] = child.GetComponent<InvenSlotUI>();  // 1번 캐릭터 무기

        child = parent.transform.GetChild(3);
        slots[1] = child.GetComponent<InvenSlotUI>();  // 1번 캐릭터 상의

        child = parent.transform.GetChild(4);
        slots[2] = child.GetComponent<InvenSlotUI>();  // 1번 캐릭터 하의
    }

    private void Update()
    {
        status[0].text = $"Str {(gameManager.selectType3Str + Str[0] + Str[1] + Str[2]).ToString()}";
        status[1].text = $"Def {(gameManager.selectType3Def + Def[0] + Def[1] + Def[2]).ToString()}";
        status[2].text = $"HP {(gameManager.selectType3HP + HP[0] + HP[1] + HP[2]).ToString()}";
        status[3].text = $"MP {(gameManager.selectType3MP + MP[0] + MP[1] + MP[2]).ToString()}";
        status[4].text = $"Speed {(gameManager.selectType3Speed).ToString()}";
    }

    public void Status()
    {
        for (int i = 0; i < 3; i++)
        {
            if (slots[i].InvenSlot.ItemData != null)
            {
                Str[i] = slots[i].InvenSlot.ItemData.beforeStr;
                Def[i] = slots[i].InvenSlot.ItemData.beforeDef;
                HP[i] = slots[i].InvenSlot.ItemData.beforeHP;
                MP[i] = slots[i].InvenSlot.ItemData.beforeMP;
            }
            else if (slots[i].InvenSlot.ItemData == null)
            {
                Str[i] = 0;
                Def[i] = 0;
                HP[i] = 0;
                MP[i] = 0;
            }
        }
    }
}
