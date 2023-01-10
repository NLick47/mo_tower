using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel25Floor1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �������������
            GameManager.Instance.PlayerManager.Enable = false;
            // ��������
            GameManager.Instance.SoundManager.LockEnable = true;
            // ��ʼ�Ի�
            GameManager.Instance.UIManager.ShowDialog("������", new List<string> { "ɱ�������룡�֣��ߣ�" }, (() =>
            {
                // �������������
                GameManager.Instance.PlayerManager.Enable = true;
                // ���վ�����Դ
                GameManager.Instance.PoolManager.RecycleResource(gameObject);
            }));
        }
    }
}
