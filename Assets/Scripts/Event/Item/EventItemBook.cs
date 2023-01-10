using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����������Ϣ
/// </summary>
public class EnemyPropertyInfo
{
    public string Name;
    public string IconPath;
    public int Health;
    public int Attack;
    public int Denfence;
    public int Gold;
    public string Damage;
}

public class EventItemBook : MonoBehaviour, IInteraction
{
    public bool Interaction()
    {
        GameManager.Instance.UIManager.ShowBook(() =>
        {
            List<EnemyPropertyInfo> enemyPropertyInfos = new List<EnemyPropertyInfo>();
            // ��ȡ�������л��ŵĹ���
            List<EnemyController> tempEnemyController = new List<EnemyController>();
            foreach (var enemy in GameManager.Instance.PoolManager.UseList)
            {
                if (enemy.GetComponent<EnemyController>())
                {
                    bool has = false;
                    foreach (var tec in tempEnemyController)
                    {
                        if (tec.ID == enemy.GetComponent<EnemyController>().ID)
                        {
                            has = true;
                            break;
                        }
                    }
                    if (!has) tempEnemyController.Add(enemy.GetComponent<EnemyController>());
                }
            }
            // �����˺�
            foreach (var enemy in tempEnemyController)
            {
                enemyPropertyInfos.Add(new EnemyPropertyInfo
                {
                    Name = enemy.Name,
                    IconPath = enemy.IconPath,
                    Health = enemy.Health,
                    Attack = enemy.Attack,
                    Denfence = enemy.Defence,
                    Gold = enemy.Gold,
                    Damage = CalculateEnemyDamage(enemy),
                });
            }
            // ���������
            foreach (var enemyPropertyInfo in enemyPropertyInfos)
            {
                GameObject obj = Instantiate(Resources.Load("UI/EnemyInfoPanel"), GameManager.Instance.UIManager.BookContent) as GameObject;
                obj.transform.Find("IconImage").GetComponent<Image>().sprite = Instantiate(Resources.Load(enemyPropertyInfo.IconPath) as Sprite);
                obj.transform.Find("NameValueText").GetComponent<Text>().text = enemyPropertyInfo.Name;
                obj.transform.Find("HealthValueText").GetComponent<Text>().text = enemyPropertyInfo.Health.ToString();
                obj.transform.Find("AttackValueText").GetComponent<Text>().text = enemyPropertyInfo.Attack.ToString();
                obj.transform.Find("DefenceValueText").GetComponent<Text>().text = enemyPropertyInfo.Denfence.ToString();
                obj.transform.Find("GoldValueText").GetComponent<Text>().text = enemyPropertyInfo.Gold.ToString();
                obj.transform.Find("DamageValueText").GetComponent<Text>().text = enemyPropertyInfo.Damage;
            }
        });
        return false;
    }

    /// <summary>
    /// ��������˺�
    /// </summary>
    /// <param name="enemy">���˿�����</param>
    /// <returns>�����˺�����</returns>
    private string CalculateEnemyDamage(EnemyController enemy)
    {
        // ��������˺�
        int playerDamage = GameManager.Instance.PlayerManager.PlayerInfo.Attack - enemy.Defence;
        playerDamage = playerDamage < 0 ? 0 : playerDamage;
        CalculateJudge(enemy, ref playerDamage);
        // ��������˺�
        int enemyDamage = enemy.Attack - GameManager.Instance.PlayerManager.PlayerInfo.Defence;
        enemyDamage = enemyDamage < 0 ? 0 : enemyDamage;
        // �������˺�Ϊ 0
        if (playerDamage == 0)
        {
            // ��������˺�ҲΪ 0
            if (enemyDamage == 0) return "�޽�";
            // ������������
            else return "����";
        }
        // ����غ���
        int round = enemy.Health % playerDamage > 0 ? enemy.Health / playerDamage : enemy.Health / playerDamage - 1;
        return (round * enemyDamage).ToString();
    }

    /// <summary>
    /// ����ʮ�ּܶ���Ѫ�����˵��˺�
    /// </summary>
    /// <param name="damage">�˺� �������� ֱ�ӷ���</param>
    private void CalculateJudge(EnemyController enemy,ref int damage)
    {
        // �ж϶Է��Ƿ�����Ѫ��
        if (enemy.ID == 12 || enemy.ID == 13 || enemy.ID == 16)
        {
            // �жϱ����Ƿ���ʮ�ּ� �˺�����
            if (GameManager.Instance.BackpackManager.BackpackDictionary.ContainsKey(19)) damage *= 2;
        }
    }
}
