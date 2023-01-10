using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemOther1 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        // ��ȡ���ܵ�ǽ
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(GameManager.Instance.PlayerManager.PlayerController.transform.position.x + 1, GameManager.Instance.PlayerManager.PlayerController.transform.position.y))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 6) obj.GetComponent<EnvironmentController>().Open(null);
            }
            else if ((Vector2)obj.transform.position == new Vector2(GameManager.Instance.PlayerManager.PlayerController.transform.position.x - 1, GameManager.Instance.PlayerManager.PlayerController.transform.position.y))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 6) obj.GetComponent<EnvironmentController>().Open(null);
            }
            else if ((Vector2)obj.transform.position == new Vector2(GameManager.Instance.PlayerManager.PlayerController.transform.position.x, GameManager.Instance.PlayerManager.PlayerController.transform.position.y + 1))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 6) obj.GetComponent<EnvironmentController>().Open(null);
            }
            else if ((Vector2)obj.transform.position == new Vector2(GameManager.Instance.PlayerManager.PlayerController.transform.position.x, GameManager.Instance.PlayerManager.PlayerController.transform.position.y - 1))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 6) obj.GetComponent<EnvironmentController>().Open(null);
            }
        });
        // ��ʾ��Ϣ���ӱ������
        int itemId = 13;
        GameManager.Instance.UIManager.ShowInfo($"ʹ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} 1 �ѣ�������Χ��ǽ�ڡ�");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        return false;
    }
}
