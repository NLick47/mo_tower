using OfficeOpenXml;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��Դ����
/// </summary>
public enum EResourceType
{
    Environment,
    Item,
    Actor,
    Enemy,
}

/// <summary>
/// ��ͼ��Դ��Ϣ
/// </summary>
[System.Serializable]
public class MapResourceInfo
{
    public int Level;
    public EResourceType Type;
    public int ID;
    public Vector2 Point;
}

/// <summary>
/// ��Դ��Ϣ
/// </summary>
[System.Serializable]
public class ResourceInfo
{
    public EResourceType Type;
    public int ID;
    public string Name;
    public string Info;
    public string Path;
    public string IconPath;
}

/// <summary>
/// ��Ϸ��Ϣ ���ڴ浵
/// </summary>
[System.Serializable]
public class GameInfo
{
    public string MapArchive;
    public string PlayerInfo;
    public int LevelInfo;
    public int MaxLevelInfo;
    public string BackpackInfo;
    public string PlotInfo;
}

public class ResourceManager : Singleton<ResourceManager>
{
    private string _mapInfoPath = Application.dataPath + "/Settings/��ͼ��Ϣ.txt";
    private string _propertyInfoPath = Application.dataPath + "/Settings/�����б�.txt";

    private List<MapResourceInfo> _mapResourceInfoList = new List<MapResourceInfo>();
    private Dictionary<EResourceType, Dictionary<int, ResourceInfo>> _resourceInfoDic = new Dictionary<EResourceType, Dictionary<int, ResourceInfo>>();

    public ResourceManager()
    {
        LoadPropertyFile();
    }

    /// <summary>
    /// ���¼�
    /// </summary>
    public void BindEvent()
    {
        GameManager.Instance.EventManager.OnSaveGameInput += SaveGameInfoEvent;
        GameManager.Instance.EventManager.OnLevelChanged += LoadLevelEvent;
    }

    /// <summary>
    /// �½���Ϸ��Ϣ
    /// </summary>
    public void NewGameInfo()
    {
        // �� txt ���õ�ͼ��Ϣ
        using (FileStream fs = new FileStream(_mapInfoPath, FileMode.Open))
        {
            // ���� txt �ֽڳ��ȵ� byte ����
            byte[] bytes = new byte[fs.Length];
            // ��ȡ txt �ֽڵ� byte ����
            fs.Read(bytes, 0, bytes.Length);
            // �� utf-8 ����
            string mapStr = System.Text.Encoding.UTF8.GetString(bytes);
            // json ת���� list
            _mapResourceInfoList = JsonUtility.FromJson<Serialization<MapResourceInfo>>(mapStr).ToList();
        }
        // ��ʼ��������Ϣ
        GameManager.Instance.PlotManager.Init();
        // ���ò�����Ϣ
        GameManager.Instance.LevelManager.Level = 1;
        // ���������Ϣ
        GameManager.Instance.PlayerManager.PlayerInfo = new PlayerInfo
        {
            Health = 1000,
            Attack = 100,
            Defence = 100,
            Gold = 0,
            WeaponID = 32,
            ArmorID = 33,
            NotepadInfo = "",
        };
        // ɾ����ʶ��
        PlayerPrefs.DeleteKey("NewGame");
    }

    /// <summary>
    /// ������Դ
    /// </summary>
    /// <param name="type">��Դ����</param>
    /// <param name="id">��Դ id</param>
    /// <returns>��Դ����</returns>
    public GameObject LoadResource(EResourceType type, int id)
    {
        // ��ȡ��Դ����
        ResourceInfo tempInfo = _resourceInfoDic[type][id];
        if (null == tempInfo) return null;
        // ������Դ������
        return Object.Instantiate(Resources.Load<GameObject>(tempInfo.Path), Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// ��ȡ��Դ��Ϣ
    /// </summary>
    /// <param name="type">��Դ����</param>
    /// <param name="id">��Դ id</param>
    /// <returns>��Դ��Ϣ</returns>
    public ResourceInfo GetResourceInfo(EResourceType type, int id)
    {
        if (!_resourceInfoDic.ContainsKey(type)) return null;
        if (!_resourceInfoDic[type].ContainsKey(id)) return null;
        return _resourceInfoDic[type][id];
    }

    /// <summary>
    /// ��ȡ����Ϸ״̬
    /// </summary>
    /// <returns></returns>
    public bool GetNewGameStatus()
    {
        return PlayerPrefs.HasKey("NewGame");
    }

    /// <summary>
    /// ��ȡ��Ϸ�浵״̬
    /// </summary>
    /// <returns>�Ƿ��д浵</returns>
    public bool GetGameArchiveStatus()
    {
        return PlayerPrefs.HasKey("GameInfo");
    }

    /// <summary>
    /// ������Ϸ�浵
    /// </summary>
    /// <returns>�Ƿ��ܹ�����</returns>
    public void LoadGameArchive()
    {
        // �жϴ浵�Ƿ����
        if (!PlayerPrefs.HasKey("GameInfo")) return;
        // json ����ת GameInfo
        GameInfo gameInfo = JsonUtility.FromJson<GameInfo>(PlayerPrefs.GetString("GameInfo"));
        // ���ش浵
        _mapResourceInfoList = JsonUtility.FromJson<Serialization<MapResourceInfo>>(gameInfo.MapArchive).ToList();
        // ���ر�����Ϣ
        GameManager.Instance.BackpackManager.BackpackDictionary = JsonUtility.FromJson<Serialization<int, ItemInfo>>(new(gameInfo.BackpackInfo)).ToDictionary();
        // ���������Ϣ
        GameManager.Instance.PlayerManager.PlayerInfo = JsonUtility.FromJson<PlayerInfo>(gameInfo.PlayerInfo);
        // ���ؾ�����Ϣ
        GameManager.Instance.PlotManager.PlotDictionary = JsonUtility.FromJson<Serialization<int, int>>(new(gameInfo.PlotInfo)).ToDictionary();
        // ���ز�����Ϣ
        GameManager.Instance.LevelManager.Level = gameInfo.LevelInfo;
        GameManager.Instance.LevelManager.MaxLevel = gameInfo.MaxLevelInfo;
    }

    /// <summary>
    /// �ƶ����λ��
    /// </summary>
    /// <param name="level">�ؿ�</param>
    /// <param name="point">λ��</param>
    public void MovePlayerPointForLevel(int level, Vector2 point)
    {
        _mapResourceInfoList.ForEach(mri =>
        {
            if (mri.Level == level && mri.Type == EResourceType.Actor && mri.ID == 1)
            {
                mri.Point = point;
                return;
            }
        });
    }

    /// <summary>
    /// ������Դ���ؿ�
    /// </summary>
    /// <param name="level"></param>
    /// <param name="type"></param>
    /// <param name="id"></param>
    /// <param name="point"></param>
    public void MakeResourceForLevel(int level, EResourceType type, int id, Vector2 point)
    {
        _mapResourceInfoList.Add(new MapResourceInfo()
        {
            Level = level,
            Type = type,
            ID = id,
            Point = point,
        });
    }

    /// <summary>
    /// ���������ļ�
    /// </summary>
    private void LoadPropertyFile()
    {
        // �ж������ļ��Ƿ����
        if (!File.Exists(_propertyInfoPath)) return;
        // �� txt
        using (FileStream fs = new FileStream(_propertyInfoPath, FileMode.Open))
        {
            // ���� txt �ֽڳ��ȵ� byte ����
            byte[] bytes = new byte[fs.Length];
            // ��ȡ txt �ֽڵ� byte ����
            fs.Read(bytes, 0, bytes.Length);
            // �� utf-8 ����
            string infoStr = System.Text.Encoding.UTF8.GetString(bytes);
            // json ת���� list
            List<ResourceInfo> tempList = JsonUtility.FromJson<Serialization<ResourceInfo>>(infoStr).ToList();
            // �� list �����ֵ�
            tempList.ForEach(ri =>
            {
                if (!_resourceInfoDic.ContainsKey(ri.Type)) _resourceInfoDic.Add(ri.Type, new Dictionary<int, ResourceInfo>());
                if (!_resourceInfoDic[ri.Type].ContainsKey(ri.ID)) _resourceInfoDic[ri.Type].Add(ri.ID, ri);
            });
        }
    }


    /// <summary>
    /// ���عؿ��¼�
    /// </summary>
    /// <param name="oldIndex">�ɹؿ����</param>
    /// <param name="newIndex">�¹ؿ����</param>
    private void LoadLevelEvent(int oldIndex, int newIndex)
    {
        // �жϵ�ͼ��Ϣ�Ƿ����
        if (_mapResourceInfoList.Count == 0) return;
        // ��ȡ��ǰ�ؿ�������Դ
        for (int i = _mapResourceInfoList.Count - 1; i >= 0; i--)
        {
            if (_mapResourceInfoList[i].Level == oldIndex) _mapResourceInfoList.RemoveAt(i);
        }
        // ���µ���Դ��Ϣ��
        GameManager.Instance.PoolManager.UseList.ForEach(u =>
        {
            EResourceType type = EResourceType.Actor;
            switch (u.tag)
            {
                case "Item":
                    type = EResourceType.Item;
                    break;
                case "Enemy":
                    type = EResourceType.Enemy;
                    break;
                case "Environment":
                    type = EResourceType.Environment;
                    break;
                default:
                    break;
            }
            _mapResourceInfoList.Add(new MapResourceInfo
            {
                Level = oldIndex,
                Type = type,
                ID = u.GetComponent<ResourceController>().ID,
                Point = u.transform.position,
            });
        });
        // ������Դ
        GameManager.Instance.PoolManager.RecycleResource();
        // ������Դ
        foreach (var info in _mapResourceInfoList)
        {
            // �жϹؿ�һ��
            if (info.Level == newIndex)
            {
                GameObject tempObj = GameManager.Instance.PoolManager.GetResourceInFreePool(info.Type, info.ID);
                tempObj.transform.position = info.Point;
            }
        }
        // �������ִ���¼�
        GameManager.Instance.EventManager.OnResourceLoaded?.Invoke();
    }

    /// <summary>
    /// ������Ϸ��Ϣ�¼�
    /// </summary>
    private void SaveGameInfoEvent()
    {
        // ɾ����ǰ�ؿ���Դ
        for (int i = _mapResourceInfoList.Count - 1; i >= 0; i--)
        {
            if (_mapResourceInfoList[i].Level == GameManager.Instance.LevelManager.Level) _mapResourceInfoList.RemoveAt(i);
        }
        // �����ö������Դ������Դ��Ϣ��
        GameManager.Instance.PoolManager.UseList.ForEach(u =>
        {
            EResourceType type = EResourceType.Actor;
            switch (u.tag)
            {
                case "Item":
                    type = EResourceType.Item;
                    break;
                case "Enemy":
                    type = EResourceType.Enemy;
                    break;
                case "Environment":
                    type = EResourceType.Environment;
                    break;
            }
            _mapResourceInfoList.Add(new MapResourceInfo
            {
                Level = GameManager.Instance.LevelManager.Level,
                Type = type,
                ID = u.GetComponent<ResourceController>().ID,
                Point = u.transform.position,
            });
        });
        // ��ȡ��Ϸ��Ϣ
        GameInfo gameInfo = new GameInfo
        {
            MapArchive = JsonUtility.ToJson(new Serialization<MapResourceInfo>(_mapResourceInfoList)),
            PlayerInfo = JsonUtility.ToJson(GameManager.Instance.PlayerManager.PlayerInfo),
            LevelInfo = GameManager.Instance.LevelManager.Level,
            MaxLevelInfo = GameManager.Instance.LevelManager.MaxLevel,
            BackpackInfo = JsonUtility.ToJson(new Serialization<int, ItemInfo>(GameManager.Instance.BackpackManager.BackpackDictionary)),
            PlotInfo = JsonUtility.ToJson(new Serialization<int, int>(GameManager.Instance.PlotManager.PlotDictionary)),
        };
        // ������Դ��Ϣ
        PlayerPrefs.SetString("GameInfo", JsonUtility.ToJson(gameInfo));
        // UI ��ʾ
        GameManager.Instance.UIManager.ShowInfo("��Ϸ����ɹ���");
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Save");
    }
}
