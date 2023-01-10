using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel12Actor2 : ActorController
{
    public override bool Interaction()
    {
        GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "���� 1 ��ң���������͸���ʲô��", "�󱦽���", "��֪��~", () =>
        {
            // ���� 1 ���
            if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 1)
            {
                GameManager.Instance.UIManager.ShowInfo("������߿�!");
                // ��Ƶ����
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
                return;
            }
            GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 1;
            int randomInt = UnityEngine.Random.Range(1, 100);
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
            if (randomInt == 88)
            {
                GameManager.Instance.PlayerManager.PlayerInfo.Gold += 88;
                GameManager.Instance.UIManager.ShowInfo("����������~��� 88 ��ҡ�");
                // �����������
                GameManager.Instance.PlayerManager.Enable = true;
                // NPC ����
                GameManager.Instance.PoolManager.RecycleResource(gameObject);
            }
        });
        return false;
    }
}
