using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel29Actor1 : ActorController
{
    public override bool Interaction()
    {
        if (GameManager.Instance.PlotManager.PlotDictionary[7] != 43)
        {
            // С͵˵��
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "����ȥ�����ط����ߣ��һ����ڰ�����" }, () =>
            {
                // �������������
                GameManager.Instance.PlayerManager.Enable = true;
                // ��Ƶ����
                GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "21-30");
            });
        }
        else
        {
            // С͵˵��
            GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "�Ҹ���ɰ�������ÿ�ζ���ʱ�ϵ���", "�������ѵķ��ϣ���������ʹ�á�", "�������ˣ��´��ټ�~" }, () =>
            {
                // ��ǽ��
                GameManager.Instance.PoolManager.UseList.ForEach(obj =>
                {
                    if (obj.GetComponent<EnvironmentController>() != null && (Vector2)obj.transform.position == new Vector2(transform.position.x, transform.position.y - 1)) obj.GetComponent<EnvironmentController>().Open(() =>
                    {
                        // �ƶ�
                        StartCoroutine(Moving());
                    });
                });
            });
        }
        return false;
    }

    /// <summary>
    /// �ƶ�
    /// </summary>
    IEnumerator Moving()
    {
        yield return StartCoroutine(Move(transform, new List<Vector2> { new Vector2(0, -5) }));
        // �������״̬
        GameManager.Instance.PlotManager.PlotDictionary[7] = 44;
        // С͵��ʧ
        _animator.SetTrigger("hide");
        // �������������
        GameManager.Instance.PlayerManager.Enable = true;
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "21-30");
        yield break;
    }

    IEnumerator Move(Transform transform, List<Vector2> targetPoints)
    {
        float timer = 0;
        float speed = 30f;
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
