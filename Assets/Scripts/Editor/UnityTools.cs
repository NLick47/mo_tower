using OfficeOpenXml;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



public class UnityTools : MonoBehaviour
{
    static string _mapInfoPath = Application.dataPath + "/Settings/��ͼ��Ϣ.txt";
    static string _propertyInfoPath = Application.dataPath + "/Settings/�����б�.txt";
    static string _propertyListPath = "Assets/Settings/�����б�.xlsx";

    [UnityEditor.MenuItem("�ҵĹ���/ת����ǰ��ͼ��ϢΪ Json �ı��ļ�")]
    public static void TransferMapInfoToJsonTxt()
    {
        List<MapResourceInfo> mapInfos = new List<MapResourceInfo>();
        // ��ȡ������Ϣ
        GameObject levelObj = GameObject.Find("Level");
        int levelNum = 0;
        while (true)
        {
            // ����Ż�ȡ level
            Transform levelChildren = levelObj.transform.Find(levelNum.ToString());
            if (levelChildren != null)
            {
                ResourceController[] resources = levelChildren.GetComponentsInChildren<ResourceController>();
                foreach (var resource in resources)
                {
                    MapResourceInfo mapInfo = new MapResourceInfo
                    {
                        Level = levelNum,
                        ID = resource.ID,
                        Point = resource.transform.position,
                    };
                    switch (resource.tag)
                    {
                        case "Environment":
                            mapInfo.Type = EResourceType.Environment;
                            break;
                        case "Item":
                            mapInfo.Type = EResourceType.Item;
                            break;
                        case "Enemy":
                            mapInfo.Type = EResourceType.Enemy;
                            break;
                        case "Player":
                            mapInfo.Type = EResourceType.Actor;
                            break;
                        case "Actor":
                            mapInfo.Type = EResourceType.Actor;
                            break;
                        default:
                            print("��������Ǹ�ɶ��" + resource.gameObject.name);
                            break;
                    }
                    mapInfos.Add(mapInfo);
                }
                levelNum++;
            }
            else
            {
                break;
            }
        }
        // ת json
        string mapJson = JsonUtility.ToJson(new Serialization<MapResourceInfo>(mapInfos));
        // �� txt
        using (FileStream fs = new FileStream(_mapInfoPath, FileMode.OpenOrCreate))
        {
            // ���֮ǰ���� ����ջ���� JSON ��ʽ���쳣 BUG
            fs.Seek(0, SeekOrigin.Begin);
            fs.SetLength(0);
            // ����ת utf-8
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(mapJson);
            fs.Write(bytes);
        }
    }

    [UnityEditor.MenuItem("�ҵĹ���/�������б�Ӧ����Ԥ����")]
    public static void UsePropertyToPrefab()
    {
        // �ж��ļ��Ƿ����
        if (!File.Exists(_propertyListPath))
        {
            print("�����б��ļ������ڣ��޷�Ӧ����Ԥ����");
            return;
        }
        // ��Ȩ
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        // ���� excel ��ֹѭ�������˷��ڴ�
        using (ExcelPackage propertyListExcel = new ExcelPackage(new FileInfo(_propertyListPath)))
        {
            // ��ȡ��Դ��Ϣ
            ExcelWorksheet environmentSheet = propertyListExcel.Workbook.Worksheets[0];
            ExcelWorksheet itemSheet = propertyListExcel.Workbook.Worksheets[1];
            ExcelWorksheet actorSheet = propertyListExcel.Workbook.Worksheets[2];
            ExcelWorksheet enemySheet = propertyListExcel.Workbook.Worksheets[3];
            // Ӧ����Ԥ����
            for (int i = 2; i <= environmentSheet.Dimension.Rows; i++)
            {
                if (null == environmentSheet.Cells[i, 4].Value) continue;
                GameObject tempObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/" + environmentSheet.Cells[i, 4].Value.ToString() + ".prefab");
                tempObj.GetComponent<EnvironmentController>().ID = int.Parse(environmentSheet.Cells[i, 1].Value.ToString());
                tempObj.GetComponent<EnvironmentController>().Name = environmentSheet.Cells[i, 2].Value.ToString();
                tempObj.GetComponent<EnvironmentController>().Info = environmentSheet.Cells[i, 3].Value?.ToString();
                EditorUtility.SetDirty(tempObj);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            for (int i = 2; i <= itemSheet.Dimension.Rows; i++)
            {
                if (null == itemSheet.Cells[i, 4].Value) continue;
                GameObject tempObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/" + itemSheet.Cells[i, 4].Value.ToString() + ".prefab");
                tempObj.GetComponent<ItemController>().ID = int.Parse(itemSheet.Cells[i, 1].Value.ToString());
                tempObj.GetComponent<ItemController>().Name = itemSheet.Cells[i, 2].Value.ToString();
                tempObj.GetComponent<ItemController>().Info = itemSheet.Cells[i, 3].Value?.ToString();
                tempObj.GetComponent<ItemController>().IconPath = itemSheet.Cells[i, 5].Value.ToString();
                tempObj.GetComponent<ItemController>().UseCount = int.Parse(itemSheet.Cells[i, 6].Value.ToString());
                EditorUtility.SetDirty(tempObj);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            for (int i = 2; i <= actorSheet.Dimension.Rows; i++)
            {
                if (null == actorSheet.Cells[i, 4].Value) continue;
                GameObject tempObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/" + actorSheet.Cells[i, 4].Value.ToString() + ".prefab");
                tempObj.GetComponent<ActorController>().ID = int.Parse(actorSheet.Cells[i, 1].Value.ToString());
                tempObj.GetComponent<ActorController>().Name = actorSheet.Cells[i, 2].Value.ToString();
                tempObj.GetComponent<ActorController>().Info = actorSheet.Cells[i, 3].Value?.ToString();
                tempObj.GetComponent<ActorController>().IconPath = actorSheet.Cells[i, 5].Value.ToString();
                EditorUtility.SetDirty(tempObj);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            for (int i = 2; i <= enemySheet.Dimension.Rows; i++)
            {
                if (null == enemySheet.Cells[i, 4].Value) continue;
                GameObject tempObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/" + enemySheet.Cells[i, 4].Value.ToString() + ".prefab");
                tempObj.GetComponent<EnemyController>().ID = int.Parse(enemySheet.Cells[i, 1].Value.ToString());
                tempObj.GetComponent<EnemyController>().Name = enemySheet.Cells[i, 2].Value.ToString();
                tempObj.GetComponent<EnemyController>().Info = enemySheet.Cells[i, 3].Value?.ToString();
                tempObj.GetComponent<EnemyController>().IconPath = enemySheet.Cells[i, 5].Value?.ToString();
                tempObj.GetComponent<EnemyController>().Health = int.Parse(enemySheet.Cells[i, 6].Value.ToString());
                tempObj.GetComponent<EnemyController>().MaxHealth = int.Parse(enemySheet.Cells[i, 6].Value.ToString());
                tempObj.GetComponent<EnemyController>().Attack = int.Parse(enemySheet.Cells[i, 7].Value.ToString());
                tempObj.GetComponent<EnemyController>().Defence = int.Parse(enemySheet.Cells[i, 8].Value.ToString());
                tempObj.GetComponent<EnemyController>().Gold = int.Parse(enemySheet.Cells[i, 9].Value.ToString());
                EditorUtility.SetDirty(tempObj);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }

    [UnityEditor.MenuItem("�ҵĹ���/ת�������б�Ϊ Json �ı��ļ�")]
    public static void TransferPropertyInfoToJsonTxt()
    {
        List<ResourceInfo> resourceInfos = new List<ResourceInfo>();
        // �ж��ļ��Ƿ����
        if (!File.Exists(_propertyListPath))
        {
            print("�����б��ļ������ڣ��޷�ת��Ϊ Json �ı��ļ�");
            return;
        }
        // ��Ȩ
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        // ���� excel ��ֹѭ�������˷��ڴ�
        using (ExcelPackage propertyListExcel = new ExcelPackage(new FileInfo(_propertyListPath)))
        {
            // ��ȡ��Դ��Ϣ
            for (int i = 0; i < 4; i++)
            {
                ExcelWorksheet sheet = propertyListExcel.Workbook.Worksheets[i];
                EResourceType type = (EResourceType)i;
                for (int j = 2; j <= sheet.Dimension.Rows; j++)
                {
                    resourceInfos.Add(new ResourceInfo
                    {
                        Type = type,
                        ID = int.Parse(sheet.Cells[j, 1].Value.ToString()),
                        Name = sheet.Cells[j, 2].Value.ToString(),
                        Info = sheet.Cells[j, 3].Value?.ToString(),
                        Path = sheet.Cells[j, 4].Value?.ToString(),
                        IconPath = sheet.Cells[j, 5].Value?.ToString(),
                    });
                }
            }
        }
        // ת json
        string resourceJson = JsonUtility.ToJson(new Serialization<ResourceInfo>(resourceInfos));
        // �� txt
        using (FileStream fs = new FileStream(_propertyInfoPath, FileMode.OpenOrCreate))
        {
            // ���֮ǰ���� ����ջ���� JSON ��ʽ���쳣 BUG
            fs.Seek(0, SeekOrigin.Begin);
            fs.SetLength(0);
            // ����ת utf-8
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(resourceJson);
            fs.Write(bytes);
        }
    }
}
