using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel32Floor1 : MonoBehaviour
{
    GameObject _obj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ������Ƶ����
            GameManager.Instance.SoundManager.LockEnable = true;
            // �������������
            GameManager.Instance.PlayerManager.Enable = false;
            // ��ʿ�������
            StartCoroutine(TriggerTrap());
        }
    }

    IEnumerator TriggerTrap()
    {
        // ������ʿ�ӳ�
        _obj = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 25);
        _obj.transform.position = new Vector2(5, 5);
        // ��ʿ�ӳ��ƶ�
        yield return StartCoroutine(Move(_obj.transform, new List<Vector2> { new Vector2(0, 5) }));
        // �Ի�
        GameManager.Instance.UIManager.ShowDialog(_obj.GetComponent<ResourceController>().Name, new List<string> { "�������Ȼ���������ͷĿ��", "����������Ϸ�����ˣ��ҽ�����ɱ���㣡" }, () =>
        {
            StartCoroutine(AttackPlayer());
        });
        yield break;
    }

    IEnumerator AttackPlayer()
    {
        // ���������ƶ�
        GameManager.Instance.PlayerManager.LockEnable = true;
        _obj.GetComponent<EnemyController>().OnDeath += () =>
        {
            // ��ʿ�ӳ�˵��
            GameManager.Instance.UIManager.ShowDialog(_obj.GetComponent<ResourceController>().Name, new List<string> { "����Ϊ��ǳ�ǿ����", "��ֻ�ǽ���״̬���Ѷ��ѡ�", "�б��µ� 40 ��������ս��" }, (() =>
            {
                StartCoroutine(RunAway());
            }));
        };
        // ��ʿ�ӳ��ƶ�
        yield return StartCoroutine(Move(_obj.transform, new List<Vector2> { transform.position }, 40));
    }

    IEnumerator RunAway()
    {
        _obj = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 25);
        _obj.transform.position = (Vector2)transform.position + Vector2.up;
        yield return StartCoroutine(Move(_obj.transform, new List<Vector2> { new Vector2(0, 5), new Vector2(5, 5) }, 40));
        // ��������
        GameManager.Instance.PoolManager.RecycleResource(_obj);
        // ������Ƶ����
        GameManager.Instance.SoundManager.LockEnable = false;
        // ������������ƶ�
        GameManager.Instance.PlayerManager.LockEnable = false;
        // �������������
        GameManager.Instance.PlayerManager.Enable = true;
        // ��������
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
    }

    IEnumerator Move(Transform transform, List<Vector2> targetPoints, float speed = 20f)
    {
        float timer = 0;
        Vector2 beginPoint = transform.position;
        for (int i = 0; i < targetPoints.Count; i++)
        {
            timer = 0;
            beginPoint = transform.position;
            while ((Vector2)transform.position != targetPoints[i])
            {
                timer += Time.deltaTime;
                float t = timer / (beginPoint - targetPoints[i]).sqrMagnitude * speed;
                transform.position = Vector2.Lerp(beginPoint, targetPoints[i], t);
                yield return null;
            }
        }
        yield break;
    }
}
