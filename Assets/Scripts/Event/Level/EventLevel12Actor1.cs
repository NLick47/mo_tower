using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel12Actor1 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[5])
        {
            // ״̬ 1 ����
            case 1:
                GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "����һ�Ѻ�Կ�ף�ֻ�� 800 �������ܵõ�����", "������", "�е��", () =>
                {
                    // ���� 800 ���
                    if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 800)
                    {
                        GameManager.Instance.UIManager.ShowInfo("��Ҳ���Ŷ~��ȥ����һ�°�~");
                        // ��Ƶ����
                        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                        return;
                    }
                    GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 800;
                    // ��� ��Կ�� 5 ��
                    GameManager.Instance.BackpackManager.PickUp(GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 3).GetComponent<ItemController>());
                    // �������״̬
                    GameManager.Instance.PlotManager.PlotDictionary[5] = 2;
                    // �����������
                    GameManager.Instance.PlayerManager.Enable = true;
                });
                break;
            case 2:
                string talk = "���Ƿ�ע�⵽ 5��9��14��16��18 ��ǽ�����ڲ�ͬ��";
                // ����˵��
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
                {
                    // �Ǳʼ�
                    GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
                    // �����������
                    GameManager.Instance.PlayerManager.Enable = true;
                    // ��Ƶ����
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "11-19");
                    // NPC ����
                    GameManager.Instance.PoolManager.RecycleResource(gameObject);
                });
                break;
        }
        return false;
    }
}
