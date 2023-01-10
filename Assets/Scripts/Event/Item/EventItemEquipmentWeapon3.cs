using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemEquipmentWeapon3 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        int itemId = 28;
        int giveAttack = 40;
        GameManager.Instance.PlayerManager.PlayerInfo.Attack += giveAttack;
        GameManager.Instance.UIManager.ShowInfo($"װ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ������ {giveAttack} �㹥������");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        GameManager.Instance.PlayerManager.PlayerInfo.WeaponID = itemId;
        return false;
    }
}
