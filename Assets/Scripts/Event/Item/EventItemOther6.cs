using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventItemOther6 : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        List<GameObject> tempObjs = new List<GameObject>();
        // ��ȡ���ܵĹ���
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.up) && obj.GetComponent<EnemyController>() != null && obj.GetComponent<IExplosionproof>() == null) tempObjs.Add(obj);
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.down) && obj.GetComponent<EnemyController>() != null && obj.GetComponent<IExplosionproof>() == null) tempObjs.Add(obj);
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.left) && obj.GetComponent<EnemyController>() != null && obj.GetComponent<IExplosionproof>() == null) tempObjs.Add(obj);
            else if ((Vector2)obj.transform.position == ((Vector2)GameManager.Instance.PlayerManager.PlayerController.transform.position + Vector2.right) && obj.GetComponent<EnemyController>() != null && obj.GetComponent<IExplosionproof>() == null) tempObjs.Add(obj);
        });
        // ������Դ
        foreach (var obj in tempObjs)
        {
            obj.GetComponent<EnemyController>().Health = 0;
            GameManager.Instance.PoolManager.RecycleResource(obj);
        }
        // ��ʾ��Ϣ���ӱ������
        int itemId = 16;
        GameManager.Instance.UIManager.ShowInfo($"ʹ�� {GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, itemId).Name} ����ɱ����Χ�ĵ��ˡ�");
        GameManager.Instance.BackpackManager.ConsumeItem(itemId);
        return false;
    }
}
