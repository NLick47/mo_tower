using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public enum EEnvironmentType
{
    None,
    Wall,
    Door,
    Stairs,
}

public class EnvironmentController : ResourceController, IInteraction
{
    public EEnvironmentType type = EEnvironmentType.None;  // ��������

    public bool HasDirection = false;  // �Ƿ��п��ŷ���
    public EDirectionType OpenDirection;  // ���ŷ���
    public int KeyId;  // Կ�� id

    public bool isUp = false;  // �Ƿ���¥

    public Action OnOpened;  // ��ǽ�ڴ�ʱ

    protected Animator _animator;

    protected bool _opening = false;  // ��״̬

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected void OnEnable()
    {
        _opening = false;
    }
    protected void OnDisable()
    {
        OnOpened = null;
    }

    public virtual bool Interaction()
    {
        if (_opening) return false;
        switch (type)
        {
            case EEnvironmentType.None:
                return true;
            case EEnvironmentType.Wall:
                return false;
            case EEnvironmentType.Door:
                // �Ƿ��ܹ�����
                bool canOpen = false;
                // Կ���ж�
                if (KeyId != 0)
                {
                    if (GameManager.Instance.BackpackManager.ConsumeItem(KeyId))
                    {
                        canOpen = true;
                        GameManager.Instance.UIManager.ShowInfo($"�� {Name} ���� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, KeyId).Name} 1 ����");
                    }
                    else return false;
                }
                // ������ÿ��ŷ���
                if (HasDirection)
                {
                    // ���㿪�ŷ���
                    Vector2 direction = GameManager.Instance.PlayerManager.PlayerController.transform.position - transform.position;
                    switch (OpenDirection)
                    {
                        case EDirectionType.UP:
                            if (direction.y == 1) canOpen = true;
                            break;
                        case EDirectionType.DOWN:
                            if (direction.y == -1) canOpen = true;
                            break;
                        case EDirectionType.LEFT:
                            if (direction.x == -1) canOpen = true;
                            break;
                        case EDirectionType.RIGHT:
                            if (direction.x == 1) canOpen = true;
                            break;
                    }
                }
                else canOpen = true;
                if (canOpen)
                {
                    Open(null);
                    _opening = canOpen;
                }
                return false;
            case EEnvironmentType.Stairs:
                // ��ȡ��һ�����
                int nextIndex = isUp ? GameManager.Instance.LevelManager.Level + 1 : GameManager.Instance.LevelManager.Level - 1;
                // 44 ����Ծ
                int addNumber = nextIndex == 44 ? 2 : 1;
                nextIndex = isUp ? GameManager.Instance.LevelManager.Level + addNumber : GameManager.Instance.LevelManager.Level - addNumber;
                // ��ȡ��һ����Ϣ
                LevelTransferInfo nextLevelInfo = GameManager.Instance.LevelManager.LevelTransferInfo[nextIndex];
                // �޸�������һ��λ�����ڴ���
                GameManager.Instance.ResourceManager.MovePlayerPointForLevel(nextIndex, isUp ? nextLevelInfo.DownStairPoint : nextLevelInfo.UpStairPoint);
                // ���͵���һ��
                GameManager.Instance.LevelManager.Level = nextIndex;
                return false;
        }
        return false;
    }

    /// <summary>
    /// ��
    /// </summary>
    public void Open(Action callback)
    {
        // ���Ŷ���
        _animator.SetTrigger("open");
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "OpenTheDoor");
        // ��Ӵ�ǽ�ڻص�
        OnOpened += callback;
    }

    public void RecycleSelf()
    {
        // ִ�д򿪻ص�
        OnOpened?.Invoke();
        // ������Դ
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
    }
}
