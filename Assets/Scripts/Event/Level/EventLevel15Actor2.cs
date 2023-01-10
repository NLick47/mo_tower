using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel15Actor2 : ActorController
{
    public override bool Interaction()
    {
        // �������������
        GameManager.Instance.PlayerManager.Enable = false;
        // С͵��˵��
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "����~�����㡣", "������㵲ס���ҵ�ȥ·��������һ�������������Ҳ���ŵ㡣", "��Ҫ���ˣ��ݰ�~" }, () =>
        {
            // С͵��ʧ
            StartCoroutine(HideNPC());
        });
        return false;
    }

    IEnumerator HideNPC()
    {
        // ��ǽ
        yield return StartCoroutine(OpenWall());
        yield return new WaitForSeconds(1 / 3);
        // �ƶ�
        yield return StartCoroutine(Move(transform, new List<Vector2> { new Vector2(0, 5) }));
        GetComponent<Animator>().SetTrigger("hide");
        // �������������
        GameManager.Instance.PlayerManager.Enable = true;
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "11-19");
        // ��Դ����
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
        yield break;
    }

    IEnumerator OpenWall()
    {
        // ����ʹ�������б��а�λ�û�ȡ����
        GameManager.Instance.PoolManager.UseList.ForEach(obj =>
        {
            if ((Vector2)obj.transform.position == new Vector2(transform.position.x - 1, transform.position.y))
            {
                if (obj.GetComponent<EnvironmentController>() != null)
                {
                    obj.GetComponent<EnvironmentController>().Open(null);
                }
            }
        });
        yield break;
    }

    IEnumerator Move(Transform transform, List<Vector2> targetPoints)
    {
        float timer = 0;
        float speed = 10f;
        Vector2 beginPoint = transform.position;
        for (int i = 0; i < targetPoints.Count; i++)
        {
            timer = 0;
            beginPoint = transform.position;
            while ((Vector2)transform.position != targetPoints[i])
            {
                timer += Time.deltaTime;
                float t = timer / (beginPoint - targetPoints[i]).sqrMagnitude * speed;
                transform.position = Vector2.Lerp(beginPoint, targetPoints[i], t);
                yield return null;
            }
        }
        yield break;
    }
}
