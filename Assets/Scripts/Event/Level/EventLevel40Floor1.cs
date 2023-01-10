using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel40Floor1 : MonoBehaviour
{
    private EnemyController _enemy1;  // ��սʿ
    private EnemyController _enemy2;
    private EnemyController _enemy3;
    private EnemyController _enemy4;  // սʿ
    private EnemyController _enemy5;
    private EnemyController _enemy6;
    private EnemyController _enemy7;  // ��ʿ
    private EnemyController _enemy8;
    private EnemyController _enemy9;
    private EnemyController _enemy10;  // ��ʿ
    private EnemyController _enemy11;
    private EnemyController _enemy12;
    private EnemyController _enemy13;  // ��ʿ�ӳ�

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
            // �������������
            GameManager.Instance.PlayerManager.LockEnable = true;
            // ������Ƶ����
            GameManager.Instance.SoundManager.LockEnable = true;
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
        // ���öӳ�˵��
        GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "�һ��ڵ�������������һ�㣬�����ҵ͹����ˡ�", "���һ���ܴ���㡣", "��սʿ�������ϣ�" }, (() =>
        {
            Attack();
        }));
    }

    private void Attack()
    {
        // �����¼�
        _enemy1.OnDeath += () =>
        {
            _enemy2.OnDeath += () =>
            {
                _enemy3.OnDeath += () =>
                {
                    // ���öӳ�˵��
                    GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "�ߣ�û��ϵ��սʿ�ǣ������ϣ�" }, (() =>
                    {
                        // �����¼�
                        _enemy4.OnDeath += () =>
                        {
                            _enemy5.OnDeath += () =>
                            {
                                _enemy6.OnDeath += () =>
                                {
                                    // ���öӳ�˵��
                                    GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "������ս���Ÿոտ�ʼ����ʿ�ǣ������ϣ�" }, (() =>
                                    {
                                        // �����¼�
                                        _enemy7.OnDeath += () =>
                                        {
                                            _enemy8.OnDeath += () =>
                                            {
                                                _enemy9.OnDeath += () =>
                                                {
                                                    // ���öӳ�˵��
                                                    GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "�㣬����ô��ù��ģ�", "��ʿ�ǣ����ҳ壡" }, (() =>
                                                    {
                                                        // �����¼�
                                                        _enemy10.OnDeath += () =>
                                                        {
                                                            _enemy11.OnDeath += () =>
                                                            {
                                                                _enemy12.OnDeath += () =>
                                                                {
                                                                    // ���öӳ�˵��
                                                                    GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "������ô�����ҵ����µģ���", "�Һ����Ʋ���������ʧȥ���ǣ�" }, (() =>
                                                                    {
                                                                        _enemy13.OnDeath += () =>
                                                                        {
                                                                            // ���öӳ�˵��
                                                                            GameManager.Instance.UIManager.ShowDialog(_enemy13.Name, new List<string> { "����������㣬�´��ٺ�����ʽ�����������Ͷ���������ܣ�" }, (() =>
                                                                            {
                                                                                // ���Ŷ���Э��
                                                                                StartCoroutine(GoOut());
                                                                            }));
                                                                        };
                                                                        StartCoroutine(Move(_enemy13.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }, 30));
                                                                    }));
                                                                };
                                                                StartCoroutine(Move(_enemy12.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                                            };
                                                            StartCoroutine(Move(_enemy11.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                                        };
                                                        // �ƶ�
                                                        StartCoroutine(Move(_enemy10.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                                    }));
                                                };
                                                StartCoroutine(Move(_enemy9.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                            };
                                            StartCoroutine(Move(_enemy8.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                        };
                                        // �ƶ�
                                        StartCoroutine(Move(_enemy7.transform, new List<Vector2> { new Vector2(0, 4), new Vector2(0, -1) }, 30));
                                    }));
                                };
                                StartCoroutine(Move(_enemy6.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
                            };
                            StartCoroutine(Move(_enemy5.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
                        };
                        // �ƶ�
                        StartCoroutine(Move(_enemy4.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
                    }));
                };
                StartCoroutine(Move(_enemy3.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
            };
            StartCoroutine(Move(_enemy2.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
        };
        // �ƶ�
        StartCoroutine(Move(_enemy1.transform, new List<Vector2> { new Vector2(0, 2), new Vector2(0, -1) }));
    }

    IEnumerator GoOut()
    {
        // ������ʿ�ӳ�
        GameObject obj = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 25);
        obj.transform.position = (Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.up;
        // ����¥��
        GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 7).transform.position = new Vector2(0, 5);
        // �ƶ�
        yield return StartCoroutine(Move(obj.transform, new List<Vector2> { new Vector2(0, 5) }, 40));
        // ������Դ
        GameManager.Instance.PoolManager.RecycleResource(obj);
        // ������Կ��
        for (int i = 0; i < 3; i++)
        {
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 1).transform.position = new Vector2(-4 + i, 4);
        }
        // �����챦ʯ
        for (int i = 0; i < 3; i++)
        {
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 7).transform.position = new Vector2(2 + i, 4);
        }
        // ������Ѫƿ
        for (int i = 0; i < 3; i++)
        {
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 6).transform.position = new Vector2(-3 + i, 2);
        }
        // ��������ʯ
        for (int i = 0; i < 3; i++)
        {
            GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 8).transform.position = new Vector2(1 + i, 2);
        }
        // �ı����״̬
        GameManager.Instance.PlotManager.PlotDictionary[16] = 2;
        // �������������
        GameManager.Instance.PlayerManager.LockEnable = false;
        // �������������
        GameManager.Instance.PlayerManager.Enable = true;
        // ������Ƶ����
        GameManager.Instance.SoundManager.LockEnable = false;
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "LevelWin");
        // ���վ�����Դ
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
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
        _enemy2 = null;
        _enemy3 = null;
        _enemy4 = null;
        _enemy5 = null;
        _enemy6 = null;
        _enemy7 = null;
        _enemy8 = null;
        _enemy9 = null;
        _enemy10 = null;
        _enemy11 = null;
        _enemy12 = null;
        _enemy13 = null;
        // ����ʹ�������б��а�λ�û�ȡ����
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(-1, 2)) _enemy1 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-2, 2)) _enemy2 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-3, 2)) _enemy3 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(1, 2)) _enemy4 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(2, 2)) _enemy5 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(3, 2)) _enemy6 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-2, 4)) _enemy7 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-3, 4)) _enemy8 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-4, 4)) _enemy9 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(2, 4)) _enemy10 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(3, 4)) _enemy11 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(4, 4)) _enemy12 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(0, 5)) _enemy13 = obj.GetComponent<EnemyController>();
        });
    }
}
