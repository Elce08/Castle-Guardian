using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class DetailInfoUI : MonoBehaviour
{
    /// <summary>
    /// 아이템의 아이콘을 표시할 이미지
    /// </summary>
    Image itemIcon;

    /// <summary>
    /// 아이템의 이름을 출력할 텍스트
    /// </summary>
    TextMeshProUGUI itemName;

    /// <summary>
    /// 아이템의 가격을 출력할 텍스트
    /// </summary>
    TextMeshProUGUI itemPrice;

    /// <summary>
    /// 강화 전 아이템 오브젝트
    /// </summary>
    GameObject before;

    /// <summary>
    /// 강화 후 아이템 오브젝트
    /// </summary>
    GameObject after;

    /// <summary>
    /// 상승치의 아이템 오브젝트
    /// </summary>
    GameObject rising;

    /// <summary>
    /// 아이템의 기존 스텟을 출력할 텍스트
    /// </summary>
    TextMeshProUGUI[] beforeStat;

    /// <summary>
    /// 아이템의 애프터 스텟을 출력할 텍스트
    /// </summary>
    TextMeshProUGUI[] afterStat;

    /// <summary>
    /// 아이템의 스텟 상승치를 출력할 텍스트
    /// </summary>
    TextMeshProUGUI[] risingStat;

    /// <summary>
    /// 코스트 수치를 적을 텍스트
    /// </summary>
    TextMeshProUGUI costText;

    /// <summary>
    /// 디테일창 전체의 알파를 조절할 캔버스 그룹
    /// </summary>
    CanvasGroup canvasGroup;

    /// <summary>
    /// 디테일창의 일시 정지 여부를 표시하는 변수
    /// </summary>
    bool isPause = false;

    ItemData nowItem;

    Stat_1 stat1;
    Stat_2 stat2;
    Stat_3 stat3;

    GameManager owner;
    /// <summary>
    /// 일시정지 여부를 확인 및 설정하는 프로퍼티
    /// </summary>
    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;
            if (isPause)
            {
                Close();    // 일시 정지가 되면 열려 있던 것도 닫는다.
            }
        }
    }

    /// <summary>
    /// 디테일창이 열리고 닫히는 속도
    /// </summary>
    public float alphaChangeSpeed = 10.0f;

    private void Awake()
    {
        owner = FindObjectOfType<GameManager>();
        nowItem = new();
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

        for(int i = 0; i < 4; i++)
        {
            beforeStat[i] = before.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            afterStat[i] = after.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            risingStat[i] = rising.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
        beforeStat[4] = before.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        afterStat[4] = after.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        stat1 = FindObjectOfType<Stat_1>();
        stat2 = FindObjectOfType<Stat_2>();
        stat3 = FindObjectOfType<Stat_3>();
    }

    /// <summary>
    /// 상세정보창을 여는 함수
    /// </summary>
    /// <param name="data">상세정보창에서 표시할 아이템의 데이터</param>
    public void Open(ItemData data)
    {
        if (!IsPause && data != null)    // 일시정지 상태가 아니고, 아이템 데이터가 있을때만 열기
        {
            data.ItemStatus();
            if (data.upgrade != 5)
            {
                NoMax(data);    // 업그레이드 횟수가 Max가 아닐때 텍스트
                StopAllCoroutines();
                StartCoroutine(FadeIn());   // 알파를 점점 1이 되도록 설정해서 보이게 만들기
                nowItem = data;
            }
            else
            {
                YesMax(data);   // 업그레이드 횟수가 Max일때 텍스트
                StopAllCoroutines();
                StartCoroutine(FadeIn());   // 알파를 점점 1이 되도록 설정해서 보이게 만들기
                nowItem = data;
            }
        }
    }

    /// <summary>
    /// 상세정보창을 닫는 함수
    /// </summary>
    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());  // 알파를 점점 0이 되도록 설정해서 안보이게 만들기
    }

    /// <summary>
    /// 상세정보창을 움직이는 함수
    /// </summary>
    /// <param name="screenPos">위치할 스크린 좌표</param>
    public void MovePosition(Vector2 screenPos)
    {
        screenPos = new Vector2(0, -280);
    }

    /// <summary>
    /// 강화 버튼을 눌렀을때
    /// </summary>
    public void Upgrade()
    {
        if (nowItem.upgrade <= 4)
        {
            if (nowItem.cost < owner.Money)
            {
                owner.Money -= nowItem.cost;    // 비용 지불
                float Success = 0.0f;   // 성공확률
                switch (nowItem.upgrade)
                {
                    case 0: // 0강일때
                        Success = 7.0f;
                        break;
                    case 1: // 1강일때
                        Success = 6.0f;
                        break;
                    case 2: // 2강일때
                        Success = 5.0f;
                        break;
                    case 3: // 3강일때
                        Success = 4.0f;
                        break;
                    case 4: // 4강일때
                        Success = 3.0f;
                        break;
                    case 5: // 5강일때
                        Success = 0.0f;
                        break;
                }
                float upgrade = Random.Range(0, 10);
                if (upgrade <= Success) // 성공확률에 따라
                {
                    nowItem.upgrade++;
                    Close();
                    Open(nowItem);
                    Debug.Log("강화에 성공했습니다.");
                }
                else
                {
                    Debug.Log("강화에 실패했습니다.");
                    Open(nowItem);
                }
            }
            else
            {
                Debug.Log("골드가 부족합니다");
            }
        }
        else
        {
            Debug.Log("강화가 최대치 입니다.");
        }
        stat1.Status();
        stat2.Status();
        stat3.Status();
    }

    /// <summary>
    /// 판매 버튼을 눌렀을때
    /// </summary>
    public void Sell()
    {
        InvenSlot invenSlot = Inventory.slots[InventoryUI.indexNum];
        invenSlot.ClearSlotItem();
        owner.Money += nowItem.price;
        stat1.Status();
        stat2.Status();
        stat3.Status();
        Close();
    }

    /// <summary>
    /// 강화 수치가 최대치가 아닐때 출력되는 텍스트
    /// </summary>
    /// <param name="data"></param>
    void NoMax(ItemData data)
    {
        itemIcon.sprite = data.itemIcon;                // 아이콘 설정
        itemName.text = data.itemName;                  // 이름 설정
        itemPrice.text = $"{data.price.ToString("N0")} Gold";     // 가격 설정(3자리마다 콤마 추가)

        beforeStat[0].text = data.beforeStr.ToString();        // 강화 전 스텟(Str)
        beforeStat[1].text = data.beforeDef.ToString();        // 강화 전 스텟(Int)
        beforeStat[2].text = data.beforeHP.ToString();         // 강화 전 스텟(HP)
        beforeStat[3].text = data.beforeMP.ToString();         // 강화 전 스텟(MP)
        beforeStat[4].text = data.beforeValue.ToString();      // 강화 전 강화수치

        afterStat[0].text = data.afterStr.ToString();          // 강화 후 스텟(Str)
        afterStat[1].text = data.afterDef.ToString();          // 강화 후 스텟(Int)
        afterStat[2].text = data.afterHP.ToString();           // 강화 후 스텟(HP)
        afterStat[3].text = data.afterMP.ToString();           // 강화 후 스텟(MP)
        afterStat[4].text = data.afterValue.ToString();        // 강화 후 강화수치

        risingStat[0].text = $"+ {data.risingStr.ToString()}";        // 강화 시 상승 스텟(Str)
        risingStat[1].text = $"+ {data.risingDef.ToString()}";        // 강화 시 상승 스텟(Int)
        risingStat[2].text = $"+ {data.risingHP.ToString()}";         // 강화 시 상승 스텟(HP)
        risingStat[3].text = $"+ {data.risingMP.ToString()}";         // 강화 시 상승 스텟(MP)

        costText.text = $"{data.cost.ToString("N0")} Gold";                  // 강화 시 소모비용
    }

    /// <summary>
    /// 강화 수치가 최대치일때 출력되는 텍스트
    /// </summary>
    /// <param name="data"></param>
    void YesMax(ItemData data)
    {
        itemIcon.sprite = data.itemIcon;                // 아이콘 설정
        itemName.text = data.itemName;                  // 이름 설정
        itemPrice.text = $"{data.price.ToString("N0")} Gold";     // 가격 설정(3자리마다 콤마 추가)

        beforeStat[0].text = data.beforeStr.ToString();        // 강화 전 스텟(Str)
        beforeStat[1].text = data.beforeDef.ToString();        // 강화 전 스텟(Def)
        beforeStat[2].text = data.beforeHP.ToString();         // 강화 전 스텟(HP)
        beforeStat[3].text = data.beforeMP.ToString();         // 강화 전 스텟(MP)
        beforeStat[4].text = data.beforeValue.ToString();      // 강화 전 강화수치

        afterStat[0].text = "Max";          // 강화 후 스텟(Str)
        afterStat[1].text = "Max";          // 강화 후 스텟(Def)
        afterStat[2].text = "Max";           // 강화 후 스텟(HP)
        afterStat[3].text = "Max";           // 강화 후 스텟(MP)
        afterStat[4].text = "Max";        // 강화 후 강화수치

        risingStat[0].text = $"+ 0";        // 강화 시 상승 스텟(Str)
        risingStat[1].text = $"+ 0";        // 강화 시 상승 스텟(Def)
        risingStat[2].text = $"+ 0";         // 강화 시 상승 스텟(HP)
        risingStat[3].text = $"+ 0";         // 강화 시 상승 스텟(MP)

        costText.text = "강화 불가";                  // 강화 시 소모비용
    }

    /// <summary>
    /// 디테일창을 점점 보이게 만드는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1.0f)    // 알파가 1이 될 때까지 매 프레임마다 조금씩 증가
        {
            canvasGroup.alpha += Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
    }

    /// <summary>
    /// 디테일창이 점점 안보이게 만드는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0.0f)    // 알파가 0이 될 때까지 매프레임마다 조금씩 감소
        {
            canvasGroup.alpha -= Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
    }
}
