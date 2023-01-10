using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemBottle1 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        int itemId = 5;
        int giveHealth = 200;
        GameManager.Instance.PlayerManager.PlayerInfo.Health += giveHealth;
        GameManager.Instance.UIManager.ShowInfo($"ʹ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} 1 �����ָ� {giveHealth} ������ֵ��");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "GiveHealth");
        return false;
    }
}
