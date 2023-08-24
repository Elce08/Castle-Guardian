using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    /// <summary>
    /// �� ���� ������Ʈ�� ��Ȱ��ȭ �� �� ����Ǵ� ��������Ʈ
    /// </summary>
    public System.Action onDisable;

    protected virtual void OnEnable()
    {
        transform.SetLocalPositionAndRotation(transform.position, Quaternion.identity);
    }

    protected virtual void OnDisable()
    {
        onDisable?.Invoke();    // ��Ȱ��ȭ �Ǿ��ٰ� �˸�
    }
}
