using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DetailInfoUI : MonoBehaviour
{
    /// <summary>
    /// �������� �������� ǥ���� �̹���
    /// </summary>
    Image itemIcon;

    /// <summary>
    /// �������� �̸��� ����� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI itemName;

    /// <summary>
    /// �������� ������ ����� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI itemPrice;

    /// <summary>
    /// ��ȭ �� ������ ������Ʈ
    /// </summary>
    GameObject before;

    /// <summary>
    /// ��ȭ �� ������ ������Ʈ
    /// </summary>
    GameObject after;

    /// <summary>
    /// ���ġ�� ������ ������Ʈ
    /// </summary>
    GameObject rising;

    /// <summary>
    /// �������� ���� ������ ����� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI[] beforeStat;

    /// <summary>
    /// �������� ������ ������ ����� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI[] afterStat;

    /// <summary>
    /// �������� ���� ���ġ�� ����� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI[] risingStat;

    /// <summary>
    /// �ڽ�Ʈ ��ġ�� ���� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI costText;

    /// <summary>
    /// ������â ��ü�� ���ĸ� ������ ĵ���� �׷�
    /// </summary>
    CanvasGroup canvasGroup;

    /// <summary>
    /// ������â�� �Ͻ� ���� ���θ� ǥ���ϴ� ����
    /// </summary>
    bool isPause = false;

    /// <summary>
    /// ������ â�� �����ִ��� Ȯ���ϴ� ����
    /// </summary>
    bool isOpen = false;

    /// <summary>
    /// �Ͻ����� ���θ� Ȯ�� �� �����ϴ� ������Ƽ
    /// </summary>
    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;
            if (isPause)
            {
                Close();    // �Ͻ� ������ �Ǹ� ���� �ִ� �͵� �ݴ´�.
            }
        }
    }

    /// <summary>
    /// ������â�� ������ ������ �ӵ�
    /// </summary>
    public float alphaChangeSpeed = 10.0f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;
        Transform child = transform.GetChild(0);
        itemIcon = child.GetComponent<Image>();
        child = transform.GetChild(1);
        itemName = child.GetComponent<TextMeshProUGUI>();


        beforeStat = new TextMeshProUGUI[6];
        afterStat = new TextMeshProUGUI[6];
        risingStat = new TextMeshProUGUI[5];


        child = transform.GetChild(3);;
        before = child.gameObject;
        child = transform.GetChild(4);
        after = child.gameObject;
        child = transform.GetChild(5);
        rising = child.gameObject;

        child = transform.GetChild(6);
        costText = child.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(7);
        itemPrice = child.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        for(int i = 0; i < 5; i++)
        {
            beforeStat[i] = before.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            afterStat[i] = after.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            risingStat[i] = rising.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
        beforeStat[5] = before.transform.GetChild(5).GetComponent<TextMeshProUGUI>();

        afterStat[5] = after.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// ������â�� ���� �Լ�
    /// </summary>
    /// <param name="data">������â���� ǥ���� �������� ������</param>
    public void Open(ItemData data)
    {
        if (!IsPause && data != null && isOpen == false)    // �Ͻ����� ���°� �ƴϰ�, ������ �����Ͱ� �ְ�, ������ â�� ������������ ����
        {
            itemIcon.sprite = data.itemIcon;                // ������ ����
            itemName.text = data.itemName;                  // �̸� ����
            itemPrice.text = data.price.ToString("N0");     // ���� ����(3�ڸ����� �޸� �߰�)
            
            beforeStat[0].text = data.beforeStr.ToString();        // ��ȭ �� ����(Str)
            beforeStat[1].text = data.beforeWis.ToString();        // ��ȭ �� ����(Agi)
            beforeStat[2].text = data.beforeDef.ToString();        // ��ȭ �� ����(Int)
            beforeStat[3].text = data.beforeHP.ToString();         // ��ȭ �� ����(HP)
            beforeStat[4].text = data.beforeMP.ToString();         // ��ȭ �� ����(MP)
            beforeStat[5].text = data.beforeValue.ToString();      // ��ȭ �� ��ȭ��ġ

            afterStat[0].text = data.afterStr.ToString();          // ��ȭ �� ����(Str)
            afterStat[1].text = data.afterWis.ToString();          // ��ȭ �� ����(Agi)
            afterStat[2].text = data.afterDef.ToString();          // ��ȭ �� ����(Int)
            afterStat[3].text = data.afterHP.ToString();           // ��ȭ �� ����(HP)
            afterStat[4].text = data.afterMP.ToString();           // ��ȭ �� ����(MP)
            afterStat[5].text = data.afterValue.ToString();        // ��ȭ �� ��ȭ��ġ

            risingStat[0].text = $"+ {data.risingStr.ToString()}";        // ��ȭ �� ��� ����(Str)
            risingStat[1].text = $"+ {data.risingWis.ToString()}";        // ��ȭ �� ��� ����(Agi)
            risingStat[2].text = $"+ {data.risingDef.ToString()}";        // ��ȭ �� ��� ����(Int)
            risingStat[3].text = $"+ {data.risingHP.ToString()}";         // ��ȭ �� ��� ����(HP)
            risingStat[4].text = $"+ {data.risingMP.ToString()}";         // ��ȭ �� ��� ����(MP)

            costText.text = data.cost.ToString();                  // ��ȭ �� �Ҹ���

            StopAllCoroutines();
            StartCoroutine(FadeIn());                       // ���ĸ� ���� 1�� �ǵ��� �����ؼ� ���̰� �����

            MovePosition(Mouse.current.position.ReadValue());   // ���� �� ���콺 Ŀ�� ��ġ�� ������
            isOpen = true;
        }
    }

    /// <summary>
    /// ������â�� �ݴ� �Լ�
    /// </summary>
    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());  // ���ĸ� ���� 0�� �ǵ��� �����ؼ� �Ⱥ��̰� �����
        isOpen = false;
    }

    /// <summary>
    /// ������â�� �����̴� �Լ�
    /// </summary>
    /// <param name="screenPos">��ġ�� ��ũ�� ��ǥ</param>
    public void MovePosition(Vector2 screenPos)
    {
        screenPos = new Vector2(0, -280);
    }

    public void Upgrade()
    {
        Debug.Log("���׷��̵� Ŭ��");
        float up = Random.value;
        if (up <= 0.5f)
        {
            
        }
    }

    public void Sell()
    {
        Debug.Log("�Ǹ� Ŭ��");
    }

    /// <summary>
    /// ������â�� ���� ���̰� ����� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1.0f)    // ���İ� 1�� �� ������ �� �����Ӹ��� ���ݾ� ����
        {
            canvasGroup.alpha += Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
    }

    /// <summary>
    /// ������â�� ���� �Ⱥ��̰� ����� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0.0f)    // ���İ� 0�� �� ������ �������Ӹ��� ���ݾ� ����
        {
            canvasGroup.alpha -= Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
    }
}
