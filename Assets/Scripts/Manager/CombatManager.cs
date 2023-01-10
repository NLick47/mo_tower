using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoSingleton<CombatManager>
{
    private bool _fighting = false;
    private float _combatInterval = .25f;
    private EnemyController _enemy;
    private bool _attackerIsPlayer = true;

    public bool Fighting { get => _fighting; }

    /// <summary>
    /// ��ʼս��
    /// </summary>
    /// <param name="enemyController">���˿�����</param>
    public void StartFight(EnemyController enemyController)
    {
        _enemy = enemyController;
        // ��ʼ�� UI
        GameManager.Instance.EventManager.OnEnemyCombated?.Invoke(_enemy);
        // ��ʼս��
        _fighting = true;
        // ������ҿ�����
        GameManager.Instance.PlayerManager.Enable = false;
        StartCoroutine(InTheFighting());
    }

    /// <summary>
    /// ս��
    /// </summary>
    IEnumerator InTheFighting()
    {
        while (_fighting)
        {
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Attack");
            yield return new WaitForSeconds(_combatInterval);
            // ��һغ�
            if (_attackerIsPlayer)
            {
                int damage = GameManager.Instance.PlayerManager.PlayerInfo.Attack - _enemy.Defence;
                damage = damage >= 0 ? damage : 0;
                // ӵ��ʮ�ּ�ʱ����Ѫ��������˺�����
                CalculateJudge(ref damage);
                _enemy.SetHealth(_enemy.Health - damage);
                GameManager.Instance.EventManager.OnEnemyCombated?.Invoke(_enemy);
                if (_enemy.Health <= 0)
                {
                    StopFight();
                    yield break;
                }
            }
            // ���˻غ�
            else
            {
                int damage = _enemy.Attack - GameManager.Instance.PlayerManager.PlayerInfo.Defence;
                damage = damage >= 0 ? damage : 0;
                GameManager.Instance.PlayerManager.PlayerInfo.Health -= damage;
                if (GameManager.Instance.PlayerManager.PlayerInfo.Health == 0)
                {
                    StopFight();
                    yield break;
                }
            }
            // �л�������
            _attackerIsPlayer = !_attackerIsPlayer;
        }
    }

    /// <summary>
    /// ����ս��
    /// </summary>
    private void StopFight()
    {
        _fighting = false;
        // ���ʤ�� ʧ�ܲ��ڴ˴��ж� ֱ�Ӱ��������ֵ
        if (_attackerIsPlayer)
        {
            // ���� UI
            GameManager.Instance.EventManager.OnEnemyCombated?.Invoke(null);
            GameManager.Instance.UIManager.ShowInfo($"���� {_enemy.Name} ��� {(GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(18) ? _enemy.Gold * 2 : _enemy.Gold)} �ͽ�");
            // ���ｱ��
            GameManager.Instance.PlayerManager.PlayerInfo.Gold += GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(18) ? _enemy.Gold * 2 : _enemy.Gold;
            // ɾ������
            GameManager.Instance.PoolManager.RecycleResource(_enemy.gameObject);
        }
        // ������ҿ�����
        GameManager.Instance.PlayerManager.Enable = true;
    }

    /// <summary>
    /// �������������������˺�
    /// </summary>
    /// <param name="damage">�˺� �������� ֱ�ӷ���</param>
    private void CalculateJudge(ref int damage)
    {
        // �ж϶Է��Ƿ�����Ѫ��
        if (_enemy.ID == 12 || _enemy.ID == 13 || _enemy.ID == 16)
        {
            // �жϱ����Ƿ���ʮ�ּ� �˺�����
            if (GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(19)) damage *= 2;
        }
        // �ж϶Է��Ƿ���ħ��
        else if (_enemy.ID == 23)
        {
            // �жϱ����Ƿ�������ذ�� �˺�����
            if (GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(20)) damage *= 2;
        }
    }
}
