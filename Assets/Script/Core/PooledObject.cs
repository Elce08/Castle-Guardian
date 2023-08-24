using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    /// <summary>
    /// 이 게임 오브젝트가 비활성화 될 때 실행되는 델리게이트
    /// </summary>
    public System.Action onDisable;

    protected virtual void OnEnable()
    {
        transform.SetLocalPositionAndRotation(transform.position, Quaternion.identity);
    }

    protected virtual void OnDisable()
    {
        onDisable?.Invoke();    // 비활성화 되었다고 알림
    }
}
