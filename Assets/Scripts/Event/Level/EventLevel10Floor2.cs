using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLevel10Floor2 : MonoBehaviour
{
    private GameObject _npc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ״̬һ�����Ի�
        if (GameManager.Instance.PlotManager.PlotDictionary[4] == 5)
        {
            if (collision.CompareTag("Player"))
            {
                // �������������
                GameManager.Instance.PlayerManager.Enable = false;
                // С͵����
                StartCoroutine(ShowNPC());
            }
        }
    }

    IEnumerator ShowNPC()
    {
        // ����С͵
        _npc = GameManager.Instance.PoolManager.GetResourceInFreePool(EResourceType.Actor, 12);
        _npc.transform.position = new Vector2(-5, -5);
        _npc.GetComponent<Animator>().SetTrigger("show");
        yield return new WaitForSeconds(1 / 3);
        // С͵�ƶ�
        Vector2[] points = new Vector2[]
        {
            new Vector2(-5,-5),
            new Vector2(-5,-2),
            new Vector2(-3,-2),
            new Vector2(-3,-5),
            new Vector2(-1,-5),
            new Vector2(-1,-4),
            new Vector2(0,-4),
        };
        yield return StartCoroutine(Move(_npc.transform, points.ToList()));
        // С͵��˵��
        GameManager.Instance.UIManager.ShowDialog(_npc.GetComponent<ResourceController>().Name, new List<string> { "�٣������ּ����ˣ�", "�㾹Ȼ�����˴������ͷĿ�����˲���", "�����ڷ�����ôȥ���ߵ�¥�㣬�������ڿ�����ȥ�ˡ�", "��˵������ 11 ¥�������� 17 ¥��ף�����~" }, () =>
        {
            // С͵��ʧ
            StartCoroutine(HideNPC());
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

    IEnumerator HideNPC()
    {
        yield return StartCoroutine(Move(_npc.transform, new List<Vector2> { new Vector2(0, -5) }));
        _npc.GetComponent<Animator>().SetTrigger("hide");
        // �������������
        GameManager.Instance.PlayerManager.Enable = true;
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "LevelWin");
        // ��Դ����
        GameManager.Instance.PoolManager.RecycleResource(gameObject);
        yield break;
    }
}
