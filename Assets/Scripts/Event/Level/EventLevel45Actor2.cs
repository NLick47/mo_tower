using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel45Actor2 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[12])
        {
            // ״̬ 1 ����
            case 1:
                GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "���������� 2000 ������ֵ��ֻ�� 1000 �������ܵõ�����", "������", "����Ҫ", () =>
                {
                    // ���� 1000 ���
                    if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 1000)
                    {
                        GameManager.Instance.UIManager.ShowInfo("��Ҳ���Ŷ~��ȥ����һ�°�~");
                        // ��Ƶ����
                        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                        return;
                    }
                    GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 1000;
                    // ��������
                    GameManager.Instance.PlayerManager.PlayerInfo.Health += 2000;
                    // �������״̬
                    GameManager.Instance.PlotManager.PlotDictionary[12] = 2;
                    // �����������
                    GameManager.Instance.PlayerManager.Enable = true;
                    // ��Ƶ����
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
                });
                break;
            case 2:
                string talk = "��ʥ��������ħ����������������������ڡ�";
                // ����˵��
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
                {
                    // �Ǳʼ�
                    GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
                    // �����������
                    GameManager.Instance.PlayerManager.Enable = true;
                    // ��Ƶ����
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "41-48");
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
