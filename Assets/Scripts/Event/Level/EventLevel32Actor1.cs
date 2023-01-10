using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel32Actor1 : ActorController
{
    public override bool Interaction()
    {
        // ���̵����
        GameManager.Instance.EventManager.OnShopShow?.Invoke(GetComponent<ActorController>().Name, 100, ShopShowCallback);
        return false;
    }

    /// <summary>
    /// ���̵�ص�
    /// </summary>
    private void ShopShowCallback()
    {
        // �жϽ���Ƿ��㹻
        if (GameManager.Instance.PlayerManager.PlayerInfo.Gold < 100)
        {
            GameManager.Instance.UIManager.ShowInfo($"���������� 100 ��Ҷ�û�аɣ�");
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
            return;
        }
        GameManager.Instance.PlayerManager.PlayerInfo.Gold -= 100;
        // ���һ������
        ERandomShopType type = ERandomShopType.Health;
        int randomNumber = Random.Range(0, 100);
        if (randomNumber <= 30) type = ERandomShopType.Health;
        else if (randomNumber > 30 && randomNumber <= 60) type = ERandomShopType.Attack;
        else if (randomNumber > 60 && randomNumber <= 90) type = ERandomShopType.Defence;
        else type = ERandomShopType.Gold;
        // �����ֵ
        switch (type)
        {
            case ERandomShopType.Health:
                randomNumber = Random.Range(200, 800);
                GameManager.Instance.PlayerManager.PlayerInfo.Health += randomNumber;
                GameManager.Instance.UIManager.ShowInfo($"��� {randomNumber} ������ֵ������");
                break;
            case ERandomShopType.Attack:
                randomNumber = Random.Range(8, 40);
                GameManager.Instance.PlayerManager.PlayerInfo.Attack += randomNumber;
                GameManager.Instance.UIManager.ShowInfo($"��� {randomNumber} �㹥����������");
                break;
            case ERandomShopType.Defence:
                randomNumber = Random.Range(8, 40);
                GameManager.Instance.PlayerManager.PlayerInfo.Defence += randomNumber;
                GameManager.Instance.UIManager.ShowInfo($"��� {randomNumber} �������������");
                break;
            case ERandomShopType.Gold:
                randomNumber = Random.Range(80, 400);
                GameManager.Instance.PlayerManager.PlayerInfo.Gold += randomNumber;
                GameManager.Instance.UIManager.ShowInfo($"���������� {randomNumber} ��ң�");
                break;
            default:
                print("�̵�����˸�ʲô�������");
                break;
        }
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
    }
}