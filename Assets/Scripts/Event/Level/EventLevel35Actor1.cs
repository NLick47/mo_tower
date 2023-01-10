using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel35Actor1 : ActorController
{
    public override bool Interaction()
    {
        // С͵˵��
        GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "�����ں��ˣ�������ƹ�ħ���ˡ�", "����˵��ʿ�ӳ�ʵ�����ְ���ţ�����Ա�ħ�����������ᡣ", "ħ��̫Σ���ˣ��ҿɲ����ٴα�ץ����Ҫ���ˣ��ټ���" }, () =>
        {
            StartCoroutine(GoOut());
        });
        return false;
    }

    IEnumerator GoOut()
    {
        // �ƶ�
        yield return StartCoroutine(Move(transform, new List<Vector2> { new Vector2(-1, -5), new Vector2(0, -5) }));
        // �����������
        GameManager.Instance.PlayerManager.Enable = true;
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "31-39");
        // NPC ����
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
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
