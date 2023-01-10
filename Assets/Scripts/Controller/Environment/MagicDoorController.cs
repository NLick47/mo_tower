using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDoorController : MonoBehaviour
{
    private EnemyController _guard1;
    private EnemyController _guard2;

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetGuardEvent;
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= GetGuardEvent;
    }

    public void TriggerClose()
    {
        GetComponent<Animator>().SetTrigger("close");
    }

    /// <summary>
    /// ��ȡ�����¼�
    /// </summary>
    private void GetGuardEvent()
    {
        _guard1 = null;
        _guard2 = null;
        // ����ʹ�������б��а�λ�û�ȡ����
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(transform.position.x - 1, transform.position.y - 1))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard1 = obj.GetComponent<EnemyController>();
                    _guard1.OnDeath += () => { _guard1 = null; DetectionOpen(); };
                }
            }
            else if ((Vector2)obj.transform.position == new Vector2(transform.position.x + 1, transform.position.y - 1))
            {
                if (obj.GetComponent<EnemyController>() != null)
                {
                    _guard2 = obj.GetComponent<EnemyController>();
                    _guard2.OnDeath += () => { _guard2 = null; DetectionOpen(); };
                }
            }
        });
    }

    /// <summary>
    /// ������Ƿ��ܴ�
    /// </summary>
    private void DetectionOpen()
    {
        if (_guard1 == null && _guard2 == null) GetComponent<EnvironmentController>().Open(null);
    }
}