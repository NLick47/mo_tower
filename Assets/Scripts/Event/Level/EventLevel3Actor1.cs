using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel3Actor1 : ActorController
{
    public override bool Interaction()
    {
        // С͵˵��
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "�Ȿ�����ֲύ���㡣", "���ܲ鿴��������������", "ף����ˡ�" }, () =>
        {
            // ������������ֲ�
            GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 10).GetComponent<ItemController>());
            // �����������
            GameManager.Instance.PlayerManager.Enable = true;
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
            // NPC ����
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
