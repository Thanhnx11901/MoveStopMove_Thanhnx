using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShop : UICanvas
{
    public Text txtCoin;
    public Text txtMoney;

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject equippedButton;

    [SerializeField] private GameObject selectButton;

    [SerializeField] private GameObject buyButton;
    public GameObject content_Hair;

    public GameObject content_Set;

    public GameObject content_Pant;

    public GameObject content_Shield;

    public GameObject img_Hair;

    public GameObject img_Set;

    public GameObject img_Pant;

    public GameObject img_Shield;

    public BtnHair prefabBtnHair;
    public BtnPant prefabBtnPant;
    public BtnShield prefabBtnShield;
    public BtnSet prefabBtnSet;

    public List<BtnHair> btnHairs = new List<BtnHair>();
    public List<BtnShield> btnShields = new List<BtnShield>();
    public List<BtnPant> BtnPants = new List<BtnPant>();
    public List<BtnSet> BtnSets = new List<BtnSet>();
    public int idSkin;
    public int id;
    private void Start()
    {
        idSkin = 1;
        id = 1;
        Spawn_Hair();
        Spawn_Shield();
        Spawn_Pant();
        Spawn_Set();
        content_Hair.SetActive(true);
        content_Shield.SetActive(false);
        content_Set.SetActive(false);
        content_Pant.SetActive(false);
        img_Hair.SetActive(true);
        img_Shield.SetActive(false);
        img_Set.SetActive(false);
        img_Pant.SetActive(false);
    }
    private void OnEnable()
    {
        idSkin = 1;
        if (PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin && PlayerPrefs.GetInt(Constants.CURRENT_HAIR) != 0)
        {
            id = PlayerPrefs.GetInt(Constants.CURRENT_HAIR);
        }
        else
        {
            id = 1;
        }
        LoadButton();
        content_Hair.SetActive(true);
        content_Shield.SetActive(false);
        content_Set.SetActive(false);
        content_Pant.SetActive(false);
        txtCoin.text = PlayerPrefs.GetInt(Constants.CURRENT_COIN).ToString();
    }
    public void Spawn_Hair()
    {
        foreach (EHair hairStyle in System.Enum.GetValues(typeof(EHair)))
        {
            if (hairStyle == EHair.None) continue;
            BtnHair btnHair = Instantiate(prefabBtnHair, content_Hair.transform);
            btnHair.OnInit(GameData.Ins.hairConfig.GetHair(hairStyle), hairStyle, (int)ESkin.Hair, (int)hairStyle, this);
            btnHairs.Add(btnHair);
        }
    }
    public void ReLoadBtn_Hair()
    {
        for (int i = 0; i < btnHairs.Count; i++)
        {
            btnHairs[i].SetLockAndEquipped();
        }
    }

    public void Spawn_Shield()
    {
        foreach (EShield shieldStyle in System.Enum.GetValues(typeof(EShield)))
        {
            if (shieldStyle == EShield.None) continue;
            BtnShield btnShield = Instantiate(prefabBtnShield, content_Shield.transform);
            btnShield.OnInit(GameData.Ins.shieldConfig.GetShield(shieldStyle), shieldStyle, (int)ESkin.Shiel, (int)shieldStyle, this);
            btnShields.Add(btnShield);
        }
    }
    public void ReLoadBtn_Shield()
    {
        for (int i = 0; i < btnShields.Count; i++)
        {
            btnShields[i].SetLockAndEquipped();
        }
    }
    public void Spawn_Pant()
    {
        foreach (EPant pantStyle in System.Enum.GetValues(typeof(EPant)))
        {
            if (pantStyle == EPant.None) continue;
            BtnPant btnPant = Instantiate(prefabBtnPant, content_Pant.transform);
            btnPant.OnInit(pantStyle, (int)ESkin.Pant, (int)pantStyle, this);
            BtnPants.Add(btnPant);
        }
    }
    public void ReLoadBtn_Pant()
    {
        for (int i = 0; i < BtnPants.Count; i++)
        {
            BtnPants[i].SetLockAndEquipped();
        }
    }
    public void Spawn_Set()
    {
        foreach (ESet setStyle in System.Enum.GetValues(typeof(ESet)))
        {
            if (setStyle == ESet.None) continue;
            BtnSet btnSet = Instantiate(prefabBtnSet, content_Set.transform);
            btnSet.OnInit(setStyle, (int)ESkin.Set, (int)setStyle, this);
            BtnSets.Add(btnSet);
        }
    }
    public void ReLoadBtn_Set()
    {
        for (int i = 0; i < BtnSets.Count; i++)
        {
            BtnSets[i].SetLockAndEquipped();
        }
    }
    public void LoadButton()
    {
        int coin = PlayerPrefs.GetInt(Constants.CURRENT_COIN);
        txtCoin.text = coin.ToString();

        int Money = GameData.Ins.itemDataConfig.GetItemData((ESkin)idSkin, id).Price;
        txtMoney.text = Money.ToString();
        if (coin < Money)
        {
            txtMoney.color = Color.red;
        }
        else
        {
            txtMoney.color = Color.black;
        }


        ESkin skin = (ESkin)idSkin;
        if (skin == ESkin.Hair)
        {
            LoadButtonHair();
            ReLoadBtn_Hair();
        }
        if (skin == ESkin.Shiel)
        {
            LoadButtonShiel();
            ReLoadBtn_Shield();
        }
        if (skin == ESkin.Set)
        {
            LoadButtonSet();
            ReLoadBtn_Set();
        }
        if (skin == ESkin.Pant)
        {
            LoadButtonPant();
            ReLoadBtn_Pant();
        }
        txtCoin.text = PlayerPrefs.GetInt(Constants.CURRENT_COIN).ToString();
    }
    public void SelectButton()
    {
        ESkin skin = (ESkin)idSkin;
        if (skin == ESkin.Hair)
        {
            PlayerPrefs.SetInt(Constants.CURRENT_SKIN, idSkin);
            PlayerPrefs.SetInt(Constants.CURRENT_HAIR, id);
            LevelManager.Ins.playerCtl.IsActiveSkin(true);
            LevelManager.Ins.playerCtl.OnInit();
            LoadButton();

        }
        if (skin == ESkin.Shiel)
        {
            PlayerPrefs.SetInt(Constants.CURRENT_SKIN, idSkin);
            PlayerPrefs.SetInt(Constants.CURRENT_SHIELD, id);
            LevelManager.Ins.playerCtl.OnInit();
            LevelManager.Ins.playerCtl.IsActiveSkin(true);
            LoadButton();

        }
        if (skin == ESkin.Set)
        {
            PlayerPrefs.SetInt(Constants.CURRENT_SKIN, idSkin);
            PlayerPrefs.SetInt(Constants.CURRENT_SET, id);
            LevelManager.Ins.playerCtl.OnInit();
            LevelManager.Ins.playerCtl.IsActiveSkin(true);
            LoadButton();
        }
        if (skin == ESkin.Pant)
        {
            PlayerPrefs.SetInt(Constants.CURRENT_SKIN, idSkin);
            PlayerPrefs.SetInt(Constants.CURRENT_PANT, id);
            LevelManager.Ins.playerCtl.OnInit();
            LevelManager.Ins.playerCtl.IsActiveSkin(true);
            LoadButton();
        }


    }
    public void EquippedButton()
    {
        PlayerPrefs.SetInt(Constants.CURRENT_SKIN, 1);
        PlayerPrefs.SetInt(Constants.CURRENT_HAIR, 0);
        PlayerPrefs.SetInt(Constants.CURRENT_SHIELD, 0);
        PlayerPrefs.SetInt(Constants.CURRENT_PANT, 0);
        PlayerPrefs.SetInt(Constants.CURRENT_SET, 0);

        Debug.Log(PlayerPrefs.GetInt(Constants.CURRENT_SKIN));
        Debug.Log(PlayerPrefs.GetInt(Constants.CURRENT_HAIR));
        Debug.Log(PlayerPrefs.GetInt(Constants.CURRENT_SHIELD));
        Debug.Log(PlayerPrefs.GetInt(Constants.CURRENT_PANT));
        Debug.Log(PlayerPrefs.GetInt(Constants.CURRENT_SET));
        LevelManager.Ins.playerCtl.OnInit();
        LevelManager.Ins.playerCtl.IsActiveSkin(false);
        LoadButton();
    }
    public void BuyButton()
    {
        // tru tien 
        int coin = PlayerPrefs.GetInt(Constants.CURRENT_COIN);
        if (coin < GameData.Ins.itemDataConfig.GetItemData((ESkin)idSkin, id).Price) return;
        coin = coin - GameData.Ins.itemDataConfig.GetItemData((ESkin)idSkin, id).Price;
        PlayerPrefs.SetInt(Constants.CURRENT_COIN, coin);

        ESkin skin = (ESkin)idSkin;
        if (skin == ESkin.Hair)
        {
            string dataHair = PlayerPrefs.GetString(Constants.HAIR) + id;
            PlayerPrefs.SetString(Constants.HAIR, dataHair);
            LoadButton();
        }
        if (skin == ESkin.Shiel)
        {
            string dataShield = PlayerPrefs.GetString(Constants.SHIELD) + id;
            PlayerPrefs.SetString(Constants.SHIELD, dataShield);
            LoadButton();
        }
        if (skin == ESkin.Set)
        {
            string dataSet = PlayerPrefs.GetString(Constants.SET).ToString() + id;
            PlayerPrefs.SetString(Constants.SET, dataSet);
            LoadButton();
        }
        if (skin == ESkin.Pant)
        {
            string dataPant = PlayerPrefs.GetString(Constants.PANT).ToString() + id;
            PlayerPrefs.SetString(Constants.PANT, dataPant);
            LoadButton();
        }
    }
    public void ExitButton()
    {
        GameManager.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MianMenu>();
        LevelManager.Ins.playerCtl.OnInit();
        LevelManager.Ins.playerCtl.IsActiveSkin(true);
        Close(0);
    }

    public void BtnSelectHair()
    {
        scrollRect.content = content_Hair.GetComponent<RectTransform>();
        idSkin = (int)ESkin.Hair;
        if (PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin && PlayerPrefs.GetInt(Constants.CURRENT_HAIR) != 0)
        {
            id = PlayerPrefs.GetInt(Constants.CURRENT_HAIR);
        }
        else
        {
            id = 1;
        }
        content_Hair.SetActive(true);
        content_Shield.SetActive(false);
        content_Set.SetActive(false);
        content_Pant.SetActive(false);

        img_Hair.SetActive(true);
        img_Shield.SetActive(false);
        img_Set.SetActive(false);
        img_Pant.SetActive(false);
        LoadButton();
        LevelManager.Ins.playerCtl.IsActiveSkin(false);
        LevelManager.Ins.playerCtl.ChangeSkinPlayer.DelTestSkin();

    }
    public void BtnSelectShield()
    {
        scrollRect.content = content_Shield.GetComponent<RectTransform>();
        idSkin = (int)ESkin.Shiel;
        if (PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin && PlayerPrefs.GetInt(Constants.CURRENT_SHIELD) != 0)
        {
            id = PlayerPrefs.GetInt(Constants.CURRENT_SHIELD);
        }
        else
        {
            id = 1;
        }
        content_Hair.SetActive(false);
        content_Shield.SetActive(true);
        content_Set.SetActive(false);
        content_Pant.SetActive(false);

        img_Hair.SetActive(false);
        img_Shield.SetActive(true);
        img_Set.SetActive(false);
        img_Pant.SetActive(false);
        LoadButton();
        LevelManager.Ins.playerCtl.IsActiveSkin(false);
        LevelManager.Ins.playerCtl.ChangeSkinPlayer.DelTestSkin();
    }
    public void BtnSelectPant()
    {
        scrollRect.content = content_Pant.GetComponent<RectTransform>();
        idSkin = (int)ESkin.Pant;
        if (PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin && PlayerPrefs.GetInt(Constants.CURRENT_PANT) != 0)
        {
            id = PlayerPrefs.GetInt(Constants.CURRENT_PANT);
        }
        else
        {
            id = 1;
        }
        content_Hair.SetActive(false);
        content_Shield.SetActive(false);
        content_Set.SetActive(false);
        content_Pant.SetActive(true);

        img_Hair.SetActive(false);
        img_Shield.SetActive(false);
        img_Set.SetActive(false);
        img_Pant.SetActive(true);
        LoadButton();
        LevelManager.Ins.playerCtl.IsActiveSkin(false);
        LevelManager.Ins.playerCtl.ChangeSkinPlayer.DelTestSkin();
    }
    public void BtnSelectSet()
    {
        scrollRect.content = content_Set.GetComponent<RectTransform>();
        idSkin = (int)ESkin.Set;
        if (PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin && PlayerPrefs.GetInt(Constants.CURRENT_SET) != 0)
        {
            id = PlayerPrefs.GetInt(Constants.CURRENT_SET);
        }
        else
        {
            id = 1;
        }
        content_Hair.SetActive(false);
        content_Shield.SetActive(false);
        content_Set.SetActive(true);
        content_Pant.SetActive(false);

        img_Hair.SetActive(false);
        img_Shield.SetActive(false);
        img_Set.SetActive(true);
        img_Pant.SetActive(false);
        LoadButton();
        LevelManager.Ins.playerCtl.IsActiveSkin(false);
        LevelManager.Ins.playerCtl.ChangeSkinPlayer.DelTestSkin();
    }

    private void LoadButtonHair()
    {
        string dataHair = PlayerPrefs.GetString(Constants.HAIR).ToString();

        if (dataHair.Contains(id.ToString()))
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(false);
            selectButton.SetActive(true);
        }
        else 
        {
            buyButton.SetActive(true);
            equippedButton.SetActive(false);
            selectButton.SetActive(false);
        }
        if (PlayerPrefs.GetInt(Constants.CURRENT_HAIR) == id && PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == (int)ESkin.Hair)
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(true);
            selectButton.SetActive(false);
        }
    }
    private void LoadButtonShiel()
    {
        string dataShield = PlayerPrefs.GetString(Constants.SHIELD).ToString();
        if (dataShield.Contains(id.ToString()))
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(false);
            selectButton.SetActive(true);
        }
        else 
        {
            buyButton.SetActive(true);
            equippedButton.SetActive(false);
            selectButton.SetActive(false);
        }
        if (PlayerPrefs.GetInt(Constants.CURRENT_SHIELD) == id && PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == (int)ESkin.Shiel)
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(true);
            selectButton.SetActive(false);
        }
    }
    private void LoadButtonPant()
    {
        string dataPant = PlayerPrefs.GetString(Constants.PANT).ToString();
        if (dataPant.Contains(id.ToString()))
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(false);
            selectButton.SetActive(true);
        }
        else 
        {
            buyButton.SetActive(true);
            equippedButton.SetActive(false);
            selectButton.SetActive(false);
        }
        if (PlayerPrefs.GetInt(Constants.CURRENT_PANT) == id && PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == (int)ESkin.Pant)
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(true);
            selectButton.SetActive(false);
        }
    }
    private void LoadButtonSet()
    {
        string dataSet = PlayerPrefs.GetString(Constants.SET).ToString();
        if (dataSet.Contains(id.ToString()))
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(false);
            selectButton.SetActive(true);
        }
        else 
        {
            buyButton.SetActive(true);
            equippedButton.SetActive(false);
            selectButton.SetActive(false);
        }
        if (PlayerPrefs.GetInt(Constants.CURRENT_SET) == id && PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == (int)ESkin.Set)
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(true);
            selectButton.SetActive(false);
        }
    }

}
