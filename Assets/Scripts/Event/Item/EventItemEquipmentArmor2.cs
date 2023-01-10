using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemEquipmentArmor2 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        int itemId = 27;
        int giveDefence = 20;
        GameManager.Instance.PlayerManager.PlayerInfo.Defence += giveDefence;
        GameManager.Instance.UIManager.ShowInfo($"װ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ������ {giveDefence} ���������");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        GameManager.Instance.PlayerManager.PlayerInfo.ArmorID = itemId;
        return false;
    }
}
