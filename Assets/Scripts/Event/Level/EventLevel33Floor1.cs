using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel33Floor1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ����ǽ��
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 19).transform.position = (Vector2)transform.position + Vector2.left;
            // �������������
            GameManager.Instance.PlayerManager.Enable = true;
            // ��������
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        }
    }
}
