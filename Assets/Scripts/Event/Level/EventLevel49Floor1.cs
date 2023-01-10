using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel49Floor1 : MonoBehaviour
{
    private EnemyController _enemy1;  // ħ��
    private EnemyController _enemy2;  // ħ������
    private EnemyController _enemy3;
    private EnemyController _enemy4;
    private EnemyController _enemy5;
    private EnemyController _enemy6;
    private EnemyController _enemy7;
    private EnemyController _enemy8;
    private EnemyController _enemy9;
    private EnvironmentController _door;

    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnResourceLoaded += GetGameObjectEvent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.PlotManager.PlotDictionary[14] == 1)
        {
            // �������������
            GameManager.Instance.PlayerManager.Enable = false;
            // ��������
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
        // �ı����
        GameManager.Instance.PlotManager.PlotDictionary[14] = 2;
        // ��������
        _enemy1 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 33).GetComponent<EnemyController>();
        _enemy1.transform.position = new Vector2(0, 3);
        _enemy2 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
        _enemy2.transform.position = new Vector2(-1, 4);
        _enemy3 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
        _enemy3.transform.position = new Vector2(0, 4);
        _enemy4 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
        _enemy4.transform.position = new Vector2(1, 4);
        _enemy5 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
        _enemy5.transform.position = new Vector2(-1, 3);
        _enemy6 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
        _enemy6.transform.position = new Vector2(1, 3);
        _enemy7 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
        _enemy7.transform.position = new Vector2(-1, 2);
        _enemy8 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
        _enemy8.transform.position = new Vector2(0, 2);
        _enemy9 = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Enemy, 31).GetComponent<EnemyController>();
        _enemy9.transform.position = new Vector2(1, 2);
        // ���¼�
        BindOnDeathEvent();
        // ����ħ����
        _door = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Environment, 4).GetComponent<EnvironmentController>();
        _door.transform.position = Vector2.down;
        // ħ��˵��
        GameManager.Instance.UIManager.ShowDialog(_enemy1.Name, new List<string> { "���������ˣ�", "���Ѿ��Ȳ����ˣ�", "���ҵ�����Ҫ�������棬��������Ҫ�ȴ�������ˡ�" }, (() =>
        {
            // �������������
            GameManager.Instance.PlayerManager.Enable = true;
        }));
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
        _door = null;
        // ����ʹ�������б��а�λ�û�ȡ����
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(0, 3)) _enemy1 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-1, 4)) _enemy2 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(0, 4)) _enemy3 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(1, 4)) _enemy4 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-1, 3)) _enemy5 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(1, 3)) _enemy6 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(-1, 2)) _enemy7 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(0, 2)) _enemy8 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == new Vector2(1, 2)) _enemy9 = obj.GetComponent<EnemyController>();
            else if ((Vector2)obj.transform.position == Vector2.down) _door = obj.GetComponent<EnvironmentController>();
        });
        // ���¼�
        BindOnDeathEvent();
    }

    private void BindOnDeathEvent()
    {
        if (_enemy1 != null)
        {
            _enemy1.OnDeath += () =>
            {
                // ħ��˵��
                GameManager.Instance.UIManager.ShowDialog(_enemy1.Name, new List<string> { "��������", "�ܺã�����һ���ϸ��սʿ��" }, (() =>
                {
                    // �������������
                    GameManager.Instance.PlayerManager.Enable = true;
                    // �ı����
                    GameManager.Instance.PlotManager.PlotDictionary[14] = 3;
                    // ��������
                    GameManager.Instance.SoundManager.LockEnable = false;
                    // ��Ƶ����
                    GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "LevelWin");
                    // �������е���
                    if (_enemy1 != null) GameManager.Instance.PoolManager.RecycleResource(_enemy1.gameObject);
                    if (_enemy2 != null) GameManager.Instance.PoolManager.RecycleResource(_enemy2.gameObject);
                    if (_enemy3 != null) GameManager.Instance.PoolManager.RecycleResource(_enemy3.gameObject);
                    if (_enemy4 != null) GameManager.Instance.PoolManager.RecycleResource(_enemy4.gameObject);
                    if (_enemy5 != null) GameManager.Instance.PoolManager.RecycleResource(_enemy5.gameObject);
                    if (_enemy6 != null) GameManager.Instance.PoolManager.RecycleResource(_enemy6.gameObject);
                    if (_enemy7 != null) GameManager.Instance.PoolManager.RecycleResource(_enemy7.gameObject);
                    if (_enemy8 != null) GameManager.Instance.PoolManager.RecycleResource(_enemy8.gameObject);
                    if (_enemy9 != null) GameManager.Instance.PoolManager.RecycleResource(_enemy9.gameObject);
                    // ����
                    _door.Open(null);
                    // �����챦ʯ
                    for (int i = 0; i < 3; i++)
                    {
                        GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 7).transform.position = new Vector2(-4 + i, 2);
                    }
                    // ������Ѫƿ
                    for (int i = 0; i < 3; i++)
                    {
                        GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 6).transform.position = new Vector2(-1 + i, 1);
                    }
                    // ��������ʯ
                    for (int i = 0; i < 3; i++)
                    {
                        GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 8).transform.position = new Vector2(2 + i, 2);
                    }
                    // ������Կ��
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 3).transform.position = new Vector2(-1, 4);
                    // ��������ذ��
                    GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Item, 20).transform.position = new Vector2(1, 4);
                    // ���վ�����Դ
                    GameManager.Instance.PoolManager.RecycleResource(gameObject);
                }));
            };
        }
        if (_enemy2 != null) _enemy2.OnDeath += () => { _enemy2 = null; DetectionWeakness(); };
        if (_enemy3 != null) _enemy3.OnDeath += () => { _enemy3 = null; DetectionWeakness(); };
        if (_enemy4 != null) _enemy4.OnDeath += () => { _enemy4 = null; DetectionWeakness(); };
        if (_enemy5 != null) _enemy5.OnDeath += () => { _enemy5 = null; DetectionWeakness(); };
        if (_enemy6 != null) _enemy6.OnDeath += () => { _enemy6 = null; DetectionWeakness(); };
        if (_enemy7 != null) _enemy7.OnDeath += () => { _enemy7 = null; DetectionWeakness(); };
        if (_enemy8 != null) _enemy8.OnDeath += () => { _enemy8 = null; DetectionWeakness(); };
        if (_enemy9 != null) _enemy9.OnDeath += () => { _enemy9 = null; DetectionWeakness(); };
    }

    /// <summary>
    /// ������Ƿ��ܴ�
    /// </summary>
    private void DetectionWeakness()
    {
        if (_enemy1 != null && _enemy2 != null && _enemy3 == null && _enemy4 != null && _enemy5 == null && _enemy6 == null && _enemy7 != null && _enemy8 == null && _enemy9 != null)
        {
            // ��������
            GameManager.Instance.SoundManager.LockEnable = true;
            // ħ��˵��
            GameManager.Instance.UIManager.ShowDialog(_enemy1.Name, new List<string> { "ʲô������ô����ӡ�ˣ�" }, (() =>
            {
                // �������������
                GameManager.Instance.PlayerManager.Enable = true;
                // ħ������
                _enemy1.Health /= 10;
                _enemy1.Attack /= 10;
                _enemy1.Defence /= 10;
            }));
        }
    }
}
