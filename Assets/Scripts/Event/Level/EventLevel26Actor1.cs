using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel26Actor1 : ActorController
{
    public override bool Interaction()
    {
        // �������������
        GameManager.Instance.PlayerManager.Enable = false;
        // ��������
        GameManager.Instance.SoundManager.LockEnable = true;
        // ��ʼ�Ի�
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "��ѡ�ٵĺ��ӣ������������ˡ�����㲻�����������㽫��Խʱ�����������" }, () =>
        {
            // ˵��
            GameManager.Instance.UIManager.ShowDialog(GameManager.Instance.PlayerManager.PlayerController.Name, new List<string> { "ʲô�����Ǹ������ޣ�" }, () =>
            {
                // �������������
                GameManager.Instance.PlayerManager.Enable = true;
                // ��������
                GameManager.Instance.SoundManager.LockEnable = false;
                if (GameManager.Instance.PlotManager.PlotDictionary[18] == 1)
                {
                    // �ı����
                    GameManager.Instance.PlotManager.PlotDictionary[18] = 2;
                }
            });
        });
        return false;
    }
}
