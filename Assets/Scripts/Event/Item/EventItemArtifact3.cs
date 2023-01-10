using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemArtifact3 : MonoBehaviour, IInteraction
{
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnArtifactUp = OnUpEvent;
        GameManager.Instance.EventManager.OnArtifactDown = OnDownEvent;
    }

    public bool Interaction()
    {
        GameManager.Instance.UIManager.ShowInfo("վ��¥�ݿڣ��� [PageUP] �� [PageDown] ���п����ƶ���");
        return false;
    }

    private void OnUpEvent()
    {
        if (NearTheStair())
        {
            int nextIndex = GameManager.Instance.LevelManager.Level + 1;
            // 44 ����Ծ
            nextIndex = nextIndex == 44 ? 45 : nextIndex;
            // ���¥�㲻����
            if (nextIndex > GameManager.Instance.LevelManager.MaxLevel)
            {
                GameManager.Instance.UIManager.ShowInfo("ǰ����·��Ҫ�Լ�̽��Ŷ~");
                return;
            }
            // ��ȡ��һ����Ϣ
            LevelTransferInfo nextLevelInfo = GameManager.Instance.LevelManager.LevelTransferInfo[nextIndex];
            // �޸�������һ��λ�����ڴ���
            GameManager.Instance.ResourceManager.MovePlayerPointForLevel(nextIndex, nextLevelInfo.DownStairPoint);
            // ���͵���һ��
            GameManager.Instance.LevelManager.Level = nextIndex;
        }
        else GameManager.Instance.UIManager.ShowInfo("��վ��¥�ݿ�ʹ�÷���Ȩ�ȣ�");
    }
    private void OnDownEvent()
    {
        if (NearTheStair())
        {
            int nextIndex = GameManager.Instance.LevelManager.Level - 1;
            // 44 ����Ծ
            nextIndex = nextIndex == 44 ? 43 : nextIndex;
            // �Թ� 0 ��
            if (nextIndex == 0) return;
            // ��ȡ��һ����Ϣ
            LevelTransferInfo nextLevelInfo = GameManager.Instance.LevelManager.LevelTransferInfo[nextIndex];
            // �޸�������һ��λ�����ڴ���
            GameManager.Instance.ResourceManager.MovePlayerPointForLevel(nextIndex, nextLevelInfo.UpStairPoint);
            // ���͵���һ��
            GameManager.Instance.LevelManager.Level = nextIndex;
        }
        else GameManager.Instance.UIManager.ShowInfo("��վ��¥�ݿ�ʹ�÷���Ȩ�ȣ�");
    }

    /// <summary>
    /// �Ƿ���¥�ݸ���
    /// </summary>
    /// <returns></returns>
    private bool NearTheStair()
    {
        // ����ʹ�������б��а�λ�û�ȡ����
        foreach (var obj in GameManager.Instance.PoolManager.UseList)
        {
            if (obj.GetComponent<EnvironmentController>() != null && (obj.GetComponent<EnvironmentController>().ID == 7 || obj.GetComponent<EnvironmentController>().ID == 8) && (Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position+Vector2.up))
            {
                return true;
            }
            else if (obj.GetComponent<EnvironmentController>() != null && (obj.GetComponent<EnvironmentController>().ID == 7 || obj.GetComponent<EnvironmentController>().ID == 8) && (Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.down))
            {
                return true;
            }
            else if (obj.GetComponent<EnvironmentController>() != null && (obj.GetComponent<EnvironmentController>().ID == 7 || obj.GetComponent<EnvironmentController>().ID == 8) && (Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.left))
            {
                return true;
            }
            else if (obj.GetComponent<EnvironmentController>() != null && (obj.GetComponent<EnvironmentController>().ID == 7 || obj.GetComponent<EnvironmentController>().ID == 8) && (Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.right))
            {
                return true;
            }
        }
        return false;
    }
}
