using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel39Actor2 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[11])
        {
            // ״̬ 1 ����
            case 1:
                GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "����������Կ�ף�ֻ�� 2000 �������ܵõ�����", "������", "����Ҫ", () =>
                {
                    // ���� 2000 ���
                    if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 2000)
                    {
                        GameManager.Instance.UIManager.ShowInfo("��Ҳ���Ŷ~��ȥ����һ�°�~");
                        // ��Ƶ����
                        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                        return;
                    }
                    GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 2000;
                    // ��� ��Կ�� 3 ��
                    for (int i = 0; i < 3; i++)
                    {
                        GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 2).GetComponent<ItemController>());
                    }
                    // �������״̬
                    GameManager.Instance.PlotManager.PlotDictionary[11] = 2;
                    // �����������
                    GameManager.Instance.PlayerManager.Enable = true;
                    // ��Ƶ����
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
                });
                break;
            case 2:
                string talk = "�����и����˽�ң�ӵ���������ͽ�ӱ���";
                // ����˵��
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
                {
                    // �Ǳʼ�
                    GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
                    // �����������
                    GameManager.Instance.PlayerManager.Enable = true;
                    // ��Ƶ����
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "31-39");
                    // NPC ����
                    GameManager.Instance.PoolManager.RecycleResource(gameObject);
                });
                break;
            default:
                break;
        }
        return false;
    }
}
