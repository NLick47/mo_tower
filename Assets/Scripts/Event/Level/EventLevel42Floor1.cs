using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel42Floor1 : MonoBehaviour
{
    private EnemyController _enemy1;  // ��ʿ�ӳ�
    private EnemyController _enemy2;  // ħ��
    private EnemyController _enemy3;  // ħ������
    private EnemyController _enemy4;
    private EnemyController _enemy5;
    private EnemyController _enemy6;

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetGameObjectEvent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ������Ƶ����
            GameManager.Instance.SoundManager.LockEnable = true;
            // �������������
            GameManager.Instance.PlayerManager.Enable = false;
            // �������������
            GameManager.Instance.PlayerManager.LockEnable = true;
            // ��ʼ�Ի�
            ShowTalk();
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded -= GetGameObjectEvent;
    }

    /// <summary>
    /// ˵��
    /// </summary>
    private void ShowTalk()
    {
        // ��ʿ�ӳ�˵��
        GameManager.Instance.UIManager.ShowDialog(_enemy1.Name, new List<string> { "���������㣡���������ܣ�" }, (() =>
        {
            StartCoroutine(GoOut());
        }));
    }

    IEnumerator GoOut()
    {
        // �ƶ�
        yield return StartCoroutine(Move(_enemy1.transform, new List<Vector2> { (Vector2)_enemy1.transform.position + Vector2.up * 2 }, 20));
        // ħ������
        _enemy2 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 33).GetComponent<EnemyController>();
        _enemy2.transform.position = Vector2.zero;
        GameManager.Instance.UIManager.ShowDialog(_enemy2.Name, new List<string> { "����������ӣ�" }, (() =>
        {
            GameManager.Instance.UIManager.ShowDialog(_enemy1.Name, new List<string> { "�ҵ������Ҵ򲻹���������У������Ұɣ�" }, (() =>
            {
                GameManager.Instance.UIManager.ShowDialog(_enemy2.Name, new List<string> { "����˵һ�Σ�", "ħ������Ҫ�������İ��࣡", "�����ţ����Ҵ�������" }, (() =>
                  {
                      // ����ħ������
                      _enemy3 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
                      _enemy3.transform.position = (Vector2)_enemy1.transform.position + Vector2.up;
                      _enemy4 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
                      _enemy4.transform.position = (Vector2)_enemy1.transform.position + Vector2.down;
                      _enemy5 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
                      _enemy5.transform.position = (Vector2)_enemy1.transform.position + Vector2.left;
                      _enemy6 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
                      _enemy6.transform.position = (Vector2)_enemy1.transform.position + Vector2.right;
                      GameManager.Instance.UIManager.ShowDialog(_enemy1.Name, new List<string> { "�ҵ������ٸ���һ�λ��ᡭ��" }, (() =>
                      {
                          // ��ʾ��Ч
                          GameObject obj1 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 31);
                          obj1.transform.position = _enemy1.transform.position;
                          obj1.GetComponent<MagicController>().ShowMagic(EDirectionType.UP);
                          GameObject obj2 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 31);
                          obj2.transform.position = _enemy1.transform.position;
                          obj2.GetComponent<MagicController>().ShowMagic(EDirectionType.DOWN);
                          GameObject obj3 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 31);
                          obj3.transform.position = _enemy1.transform.position;
                          obj3.GetComponent<MagicController>().ShowMagic(EDirectionType.LEFT);
                          GameObject obj4 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 31);
                          obj4.transform.position = _enemy1.transform.position;
                          obj4.GetComponent<MagicController>().ShowMagic(EDirectionType.RIGHT);
                          // ��ʿ�ӳ�����
                          StartCoroutine(KnightCaptainDeath());
                      }));
                  }));
            }));
        }));
        yield break;
    }

    IEnumerator KnightCaptainDeath()
    {
        yield return new WaitForSeconds(1);
        // ���վ�����Դ
        GameManager.Instance.PoolManager.RecycleResource(_enemy1.gameObject);
        // ħ��˵��
        GameManager.Instance.UIManager.ShowDialog(_enemy2.Name, new List<string> { "�ո�ֻ�ǽ�ѵ���£����ģ�����������ǲ����Զ����ٵġ�", "�ټ���" }, (() =>
        {
            // ���վ�����Դ
            GameManager.Instance.PoolManager.RecycleResource(_enemy2.gameObject);
            GameManager.Instance.PoolManager.RecycleResource(_enemy3.gameObject);
            GameManager.Instance.PoolManager.RecycleResource(_enemy4.gameObject);
            GameManager.Instance.PoolManager.RecycleResource(_enemy5.gameObject);
            GameManager.Instance.PoolManager.RecycleResource(_enemy6.gameObject);
            // ���վ�����Դ
            GameManager.Instance.PoolManager.RecycleResource(gameObject);
            // �������������
            GameManager.Instance.PlayerManager.LockEnable = false;
            // �������������
            GameManager.Instance.PlayerManager.Enable = true;
            // ������Ƶ����
            GameManager.Instance.SoundManager.LockEnable = false;
        }));
        yield break;
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

    /// <summary>
    /// ��ȡ�����¼�
    /// </summary>
    private void GetGameObjectEvent()
    {
        _enemy1 = null;
        // ����ʹ�������б��а�λ�û�ȡ����
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(0, -4)) _enemy1 = obj.GetComponent<EnemyController>();
        });
    }
}
