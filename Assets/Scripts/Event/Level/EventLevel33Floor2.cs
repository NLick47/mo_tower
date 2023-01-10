using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EventLevel33Floor2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ����ħ����
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 22).transform.position = (Vector2)transform.position + Vector2.up;
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 22).transform.position = (Vector2)transform.position - Vector2.up * 3;
            // �������������
            GameManager.Instance.PlayerManager.Enable = true;
            // ��������
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        }
    }
}