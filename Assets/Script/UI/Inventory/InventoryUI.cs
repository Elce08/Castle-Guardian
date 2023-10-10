using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// �� UI�� ������ �κ��丮 
    /// </summary>
    Inventory inven;

    InvenSlot tempSlot;

    /// <summary>
    /// �� �κ��丮�� ������ �ִ� ��� ������ UI
    /// </summary>
    InvenSlotUI[] slotsUI;

    InvenSlotUI[] invenSlotUI;
    InvenSlotUI[] equipSlotsUI; 
    public InvenSlotUI[] equipSlotsUI1; 
    public InvenSlotUI[] equipSlotsUI2; 
    public InvenSlotUI[] equipSlotsUI3; 

    /// <summary>
    /// ������ �̵��̳� �и��� �� ����� �ӽ� ���� UI
    /// </summary>
    TempSlotUI tempSlotUI;

    /// <summary>
    /// �������� �������� ǥ���ϴ� �г�
    /// </summary>
    DetailInfoUI detail;

    /// <summary>
    /// �����ڰ� ������ �ִ� �ݾ��� �����ִ� �г�
    /// </summary>
    MoneyPanel moneyPanel;

    /// <summary>
    /// �� �κ��丮�� �����ڸ� Ȯ���ϱ� ���� ������Ƽ
    /// </summary>
    public GameManager Owner => inven.Owner;

    /// <summary>
    /// ��ǲ �׼�
    /// </summary>
    PlayerInputActions inputActions;

    CanvasGroup canvasGroup;

    public Action detailOpen;
    public Action detailClose;

    public Action onInventoryOpen;
    public Action onInventoryClose;

    bool mousePoint;

    /// <summary>
    /// ������ â ǥ�ý� index ������ ����
    /// </summary>
    uint old;

    Stat_1 stat1;
    Stat_2 stat2;
    Stat_3 stat3;

    Button closeButton;

    ItemDataManager itemDataManager;

    static public uint indexNum;
    private void Awake()
    {
        itemDataManager = FindObjectOfType<ItemDataManager>();

        Transform child = transform.GetChild(0);
        invenSlotUI = child.GetComponentsInChildren<InvenSlotUI>();

        child = transform.GetChild(3);
        Transform grandChild = child.transform.GetChild(0);
        equipSlotsUI1 = grandChild.GetComponentsInChildren<InvenSlotUI>();
        grandChild = child.transform.GetChild(1);
        equipSlotsUI2 = grandChild.GetComponentsInChildren<InvenSlotUI>();
        grandChild = child.transform.GetChild(2);
        equipSlotsUI3 = grandChild.GetComponentsInChildren<InvenSlotUI>();

        equipSlotsUI1[1].equipType = WeaponType.Armor;     // ��� ���Ժ��� Ÿ�� �ϳ��� ����
        equipSlotsUI1[2].equipType = WeaponType.Pants;
        equipSlotsUI2[1].equipType = WeaponType.Armor;
        equipSlotsUI2[2].equipType = WeaponType.Pants;
        equipSlotsUI3[1].equipType = WeaponType.Armor;
        equipSlotsUI3[2].equipType = WeaponType.Pants;

        equipSlotsUI = equipSlotsUI1.Concat(equipSlotsUI2).Concat(equipSlotsUI3).ToArray();

        slotsUI = invenSlotUI.Concat(equipSlotsUI).ToArray();


        child = transform.GetChild(1);
        closeButton = child.GetComponent<Button>();
        closeButton.onClick.AddListener(Close);

        child = transform.GetChild(2);
        moneyPanel = child.GetComponent<MoneyPanel>();

        child = transform.GetChild(3);
        grandChild = child.transform.GetChild(3);
        detail = grandChild.GetComponent<DetailInfoUI>();

        child = transform.GetChild(4);
        tempSlotUI = child.GetComponent<TempSlotUI>();

        inputActions = new PlayerInputActions();

        canvasGroup = GetComponent<CanvasGroup>();

        child = transform.GetChild(0);
        Transform Gchild = child.transform.GetChild(1);

        stat1 = FindObjectOfType<Stat_1>();
        stat2 = FindObjectOfType<Stat_2>();
        stat3 = FindObjectOfType<Stat_3>();
    }

    /// <summary>
    /// �κ��丮 UI �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="playerInven">�� UI�� ����� �κ��丮</param>
    public void InitializeInventory(Inventory playerInven)
    {
        inven = playerInven;

        // ���� �ʱ�ȭ(�ʱ�ȭ �Լ� ���� �� ��������Ʈ �����ϱ�)
        for (uint i = 0; i < slotsUI.Length; i++)
        {
            slotsUI[i].InitializeSlot(inven[i]);
            slotsUI[i].onDragBegin += OnItemMoveBegin;
            slotsUI[i].onDragEnd += OnItemMoveEnd;
            slotsUI[i].onClick += OnSlotClick;
            slotsUI[i].onItemLeftClick += OnItemDetailClickOn;
            slotsUI[i].onItemRightClick += OnItemDetailClickOff;
            slotsUI[i].onPointerEnter += OnItemDetailOn;
            slotsUI[i].onPointerExit += OnItemDetailOff;
        }

        // �ӽ� ���� �ʱ�ȭ
        tempSlotUI.InitializeSlot(inven.TempSlot);
        tempSlotUI.onTempSlotOpenClose += OnDetailPause;
                      
        // ���ʿ� �Ӵ� �г� �����ϱ�
        Owner.onMoneyChange += moneyPanel.Refresh;
        moneyPanel.Refresh(Owner.Money);        
    }

    /// <summary>
    /// ����UI���� �巡�װ� ���۵Ǹ� ����� �Լ�
    /// </summary>
    /// <param name="index">�巡�װ� ���۵� ������ �ε���</param>
    private void OnItemMoveBegin(uint index)
    {
        inven.MoveItem(index, tempSlotUI.Index);    // ���� ���Կ��� �ӽ� �������� ������ �ű��
        InvenSlotUI.dragStartSlotIndex = index;
        tempSlotUI.Open();                          // �ӽ� ���� ����
    }

    /// <summary>
    /// ����UI���� �巡�װ� ������ �� ����� �Լ�
    /// </summary>
    /// <param name="index">�巡�װ� ���� ������ �ε���</param>
    /// <param name="isSuccess">�巡�װ� ���������� ����</param>
    private void OnItemMoveEnd(uint index, bool isSuccess)
    {
        uint finalIndex = index;                        // ���� ����� �ε���(�⺻�����δ� �Ķ���ͷ� ���� �ε���)
        if (64 <= finalIndex && finalIndex <= 72)
        {
            if (Inventory.invenSlot.ItemData.equipPart == slotsUI[index].equipType)
            {
                inven.MoveItem(tempSlotUI.Index, finalIndex);   // �ӽ� ���Կ��� ��� �������� ������ �ű��
                stat1.Status();     // �̵� �������� 1�� ĳ������ ���� ����
                stat2.Status();     // �̵� �������� 2�� ĳ������ ���� ����
                stat3.Status();     // �̵� �������� 3�� ĳ������ ���� ����
            }
            else
            {
                // �ӽý��Կ� �ִ� ������ Ÿ�� =! ���콺 ��ġ ������ ����Ÿ���̶��(���� ����)
                finalIndex = InvenSlotUI.dragStartSlotIndex;    // �巡�׸� ������ ��ġ��
                inven.MoveItem(tempSlotUI.Index, finalIndex);   // �ӽ� ���Կ��� �κ� �������� ������ �ű��(���� �ڸ��� ���ư�)
            }
        }
        else
        {
            inven.MoveItem(tempSlotUI.Index, finalIndex);   // �ӽ� ���Կ��� ���� �������� ������ �ű��
            stat1.Status();
            stat2.Status();
            stat3.Status();
        }

        if (tempSlotUI.InvenSlot.IsEmpty)               // ����ٸ�(���� ������ �������� �� �Ϻθ� ���� ��찡 ���� �� �����Ƿ�)
        {
            tempSlotUI.Close();                         // �ӽ� ���� �ݱ�
        }
    }

    /// <summary>
    /// ���콺 �����Ͱ� ���������� Ŭ���Ǿ����� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index">�ö� ������ �ε���</param>
    private void OnItemDetailClickOn(uint index)
    {
        indexNum = index;
        if (mousePoint == true)
        {
            detail.Open(slotsUI[index].InvenSlot.ItemData); // ������â ����
        }
        else if (mousePoint == false)
        {
            detail.Close(); // ������â �ݱ�
        }
    }

    private void OnItemDetailClickOff(uint index)
    {
        detail.Close();
    }

    /// <summary>
    /// ���콺 �����Ͱ� �������� �ö���� �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index"></param>
    private void OnItemDetailOn(uint index)
    {
        mousePoint = true;
    }

    /// <summary>
    /// ���콺 �����Ͱ� ���������� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="index">���� ������ �ε���</param>
    private void OnItemDetailOff(uint index)
    {
        mousePoint = false;
    }


    /// <summary>
    /// �ӽ� ������ ������ ������â�� �Ͻ� �����ϰ�, ������ �Ͻ� ������ Ǫ�� �Լ�
    /// </summary>
    /// <param name="isPause">true�� �Ͻ� ����, false ����</param>
    private void OnDetailPause(bool isPause)
    {
        detail.IsPause = isPause;
    }

    /// <summary>
    /// ����UI�� ���콺�� Ŭ���� �Ǿ��� �� ����� �Լ�
    /// </summary>
    /// <param name="index">Ŭ���� ������ �ε���</param>
    private void OnSlotClick(uint index)
    {
        if (!tempSlotUI.InvenSlot.IsEmpty)
        {
            // �ӽ� ���Կ� �������� ���� �� Ŭ���� �Ǿ�����
            OnItemMoveEnd(index, true); // Ŭ���� �������� ������ �̵�
        }
    }

    /// <summary>
    /// �κ��丮�� ���� �Լ�
    /// </summary>
    public void Open()
    {
        this.gameObject.SetActive(true);
        stat1.Status();
        stat2.Status();
        stat3.Status();
        tempSlotUI.Close();
    }

    /// <summary>
    /// �κ��丮�� ���� �Լ�
    /// </summary>
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
