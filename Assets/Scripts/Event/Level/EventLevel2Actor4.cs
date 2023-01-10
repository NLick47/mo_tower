using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel2Actor4 : ActorController
{
    public override bool Interaction()
    {
        GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "��л������ң����������� 3% �Ĺ������ͷ�����������Ҫ��", "�м�����", "�´���˵", () =>
        {
            // ��������
            GameManager.Instance.PlayerManager.PlayerInfo.Attack = (int)(GameManager.Instance.PlayerManager.PlayerInfo.Attack * 1.03f);
            GameManager.Instance.PlayerManager.PlayerInfo.Defence = (int)(GameManager.Instance.PlayerManager.PlayerInfo.Defence * 1.03f);
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
            // �����������
            GameManager.Instance.PlayerManager.Enable = true;
            // NPC ����
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}
