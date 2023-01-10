using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemOther2 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        List<EnvironmentController> environmentControllers = new List<EnvironmentController>();
        // ��ȡ��������ǽ
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 6) environmentControllers.Add(obj.GetComponent<EnvironmentController>());
        });
        // ������ǽ
        environmentControllers.ForEach(ec =>
        {
            ec.Open(null);
        });
        // ��ʾ��Ϣ���ӱ������
        int itemId = 14;
        GameManager.Instance.UIManager.ShowInfo($"ʹ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} 1 �ѣ��������е�ǽ��");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        return false;
    }
}
