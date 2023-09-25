using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempSlotUI : SlotUIBase
{
    /// <summary>
    /// �� �κ��丮�� ���� �÷��̾�(������ ��� ������ �ʿ�)
    /// </summary>
    public GameManager gameManager;

    /// <summary>
    /// �ӽ� ������ ������ ���� �� ����Ǵ� �Լ�
    /// </summary>
    public Action<bool> onTempSlotOpenClose;

    public bool tempSlot = false;

    private void Update()
    {
        // �ӽ� ������ ��κ� ���� ���� �Ŷ� �δ��� ����
        transform.position = Mouse.current.position.ReadValue();    // �ӽ� ������ ���콺 ��ġ�� ���� ������
    }

    /// <summary>
    /// �ӽ� ���� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    /// <param name="slot">�� �ӽ� ���԰� ����� �κ� ����</param>
    public override void InitializeSlot(InvenSlot slot)
    {
        onTempSlotOpenClose = null;                 // ��������Ʈ �ʱ�ȭ

        base.InitializeSlot(slot);

        Close();                                    // ������ �� �ڵ����� ������
    }

    /// <summary>
    /// �ӽ� ������ ���� �Լ�
    /// </summary>
    public void Open()
    {
        transform.position = Mouse.current.position.ReadValue();    // ��ġ�� ���콺 ��ġ�� ����
        onTempSlotOpenClose?.Invoke(true);                          // ���ȴٰ� ��ȣ ������
        gameObject.SetActive(true);                                 // Ȱ��ȭ ��Ű��(���̰� �����)
        tempSlot = true;
    }

    /// <summary>
    /// �ӽ� ������ �ݴ� �Լ�
    /// </summary>
    public void Close()
    {
        onTempSlotOpenClose?.Invoke(false);     // �����ٰ� ��ȣ ������
        gameObject.SetActive(false);            // ��Ȱ��ȭ ��Ű��(�Ⱥ��̰� �����)
        tempSlot = false;
    }
}
