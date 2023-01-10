using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel2Actor1 : ActorController
{
    public override bool Interaction()
    {
        switch (GameManager.Instance.PlotManager.PlotDictionary[1])
        {
            // ״̬ 1 δ�ӳ�����
            case 1:
                // С͵˵��
                GameManager.Instance.UIManager.ShowDialog(GetComponent<ResourceController>().Name, new List<string> { "�����ˣ�", "�㱻ħ�������ؽ���ʱ�����ڻ����С�", "�Ҹո�����һ������������һ���ӳ�ȥ�ɡ�" }, () =>
                {
                    // ��ǽ
                    RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(-1, 0), Vector2.left, .1f);
                    hit.collider.GetComponent<EnvironmentController>().Open(() =>
                    {
                        // �ƶ�
                        StartCoroutine(Moving1());
                    });
                });
                break;
            case 2:
                // С͵˵��
                GameManager.Instance.UIManager.ShowDialog("С͵", new List<string> { "�����ӳ����ˣ�", "���װ�������������ˣ����������ҵ���ֵ�װ����", "������ 5 ¥�������� 9 ¥��", "�һ����������ˣ�ף�����~" }, () =>
                {
                    // �ƶ�
                    StartCoroutine(Moving2());
                });
                break;
            default:
                break;
        }
        return false;
    }

    /// <summary>
    /// �ƶ� 1
    /// </summary>
    IEnumerator Moving1()
    {
        yield return StartCoroutine(Move(transform, new List<Vector2> { new Vector2(-5, -1), new Vector2(-5, -3) }));
        // �������״̬
        GameManager.Instance.PlotManager.PlotDictionary[1] = 2;
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
        // �������������
        GameManager.Instance.PlayerManager.Enable = true;
        yield break;
    }

    /// <summary>
    /// �ƶ� 2
    /// </summary>
    IEnumerator Moving2()
    {
        yield return StartCoroutine(Move(transform, new List<Vector2> { new Vector2(-5, -5) }));
        // С͵��ʧ
        _animator.SetTrigger("hide");
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "1-9");
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
