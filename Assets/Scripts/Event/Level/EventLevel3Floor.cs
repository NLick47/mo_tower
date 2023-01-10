using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel3Floor : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �������������
            GameManager.Instance.PlayerManager.Enable = false;
            // ħ������
            _animator.SetTrigger("showDevil");
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "Trap");
            // ������Ƶ
            GameManager.Instance.SoundManager.LockEnable = true;
        }
    }

    /// <summary>
    /// ħ���ճ���ʱ˵��
    /// </summary>
    public void DevilShowTalk()
    {
        // ħ��˵��
        GameManager.Instance.UIManager.ShowDialog("ħ��", new List<string> { "��ӭ����ħ�������ǵ� 114514 λ��ս�ҵ���ʿ��", "����ܴ���ҵ����£��Ҿ�����һ��һ�ľ�����" }, (() =>
        {
            // ��ɫ˵��
            GameManager.Instance.UIManager.ShowDialog("����", new List<string> { "ʲô��" }, () =>
            {
                // ħ���ر�����
                _animator.SetTrigger("showMagicGuard");
            });
        }));
    }

    public void MagicAttack()
    {
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Magic");
    }

    /// <summary>
    /// ��ұ����
    /// </summary>
    public void PlayerFail()
    {
        // ������Ƶ
        GameManager.Instance.SoundManager.LockEnable = false;
        // ���վ�����Դ
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
        // �޸��������
        GameManager.Instance.PlayerManager.PlayerInfo.Health = 400;
        GameManager.Instance.PlayerManager.PlayerInfo.Attack = 10;
        GameManager.Instance.PlayerManager.PlayerInfo.Defence = 10;
        GameManager.Instance.PlayerManager.PlayerInfo.WeaponID = 0;
        GameManager.Instance.PlayerManager.PlayerInfo.ArmorID = 0;
        // �ƶ������Դλ�� 2 ¥
        GameManager.Instance.ResourceManager.MovePlayerPointForLevel(2, new Vector2(-3, -2));
        // ¥�㴫��
        GameManager.Instance.LevelManager.Level = 2;
        // �ƶ������Դλ�� 3 ¥
        GameManager.Instance.ResourceManager.MovePlayerPointForLevel(3, new Vector2(-4, -5));
        // ����С͵�Ի�
        GameManager.Instance.UIManager.ShowDialog("С͵", new List<string> { "ι��", "��һ�ѣ�����" }, () =>
        {
            // �������������
            GameManager.Instance.PlayerManager.Enable = true;
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
        });
    }
}
