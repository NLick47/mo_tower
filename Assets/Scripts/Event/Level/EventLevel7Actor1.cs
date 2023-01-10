using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel7Actor1 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[3])
        {
            // ״̬ 1 ����
            case 1:
                GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "������ѻ�Կ�ף�ֻ�� 50 �������ܵõ�����", "������", "����Ҫ", () =>
                {
                    // ���� 50 ���
                    if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 50)
                    {
                        GameManager.Instance.UIManager.ShowInfo("��Ҳ���Ŷ~��ȥ����һ�°�~");
                        // ��Ƶ����
                        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                        return;
                    }
                    GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 50;
                    // ��� ��Կ�� 5 ��
                    for (int i = 0; i < 5; i++)
                    {
                        GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).GetComponent<ItemController>());
                    }
                    // �������״̬
                    GameManager.Instance.PlotManager.PlotDictionary[3] = 2;
                    // �����������
                    GameManager.Instance.PlayerManager.Enable = true;
                    // ��Ƶ����
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
                    // ��Ƶ����
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
                });
                break;
            case 2:
                string talk = "�������������������ֻ���ڹ��������ڵ��˵ķ�����ʱ��������������";
                // ����˵��
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
                {
                    // �Ǳʼ�
                    GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
                    // �����������
                    GameManager.Instance.PlayerManager.Enable = true;
                    // ��Ƶ����
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
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
