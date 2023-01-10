using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel50Actor1 : ActorController
{
    public override bool Interaction()
    {
        // ��������
        GameManager.Instance.SoundManager.LockEnable = true;
        // ��ʿ˵��
        GameManager.Instance.UIManager.ShowDialog(GameManager.Instance.PlayerManager.PlayerController.Name, new List<string> { "����ô��������㵽����˭��" }, () =>
        {
            // С͵˵��
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "���ɾ��ǡ���" }, () =>
            {
                StartCoroutine(Hide());
            });
        });
        return false;
    }


    /// <summary>
    /// �ƶ� 2
    /// </summary>
    IEnumerator Hide()
    {
        // С͵��ʧ
        _animator.SetTrigger("hide");
        // ����ħ��
        GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 34).transform.position = transform.position;
        yield break;
    }

}
