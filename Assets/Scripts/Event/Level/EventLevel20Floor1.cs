using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel20Floor1 : MonoBehaviour
{
    private EnemyController _enemyBat11;
    private EnemyController _enemyBat12;
    private EnemyController _enemyBat13;
    private EnemyController _enemyBat14;
    private EnemyController _enemyBat15;
    private EnemyController _enemyBat16;
    private EnemyController _enemyBat17;
    private EnemyController _enemyBat18;

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetGameObjectEvent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �������������
            GameManager.Instance.PlayerManager.Enable = false;
            // ������Ƶ����
            GameManager.Instance.SoundManager.LockEnable = true;
            // ��Ѫ�����
            StartCoroutine(TriggerTrap());
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= GetGameObjectEvent;
    }

    IEnumerator TriggerTrap()
    {
        // �������ƶ�
        StartCoroutine(Move(_enemyBat11.transform, new List<Vector2> { new Vector2(0, 0) }));
        StartCoroutine(Move(_enemyBat12.transform, new List<Vector2> { new Vector2(0, 0) }));
        StartCoroutine(Move(_enemyBat13.transform, new List<Vector2> { new Vector2(0, 0) }));
        StartCoroutine(Move(_enemyBat14.transform, new List<Vector2> { new Vector2(0, 0) }));
        StartCoroutine(Move(_enemyBat15.transform, new List<Vector2> { new Vector2(0, 0) }));
        StartCoroutine(Move(_enemyBat16.transform, new List<Vector2> { new Vector2(0, 0) }));
        StartCoroutine(Move(_enemyBat17.transform, new List<Vector2> { new Vector2(0, 0) }));
        yield return StartCoroutine(Move(_enemyBat18.transform, new List<Vector2> { new Vector2(0, 0) }));
        // ��������ʧ
        GameManager.Instance.PoolManager.RecycleResource(_enemyBat11.gameObject);
        GameManager.Instance.PoolManager.RecycleResource(_enemyBat12.gameObject);
        GameManager.Instance.PoolManager.RecycleResource(_enemyBat13.gameObject);
        GameManager.Instance.PoolManager.RecycleResource(_enemyBat14.gameObject);
        GameManager.Instance.PoolManager.RecycleResource(_enemyBat15.gameObject);
        GameManager.Instance.PoolManager.RecycleResource(_enemyBat16.gameObject);
        GameManager.Instance.PoolManager.RecycleResource(_enemyBat17.gameObject);
        GameManager.Instance.PoolManager.RecycleResource(_enemyBat18.gameObject);
        // ����ħ����
        GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 18).transform.position = new Vector2(0, -3);
        // ������Ѫ��
        GameObject obj = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 16);
        // ��Ѫ��˵��
        GameManager.Instance.UIManager.ShowDialog(obj.GetComponent<ResourceController>().Name, new List<string> { "�ܺã�����Ȼ����������壬�����޷�սʤ�ҡ��Ҿ�������񣡣���" }, (() =>
        {
            // �������������
            GameManager.Instance.PlayerManager.Enable = true;
            // ��������
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
        }));
        yield break;
    }

    IEnumerator Move(Transform transform, List<Vector2> targetPoints)
    {
        float timer = 0;
        float speed = 10f;
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

    /// <summary>
    /// ��ȡ�����¼�
    /// </summary>
    private void GetGameObjectEvent()
    {
        _enemyBat11 = null;
        _enemyBat12 = null;
        _enemyBat13 = null;
        _enemyBat14 = null;
        _enemyBat15 = null;
        _enemyBat16 = null;
        _enemyBat17 = null;
        _enemyBat18 = null;
        // ����ʹ�������б��а�λ�û�ȡ����
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(-1, 1)) _enemyBat11 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(0, 1)) _enemyBat12 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(1, 1)) _enemyBat13 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-1, 0)) _enemyBat14 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(1, 0)) _enemyBat15 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-1, -1)) _enemyBat16 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(0, -1)) _enemyBat17 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(1, -1)) _enemyBat18 = obj.GetComponent<EnemyController>();
        });
    }
}
