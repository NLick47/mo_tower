using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventItemOther5 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        // ¥����Ծ
        GameManager.Instance.LevelManager.Level += 1;
        // ��ʾ��Ϣ���ӱ������
        int itemId = 21;
        GameManager.Instance.UIManager.ShowInfo($"ʹ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ������һ�㴫�͡�");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        return false;
    }
}
