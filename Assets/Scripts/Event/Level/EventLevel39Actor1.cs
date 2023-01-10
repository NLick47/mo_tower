using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel39Actor1 : ActorController
{
    public override bool Interaction()
    {
        string talk = "���3���ӣ����͵��ر��ͻ���֡�";
        // ��ͷ˵��
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
        return false;
    }
}
