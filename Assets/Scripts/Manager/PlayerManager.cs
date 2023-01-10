using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public enum EDirectionType
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
}

/// <summary>
/// �����Ϣ
/// </summary>
[Serializable]
public class PlayerInfo
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _attack;
    [SerializeField]
    private int _defence;
    [SerializeField]
    private int _gold;
    [SerializeField]
    private int _weaponID;
    [SerializeField]
    private int _armorID;
    [SerializeField]
    private string _notepadInfo;

    public int Health
    {
        get => _health;
        set
        {
            _health = value < 0 ? 0 : value;
            GameManager.Instance.EventManager.OnHealthChanged?.Invoke(_health);
        }
    }
    public int Attack
    {
        get => _attack;
        set
        {
            _attack = value < 0 ? 0 : value;
            GameManager.Instance.EventManager.OnAttackChanged?.Invoke(_attack);
        }
    }
    public int Defence
    {
        get => _defence;
        set
        {
            _defence = value < 0 ? 0 : value;
            GameManager.Instance.EventManager.OnDefenceChanged?.Invoke(_defence);
        }
    }
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            GameManager.Instance.EventManager.OnGoldChanged?.Invoke(_gold);
        }
    }
    public int WeaponID
    {
        get => _weaponID;
        set
        {
            _weaponID = value;
            GameManager.Instance.EventManager.OnWeaponChanged?.Invoke(_weaponID);
        }
    }
    public int ArmorID
    {
        get => _armorID;
        set
        {
            _armorID = value;
            GameManager.Instance.EventManager.OnArmorChanged?.Invoke(_armorID);
        }
    }
    public string NotepadInfo
    {
        get => _notepadInfo;
        set
        {
            _notepadInfo = value;
            GameManager.Instance.EventManager.OnNotepadChanged?.Invoke(_notepadInfo);
        }
    }
}

public class PlayerManager : Singleton<PlayerManager>
{
    public bool LockEnable;  // ���ؼ���
    private PlayerController _playerController;
    private bool _enable = true;
    private PlayerInfo _playerInfo;

    public PlayerInfo PlayerInfo
    {
        get => _playerInfo;
        set
        {
            _playerInfo = value;
            // �ֶ���ֵˢ�� UI
            _playerInfo.Health = _playerInfo.Health;
            _playerInfo.Attack = _playerInfo.Attack;
            _playerInfo.Defence = _playerInfo.Defence;
            _playerInfo.Gold = _playerInfo.Gold;
            _playerInfo.WeaponID = _playerInfo.WeaponID;
            _playerInfo.ArmorID = _playerInfo.ArmorID;
            _playerInfo.NotepadInfo = _playerInfo.NotepadInfo;
        }
    }
    public bool Enable
    {
        get => _enable;
        set { if (!LockEnable) _enable = value; }
    }
    public PlayerController PlayerController { get => _playerController; }

    /// <summary>
    /// �����
    /// </summary>
    /// <param name="playerController"></param>
    public void BindPlayer(PlayerController playerController)
    {
        this._playerController = playerController;
    }

    /// <summary>
    /// ������
    /// </summary>
    public void UnbindPlayer()
    {
        this._playerController = null;
    }

    /// <summary>
    /// �������
    /// </summary>
    public void CheckInput()
    {
        if (_playerController && _enable)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.UP);
            if (Input.GetKeyDown(KeyCode.DownArrow)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.DOWN);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.LEFT);
            if (Input.GetKeyDown(KeyCode.RightArrow)) GameManager.Instance.EventManager.OnMoveInput?.Invoke(EDirectionType.RIGHT);

            if (Input.GetKeyDown(KeyCode.S)) GameManager.Instance.EventManager.OnSaveGameInput?.Invoke();
            if (Input.GetKeyDown(KeyCode.Escape)) GameManager.Instance.EventManager.OnBackHomeInput?.Invoke();

            if (Input.GetKeyDown(KeyCode.PageUp)) GameManager.Instance.EventManager.OnArtifactUp?.Invoke();
            if (Input.GetKeyDown(KeyCode.PageDown)) GameManager.Instance.EventManager.OnArtifactDown?.Invoke();
        }
    }

    /// <summary>
    /// �����Ϣ�����±�
    /// </summary>
    /// <param name="info">��Ϣ����</param>
    public void AddInfoToNotepad(string info)
    {
        // �ж��Ƿ��м��±�
        if (!GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(11)) return;
        _playerInfo.NotepadInfo += $"{info}\n\r\n\r";

    }
}
