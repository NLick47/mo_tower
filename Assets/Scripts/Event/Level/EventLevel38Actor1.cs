using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel38Actor1 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[10])
        {
            // ״̬ 1 ����
            case 1:
                GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "�������ѻ�Կ�ף�ֻ�� 200 �������ܵõ�����", "������", "����Ҫ", () =>
                {
                    // ���� 200 ���
                    if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 200)
                    {
                        GameManager.Instance.UIManager.ShowInfo("��Ҳ���Ŷ~��ȥ����һ�°�~");
                        // ��Ƶ����
                        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                        return;
                    }
                    GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 200;
                    // ��� ��Կ�� 3 ��
                    for (int i = 0; i < 3; i++)
                    {
                        GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).GetComponent<ItemController>());
                    }
                    // �������״̬
                    GameManager.Instance.PlotManager.PlotDictionary[10] = 2;
                    // �����������
                    GameManager.Instance.PlayerManager.Enable = true;
                    // ��Ƶ����
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
                });
                break;
            case 2:
                string talk = "�����佣�ķ����Ż��ˣ�������ø���ǽ���롣";
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
