using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [HideInInspector]
    public Canvas MainCanvas;

    private Text _levelValueText;

    private Text _playerHealthValueText;
    private Text _playerAttackValueText;
    private Text _playerDefenceValueText;
    private Text _playerGoldValueText;

    private Text _weaponValueText;
    private Text _armorValueText;
    private Image _weaponImage;
    private Image _armorImage;

    private Text _enemyNameValueText;
    private Text _enemyHealthValueText;
    private Text _enemyAttackValueText;
    private Text _enemyDefenceValueText;
    private Image _enemyImage;

    private GridLayoutGroup _backpackInfoPanel;
    private Dictionary<int, GameObject> _backpackDictionary = new Dictionary<int, GameObject>();

    private GameObject _gameOverPanel;
    private Button _gameOverBackHomeButton;

    private GameObject _dialogPanel;
    private Text _dialogNameValueText;
    private Text _dialogInfoValueText;

    private GameObject _bookPanel;
    private GameObject _bookContent;

    private GameObject _shopPanel;
    private Text _shopNameValueText;
    private Text _shopInfoValueText;
    private Button _shopYesButton;
    private Button _shopNoButton;

    private GameObject _infoPanel;

    private GameObject _notepadPanel;
    private Text _notepadValueText;

    private GameObject _interactionDialogPanel;
    private Text _interactionDialogNameValueText;
    private Text _interactionDialogInfoValueText;
    private Button _interactionDialogYesButton;
    private Button _interactionDialogNoButton;
    private Text _interactionDialogYesButtonValueText;
    private Text _interactionDialogNoButtonValueText;

    public Transform BookContent { get => _bookContent.transform; }

    private new void Awake()
    {
        base.Awake();

        // ��ȡ UI
        MainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();

        Transform leftBackGroundPanel = MainCanvas.transform.Find("LeftBackGroundPanel");

        _levelValueText = leftBackGroundPanel.Find("LevelInfoPanel").Find("LevelValueText").GetComponent<Text>();

        _playerHealthValueText = leftBackGroundPanel.Find("PlayerInfoPanel").Find("PlayerHealthValueText").GetComponent<Text>();
        _playerAttackValueText = leftBackGroundPanel.Find("PlayerInfoPanel").Find("PlayerAttackValueText").GetComponent<Text>();
        _playerDefenceValueText = leftBackGroundPanel.Find("PlayerInfoPanel").Find("PlayerDefenceValueText").GetComponent<Text>();
        _playerGoldValueText = leftBackGroundPanel.Find("PlayerInfoPanel").Find("PlayerGoldValueText").GetComponent<Text>();

        _weaponValueText = leftBackGroundPanel.Find("EquipmentInfoPanel").Find("WeaponValueText").GetComponent<Text>();
        _armorValueText = leftBackGroundPanel.Find("EquipmentInfoPanel").Find("ArmorValueText").GetComponent<Text>();
        _weaponImage = leftBackGroundPanel.Find("EquipmentInfoPanel").Find("WeaponImage").GetComponent<Image>();
        _armorImage = leftBackGroundPanel.Find("EquipmentInfoPanel").Find("ArmorImage").GetComponent<Image>();

        Transform rightBackGroundPanel = MainCanvas.transform.Find("RightBackGroundPanel");

        _enemyImage = rightBackGroundPanel.Find("EnemyInfoPanel").Find("EnemyImage").GetComponent<Image>();
        _enemyImage.enabled = false;

        _enemyNameValueText = rightBackGroundPanel.Find("EnemyInfoPanel").Find("EnemyNameValueText").GetComponent<Text>();

        _enemyHealthValueText = rightBackGroundPanel.Find("EnemyInfoPanel").Find("EnemyHealthValueText").GetComponent<Text>();
        _enemyAttackValueText = rightBackGroundPanel.Find("EnemyInfoPanel").Find("EnemyAttackValueText").GetComponent<Text>();
        _enemyDefenceValueText = rightBackGroundPanel.Find("EnemyInfoPanel").Find("EnemyDefenceValueText").GetComponent<Text>();

        _backpackInfoPanel = rightBackGroundPanel.Find("ItemInfoPanel").Find("BackpackInfoPanel").GetComponent<GridLayoutGroup>();

        _gameOverPanel = MainCanvas.transform.Find("GameOverPanel").gameObject;
        _gameOverBackHomeButton = _gameOverPanel.transform.Find("BackHomeButton").GetComponent<Button>();
        _gameOverPanel.SetActive(false);

        _dialogPanel = MainCanvas.transform.Find("DialogPanel").gameObject;
        _dialogNameValueText = _dialogPanel.transform.Find("NameVelueText").GetComponent<Text>();
        _dialogInfoValueText = _dialogPanel.transform.Find("InfoValueText").GetComponent<Text>();
        _dialogPanel.SetActive(false);

        _bookPanel = MainCanvas.transform.Find("BookPanel").gameObject;
        _bookContent = _bookPanel.transform.Find("Scroll View").Find("Viewport").Find("BookContent").gameObject;
        _bookPanel.SetActive(false);

        _shopPanel = MainCanvas.transform.Find("ShopPanel").gameObject;
        _shopNameValueText = _shopPanel.transform.Find("ShopNameValueText").GetComponent<Text>();
        _shopInfoValueText = _shopPanel.transform.Find("ShopValueText").GetComponent<Text>();
        _shopYesButton = _shopPanel.transform.Find("YesButton").GetComponent<Button>();
        _shopNoButton = _shopPanel.transform.Find("NoButton").GetComponent<Button>();
        _shopPanel.SetActive(false);

        _infoPanel = MainCanvas.transform.Find("InfoPanel").gameObject;

        _notepadPanel = MainCanvas.transform.Find("NotepadPanel").gameObject;
        _notepadValueText = _notepadPanel.transform.Find("Scroll View").Find("Viewport").Find("Content").Find("InfoText").GetComponent<Text>();
        _notepadValueText.rectTransform.position = Vector3.zero;
        _notepadPanel.SetActive(false);

        _interactionDialogPanel = MainCanvas.transform.Find("InteractionDialogPanel").gameObject;
        _interactionDialogNameValueText = _interactionDialogPanel.transform.Find("NameValueText").GetComponent<Text>();
        _interactionDialogInfoValueText = _interactionDialogPanel.transform.Find("InfoValueText").GetComponent<Text>();
        _interactionDialogYesButton = _interactionDialogPanel.transform.Find("YesButton").GetComponent<Button>();
        _interactionDialogNoButton = _interactionDialogPanel.transform.Find("NoButton").GetComponent<Button>();
        _interactionDialogYesButtonValueText = _interactionDialogYesButton.GetComponentInChildren<Text>();
        _interactionDialogNoButtonValueText = _interactionDialogNoButton.GetComponentInChildren<Text>();
        _interactionDialogPanel.SetActive(false);
    }

    /// <summary>
    /// ���ݰ�
    /// </summary>
    public void BindEvent()
    {
        GameManager.Instance.EventManager.OnHealthChanged += (value) =>
        {
            _playerHealthValueText.text = value.ToString();
            if (value <= 0)
            {
                _gameOverPanel.SetActive(true);
                // ������ҿ�����
                GameManager.Instance.PlayerManager.Enable = false;
                // ������ҿ�����
                GameManager.Instance.PlayerManager.LockEnable = true;
                // �󶨰�ť�¼�
                _gameOverBackHomeButton.onClick.RemoveAllListeners();
                _gameOverBackHomeButton.onClick.AddListener(() => { GameManager.Instance.BackHomeEvent(); });
            }
        };
        GameManager.Instance.EventManager.OnAttackChanged += (value) => { _playerAttackValueText.text = value.ToString(); };
        GameManager.Instance.EventManager.OnDefenceChanged += (value) => { _playerDefenceValueText.text = value.ToString(); };
        GameManager.Instance.EventManager.OnGoldChanged += (value) => { _playerGoldValueText.text = value.ToString(); };

        GameManager.Instance.EventManager.OnEnemyCombated += (enemy) =>
        {
            if (enemy != null)
            {
                _enemyNameValueText.text = enemy.Name;
                _enemyHealthValueText.text = enemy.Health.ToString();
                _enemyAttackValueText.text = enemy.Attack.ToString();
                _enemyDefenceValueText.text = enemy.Defence.ToString();
                _enemyImage.enabled = true;
                _enemyImage.sprite = Resources.Load<Sprite>(enemy.IconPath);
            }
        };

        GameManager.Instance.EventManager.OnLevelChanged += (oldValue, newValue) => { _levelValueText.text = newValue.ToString(); };

        GameManager.Instance.EventManager.OnItemChanged += ItemChangeEvent;

        GameManager.Instance.EventManager.OnShopShow += ShopShowEvent;

        GameManager.Instance.EventManager.OnWeaponChanged += (value) =>
        {
            ResourceInfo resourceInfo = GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, value);
            if (null == resourceInfo)
            {
                _weaponValueText.text = "";
                _weaponImage.enabled = false;
            }
            else
            {
                _weaponValueText.text = resourceInfo.Name;
                _weaponImage.enabled = true;
                _weaponImage.sprite = Instantiate(Resources.Load(resourceInfo.IconPath)) as Sprite;
            }
        };

        GameManager.Instance.EventManager.OnArmorChanged += (value) =>
        {
            ResourceInfo resourceInfo = GameManager.Instance.ResourceManager.GetResourceInfo(EResourceType.Item, value);
            if (null == resourceInfo)
            {
                _armorValueText.text = "";
                _armorImage.enabled = false;
            }
            else
            {
                _armorValueText.text = resourceInfo.Name;
                _armorImage.enabled = true;
                _armorImage.sprite = Instantiate(Resources.Load(resourceInfo.IconPath)) as Sprite;
            }
        };

        GameManager.Instance.EventManager.OnNotepadChanged += (value) =>
        {
            _notepadValueText.text = value;
        };
    }


    /// <summary>
    /// �̵���¼�
    /// </summary>
    /// <param name="name">�̵�����</param>
    /// <param name="gold">ÿ���������</param>
    private void ShopShowEvent(string name, int gold, Action callback)
    {
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Shop");
        // �������������
        GameManager.Instance.PlayerManager.Enable = false;
        _shopPanel.SetActive(true);
        _shopNameValueText.text = name;
        _shopInfoValueText.text = gold.ToString();
        _shopYesButton.onClick.RemoveAllListeners();
        _shopYesButton.onClick.AddListener(() =>
        {
            callback?.Invoke();
            // �������������
            GameManager.Instance.PlayerManager.Enable = true;
        });
        _shopNoButton.onClick.RemoveAllListeners();
        _shopNoButton.onClick.AddListener(() =>
        {
            // �������������
            GameManager.Instance.PlayerManager.Enable = true;
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
        });
    }

    /// <summary>
    /// ��ʼ������ UI
    /// </summary>
    public void InitBackpackUI()
    {
        // ��ձ��� UI
        LayoutElement[] layoutElements = _backpackInfoPanel.GetComponentsInChildren<LayoutElement>();
        foreach (var layoutElement in layoutElements)
        {
            Destroy(layoutElement.gameObject);
        }
        _backpackDictionary.Clear();
        // �����Ʒ
        Dictionary<int, ItemInfo> backpackDic = GameManager.Instance.BackpackManager.BackpackDictionary;
        foreach (var key in backpackDic.Keys)
        {
            // ������Ʒ
            AddItemToBackpack(backpackDic[key]);
        }
    }

    /// <summary>
    /// �򿪹����ֲ�
    /// </summary>
    /// <param name="callback">�ص�����</param>
    public void ShowBook(Action callback)
    {
        // �� UI ���
        _bookPanel.SetActive(true);
        // ������
        for (int i = 0; i < _bookContent.transform.childCount; i++)
        {
            Destroy(_bookContent.transform.GetChild(i).gameObject);
        }
        callback?.Invoke();
    }

    /// <summary>
    /// �򿪶Ի���
    /// </summary>
    /// <param name="name">˵������</param>
    /// <param name="info">˵������</param>
    /// <param name="callback">�ص�����</param>
    public void ShowDialog(string name, List<string> info, Action callback)
    {
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Music, "Dialogue");
        // �������������
        GameManager.Instance.PlayerManager.Enable = false;
        // ���öԻ���
        _dialogInfoValueText.transform.parent.gameObject.SetActive(true);
        // ��ʾ˵�������ֺ�����
        _dialogNameValueText.text = name + ":";
        StartCoroutine(LoadDialog(info, callback));
    }

    /// <summary>
    /// �򿪽����Ի���
    /// </summary>
    /// <param name="name">˵����</param>
    /// <param name="info">˵������</param>
    /// <param name="yesBtnTxt">ȷ�ϰ�ť�ı�</param>
    /// <param name="noBtnTxt">ȡ����ť�ı�</param>
    /// <param name="yesBtnCallback">���ȷ�ϰ�ť�Ļص�����</param>
    public void ShowInteractionDialog(string name, string info, string yesBtnTxt, string noBtnTxt, Action yesBtnCallback)
    {
        // ��Ƶ����
        GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "Shop");
        // �������������
        GameManager.Instance.PlayerManager.Enable = false;
        // �󶨲���
        _interactionDialogNameValueText.text = name;
        _interactionDialogInfoValueText.text = info;
        _interactionDialogYesButtonValueText.text = yesBtnTxt;
        _interactionDialogNoButtonValueText.text = noBtnTxt;
        _interactionDialogYesButton.onClick.RemoveAllListeners();
        _interactionDialogYesButton.onClick.AddListener(() =>
        {
            yesBtnCallback?.Invoke();
            _interactionDialogPanel.SetActive(false);
            // �������������
            GameManager.Instance.PlayerManager.Enable = true;
        });
        _interactionDialogNoButton.onClick.RemoveAllListeners();
        _interactionDialogNoButton.onClick.AddListener(() =>
        {
            _interactionDialogPanel.SetActive(false);
            // �������������
            GameManager.Instance.PlayerManager.Enable = true;
            // ��Ƶ����
            GameManager.Instance.SoundManager.PlaySound(ESoundType.Effect, "No");
        });
        _interactionDialogPanel.SetActive(true);
    }

    /// <summary>
    /// ���ضԻ�
    /// </summary>
    /// <param name="info">˵������</param>
    /// <param name="callback">�ص����� ����ʱ����</param>
    IEnumerator LoadDialog(List<string> info, Action callback)
    {
        int index = 0;
        while (index < info.Count)
        {
            _dialogInfoValueText.text = info[index];
            if (Input.GetKeyDown(KeyCode.Return)) index++;
            yield return null;
        }
        // ���öԻ���
        _dialogInfoValueText.transform.parent.gameObject.SetActive(false);
        // ��βִ�лص�����
        callback?.Invoke();
        yield break;
    }

    /// <summary>
    /// ��ʾ��Ϣ
    /// </summary>
    /// <param name="info">��Ϣ����</param>
    public void ShowInfo(string info)
    {
        GameObject obj = Instantiate(Resources.Load("UI/InfoImage") as GameObject, _infoPanel.transform);
        obj.GetComponent<InfoImageController>().SetText(info);
    }

    /// <summary>
    /// ��ʾ�ʼǱ�
    /// </summary>
    public void ShowNotepad()
    {
        _notepadPanel.SetActive(true);
    }

    /// <summary>
    /// ��Ʒ����¼�
    /// </summary>
    /// <param name="id">��Ʒ ID</param>
    /// <param name="itemInfo">��Ʒ��Ϣ</param>
    private void ItemChangeEvent(int id, ItemInfo itemInfo)
    {
        // �����Ʒ�������޸�����
        if (_backpackDictionary.ContainsKey(id))
        {
            // �ж��Ƿ�����
            if (itemInfo.UseCount == 0)
            {
                Destroy(_backpackDictionary[id].gameObject);
                _backpackDictionary.Remove(id);
            }
            // �������
            else _backpackDictionary[id].GetComponentInChildren<Text>().text = itemInfo.UseCount < 0 ? "" : itemInfo.UseCount.ToString();
        }
        // �����Ʒ�������򴴽�
        else AddItemToBackpack(itemInfo);
    }

    /// <summary>
    /// �����Ʒ������ UI
    /// </summary>
    /// <param name="itemInfo">��Ʒ��Ϣ</param>
    private void AddItemToBackpack(ItemInfo itemInfo)
    {
        GameObject itemUI = Instantiate(Resources.Load("UI/ItemImage"), _backpackInfoPanel.transform) as GameObject;
        itemUI.GetComponentInChildren<Text>().text = itemInfo.UseCount < 0 ? "" : itemInfo.UseCount.ToString();
        itemUI.GetComponent<Image>().sprite = Instantiate(Resources.Load(itemInfo.IconPath)) as Sprite;
        // ��������¼�
        switch (itemInfo.ID)
        {
            case 4:
                itemUI.AddComponent<EventItemKey4>();
                break;
            case 5:
                itemUI.AddComponent<EventItemBottle1>();
                break;
            case 6:
                itemUI.AddComponent<EventItemBottle2>();
                break;
            case 9:
                itemUI.AddComponent<EventItemArtifact3>();
                break;
            case 10:
                itemUI.AddComponent<EventItemBook>();
                break;
            case 11:
                itemUI.AddComponent<EventItemNotepad>();
                break;
            case 13:
                itemUI.AddComponent<EventItemOther1>();
                break;
            case 14:
                itemUI.AddComponent<EventItemOther2>();
                break;
            case 15:
                itemUI.AddComponent<EventItemArtifact4>();
                break;
            case 16:
                itemUI.AddComponent<EventItemOther6>();
                break;
            case 17:
                itemUI.AddComponent<EventItemBottle3>();
                break;
            case 18:
                itemUI.AddComponent<EventItemArtifact1>();
                break;
            case 19:
                itemUI.AddComponent<EventItemArtifact2>();
                break;
            case 21:
                itemUI.AddComponent<EventItemOther5>();
                break;
            case 22:
                itemUI.AddComponent<EventItemOther4>();
                break;
            case 23:
                itemUI.AddComponent<EventItemOther3>();
                break;
            case 24:
                itemUI.AddComponent<EventItemEquipmentWeapon1>();
                break;
            case 25:
                itemUI.AddComponent<EventItemEquipmentArmor1>();
                break;
            case 26:
                itemUI.AddComponent<EventItemEquipmentWeapon2>();
                break;
            case 27:
                itemUI.AddComponent<EventItemEquipmentArmor2>();
                break;
            case 28:
                itemUI.AddComponent<EventItemEquipmentWeapon3>();
                break;
            case 29:
                itemUI.AddComponent<EventItemEquipmentArmor3>();
                break;
            case 30:
                itemUI.AddComponent<EventItemEquipmentWeapon4>();
                break;
            case 31:
                itemUI.AddComponent<EventItemEquipmentArmor4>();
                break;
            case 32:
                itemUI.AddComponent<EventItemEquipmentWeapon5>();
                break;
            case 33:
                itemUI.AddComponent<EventItemEquipmentArmor5>();
                break;
        }
        // ���� UI ����
        _backpackDictionary.Add(itemInfo.ID, itemUI);
    }
}
