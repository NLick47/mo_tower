using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevel28Actor1 : ActorController
{
    public override bool Interaction()
    {
        GameManager.Instance.UIManager.ShowInteractionDialog(GetComponent<ResourceController>().Name, "�һ�������������ԣ���ͬ����Ҳ����ȡ��һ�������ԣ���ȷ��Ҫ������", "˭��˭", "������", () =>
        {
            // �����������
            string getStr = "", giveStr = "";
            ERandomShopType getType = ERandomShopType.Health, giveType = ERandomShopType.Health;
            int getNumber = Random.Range(0, 100);
            int giveNumber = Random.Range(0, 100);
            int randomNumber1 = Random.Range(0, 100);
            int randomNumber2 = Random.Range(0, 100);
            if (randomNumber1 <= 33) getType = ERandomShopType.Health;
            else if (randomNumber1 > 33 && randomNumber1 <= 66) getType = ERandomShopType.Attack;
            else if (randomNumber1 > 66) getType = ERandomShopType.Defence;
            if (randomNumber2 <= 33) giveType = ERandomShopType.Health;
            else if (randomNumber2 > 33 && randomNumber2 <= 66) giveType = ERandomShopType.Attack;
            else if (randomNumber2 > 66) giveType = ERandomShopType.Defence;
            // ��ȡ����
            switch (getType)
            {
                case ERandomShopType.Health:
                    getStr = "����ֵ";
                    getNumber *= 10;
                    GameManager.Instance.PlayerManager.PlayerInfo.Health -= getNumber;
                    break;
                case ERandomShopType.Attack:
                    getStr = "������";
                    GameManager.Instance.PlayerManager.PlayerInfo.Attack -= getNumber;
                    break;
                case ERandomShopType.Defence:
                    getStr = "������";
                    GameManager.Instance.PlayerManager.PlayerInfo.Defence -= getNumber;
                    break;
            }
            // ��ȡ����
            switch (giveType)
            {
                case ERandomShopType.Health:
                    giveStr = "����ֵ";
                    giveNumber *= 10;
                    GameManager.Instance.PlayerManager.PlayerInfo.Health += giveNumber;
                    break;
                case ERandomShopType.Attack:
                    giveStr = "������";
                    GameManager.Instance.PlayerManager.PlayerInfo.Attack += giveNumber;
                    break;
                case ERandomShopType.Defence:
                    giveStr = "������";
                    GameManager.Instance.PlayerManager.PlayerInfo.Defence += giveNumber;
                    break;
            }
            // ��ʾ��ʾ
            GameManager.Instance.UIManager.ShowInfo($"��ʧ {getNumber} ��{getStr},��� {giveNumber} ��{giveStr}��");
            // �����������
            GameManager.Instance.PlayerManager.Enable = true;
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Yes");
        });
        return false;
    }
}
