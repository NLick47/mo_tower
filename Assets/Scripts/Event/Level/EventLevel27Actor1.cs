using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel27Actor1 : ActorController
{
    public override bool Interaction()
    {
        string talk = "�����������һ�㻹ӵ�� 1500 ����ֵ��80 ��������98 ��������1 ����Կ�ס�5 �ѻ�Կ�׵Ļ�������ǳɹ���ʿ��";
        // ��ͷ˵��
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { talk }, () =>
        {
            // �Ǳʼ�
            GameManager.Instance.PlayerManager.AddInfoToNotepad(talk);
            // �����������
            GameManager.Instance.PlayerManager.Enable = true;
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "21-30");
            // NPC ����
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        });
        return false;
    }
}