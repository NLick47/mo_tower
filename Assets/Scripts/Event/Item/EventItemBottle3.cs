using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemBottle3 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        int itemId = 17;
        int giveHealth = GameManager.Instance.PlayerManager.PlayerInfo.Attack + GameManager.Instance.PlayerManager.PlayerInfo.Defence;
        GameManager.Instance.PlayerManager.PlayerInfo.Health += giveHealth;
        GameManager.Instance.UIManager.ShowInfo($"ʹ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} 1 �����ָ� {giveHealth} ������ֵ��");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "GiveHealth");
        return false;
    }
}
