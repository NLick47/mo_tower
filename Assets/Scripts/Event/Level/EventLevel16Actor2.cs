using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel16Actor2 : ActorController
{
    public override bool Interaction()
    {
        // С͵˵��
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "�ܺã����Ȼ�ҵ����ҡ�", "������ƿʥˮ��Ϊ������", "����������Ը�����Ĺ������ͷ���������������������Ҫ������õ��ˡ�" }, () =>
        {
            // ��������ʥˮ
            GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 17).GetComponent<ItemController>());
            // �����������
            GameManager.Instance.PlayerManager.Enable = true;
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "11-19");
            // NPC ����
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
