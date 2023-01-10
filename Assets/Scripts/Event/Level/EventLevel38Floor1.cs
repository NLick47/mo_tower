using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel38Floor1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ����ǽ��
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 6).transform.position = (Vector2)transform.position + Vector2.up;
            // ��������
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        }
    }
}
