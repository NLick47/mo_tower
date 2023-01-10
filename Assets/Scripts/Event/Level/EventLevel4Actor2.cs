using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel4Actor2 : ActorController
{
    public override bool Interaction()
    {
        string talk = "��Щ���޷���Կ�״򿪣����ǻ������������Զ��򿪡�";
        // ��ͷ˵��
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
        return false;
    }
}
