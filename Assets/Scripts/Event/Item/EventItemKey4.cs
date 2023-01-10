using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemKey4 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        List<EnvironmentController> environmentControllers = new List<EnvironmentController>();
        // ��ȡ�������л�ɫ��
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 1) environmentControllers.Add(obj.GetComponent<EnvironmentController>());
        });
        // ��������
        environmentControllers.ForEach(ec =>
        {
            ec.Open(null);
        });
        // ��ʾ��Ϣ���ӱ������
        int itemId = 4;
        GameManager.Instance.UIManager.ShowInfo($"ʹ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} 1 �ѣ��������еĻ�ɫ�š�");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        return false;
    }
}
