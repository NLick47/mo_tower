using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemArtifact4 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        List<GameObject> tempObjs = new List<GameObject>();
        // ��ȡ���ܵ��ҽ�
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.up))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 15) tempObjs.Add(obj);
            }
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.down))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 15) tempObjs.Add(obj);
            }
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.left))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 15) tempObjs.Add(obj);
            }
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.right))
            {
                if (obj.GetComponent<EnvironmentController>() != null && obj.GetComponent<EnvironmentController>().ID == 15) tempObjs.Add(obj);
            }
        });
        // ������Դ
        foreach (var obj in tempObjs)
        {
            GameManager.Instance.PoolManager.RecycleResource(obj);
        }
        // ��ʾ��Ϣ
        int itemId = 13;
        GameManager.Instance.UIManager.ShowInfo($"ʹ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ����������Χ���ҽ���");
        return false;
    }
}
