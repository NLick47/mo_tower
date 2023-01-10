using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    /// <summary>
    /// �ƶ������¼�
    /// </summary>
    public Action<EDirectionType> OnMoveInput;

    /// <summary>
    /// ����ƶ�����¼�
    /// </summary>
    public Action<Vector2> OnPlayerArrive;

    /// <summary>
    /// ������Ϸ�����¼�
    /// </summary>
    public Action OnSaveGameInput;
    /// <summary>
    /// �ص��˵������¼�
    /// </summary>
    public Action OnBackHomeInput;

    /// <summary>
    /// �������ֵ�䶯�¼�
    /// </summary>
    public Action<int> OnHealthChanged;
    /// <summary>
    /// ��ҹ������䶯�¼�
    /// </summary>
    public Action<int> OnAttackChanged;
    /// <summary>
    /// ��ҷ������䶯�¼�
    /// </summary>
    public Action<int> OnDefenceChanged;
    /// <summary>
    /// ��ҽ�Ǯ�䶯�¼�
    /// </summary>
    public Action<int> OnGoldChanged;
    /// <summary>
    /// ��������䶯�¼�
    /// </summary>
    public Action<int> OnWeaponChanged;
    /// <summary>
    /// ��ҷ��߱䶯�¼�
    /// </summary>
    public Action<int> OnArmorChanged;

    /// <summary>
    /// �ʼǱ��䶯�¼�
    /// </summary>
    public Action<string> OnNotepadChanged;

    /// <summary>
    /// ������Ʒ�䶯�¼�
    /// </summary>
    public Action<int, ItemInfo> OnItemChanged;

    /// <summary>
    /// ��ս���˸ı��¼� ���ڸ��� UI
    /// </summary>
    public Action<EnemyController> OnEnemyCombated;

    /// <summary>
    /// �ؿ��ı��¼� ���� 1 �Ǿ�ֵ ���� 2 ����ֵ
    /// </summary>
    public Action<int, int> OnLevelChanged;

    /// <summary>
    /// ��Դ��������¼�
    /// </summary>
    public Action OnResourceLoaded;

    /// <summary>
    /// ���̵��¼�
    /// </summary>
    public Action<string, int, Action> OnShopShow;

    /// <summary>
    /// ����Ȩ����¥�¼�
    /// </summary>
    public Action OnArtifactUp;
    /// <summary>
    /// ����Ȩ����¥�¼�
    /// </summary>
    public Action OnArtifactDown;

    /// <summary>
    /// ��Ѫ������¼�
    /// </summary>
    public Action OnVampireShow;

    public void RemoveAllEvent(Action action)
    {
        if (null != action)
        {
            Delegate[] ds = action?.GetInvocationList();
            for (int i = 0; i < ds.Length; i++)
            {
                action -= ds[i] as Action;
            }
        }
    }
}
