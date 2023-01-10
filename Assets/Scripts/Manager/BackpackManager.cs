using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʒ��Ϣ
/// </summary>
[Serializable]
public class ItemInfo
{
    public int ID;
    public string Name;
    public string Info;
    public string IconPath;
    public int UseCount;
}

public class BackpackManager : Singleton<BackpackManager>
{
    [SerializeField]
    public Dictionary<int, ItemInfo> BackpackDictionary = new Dictionary<int, ItemInfo>();

    /// <summary>
    /// ������Ʒ
    /// </summary>
    /// <param name="item">��Ʒ������</param>
    public void PickUp(ItemController item)
    {
        ItemInfo itemInfo = TransferItemControllerToItemInfo(item);
        // �������û���򴴽�
        if (!BackpackDictionary.ContainsKey(itemInfo.ID))
        {
            // ���������ֹ���ô���
            BackpackDictionary.Add(itemInfo.ID, itemInfo);
            GameManager.Instance.EventManager.OnItemChanged?.Invoke(itemInfo.ID, itemInfo);
        }
        // ���������������
        else
        {
            BackpackDictionary[itemInfo.ID].UseCount += itemInfo.UseCount;
            GameManager.Instance.EventManager.OnItemChanged?.Invoke(itemInfo.ID, BackpackDictionary[itemInfo.ID]);
        }
        // �����������ʧ
        GameManager.Instance.PoolManager.RecycleResource(item.gameObject);
        // UI ��ʾ
        GameManager.Instance.UIManager.ShowInfo($"��� {item.Name} {(item.UseCount < 0 ? 1 : item.UseCount)} ��");
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "PickUp");
    }

    /// <summary>
    /// ������Ʒ
    /// </summary>
    /// <param name="itemID">��Ʒ ID</param>
    /// <returns>�Ƿ����ĳɹ�</returns>
    public bool ConsumeItem(int itemID)
    {
        // �������������������
        if (BackpackDictionary.ContainsKey(itemID))
        {
            BackpackDictionary[itemID].UseCount -= 1;
            GameManager.Instance.EventManager.OnItemChanged?.Invoke(itemID, BackpackDictionary[itemID]);
            // �������Ϊ 0 ��ɾ�� С�� 0 Ϊ����ʹ�õ���Ʒ
            if (BackpackDictionary[itemID].UseCount == 0) BackpackDictionary.Remove(itemID);
            return true;
        }
        // ������򷵻� false
        else return false;
    }

    /// <summary>
    /// ת����Ʒ������Ϊ��Ʒ��Ϣ
    /// </summary>
    /// <param name="item">��Ʒ������</param>
    /// <returns>��Ʒ��Ϣ</returns>
    public ItemInfo TransferItemControllerToItemInfo(ItemController item)
    {
        return new ItemInfo
        {
            ID = item.ID,
            Name = item.Name,
            Info = item.Info,
            IconPath = item.IconPath,
            UseCount = item.UseCount,
        };
    }
}
