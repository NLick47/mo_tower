using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemOther3 : MonoBehaviour, IInteraction
{
    // �ԳƷ���
    public bool Interaction()
    {
        // ��ȡ�Գ�����
        Vector2 point = GameManager.Instance.PlayerManager.PlayerController.transform.position * -1;
        // �ж������Ƿ���Դ���
        foreach (var obj in GameManager.Instance.PoolManager.UseList)
        {
            if ((Vector2)obj.transform.position == point)
            {
                GameManager.Instance.UIManager.ShowInfo("ֻ�пյز��ܴ���Ŷ~");
                return false;
            }
        }
        // ����
        GameManager.Instance.PlayerManager.PlayerController.transform.position = point;
        // ��ʾ��Ϣ
        GameManager.Instance.UIManager.ShowInfo($"ʹ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, 23).Name} ���ƶ���Ŀ��㡣");
        GameManager.Instance.BackpackManager.ConsumeItem(23);
        return false;
    }
}
