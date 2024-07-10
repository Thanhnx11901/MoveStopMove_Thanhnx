using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShop : UICanvas
{
    [SerializeField] private GameObject equippedButton;

    [SerializeField] private GameObject selectButton;

    [SerializeField] private GameObject buyButton;
    public GameObject content_Hair;

    public GameObject content_Set;

    public GameObject content_Pant;

    public GameObject content_Shield;
    public BtnHair prefabBtnHair;
    public BtnPant prefabBtnPant;
    public BtnShield prefabBtnShield;
    public BtnSet prefabBtnSet;

    public List<BtnHair> btnHairs = new List<BtnHair>();    
    public List<BtnShield> btnShields = new List<BtnShield>();


    public int idSkin;
    public int id;
    private void Start()
    {
        idSkin = 1;
        id = 0;
        Spawn_Hair();
        LoadButton();
        Spawn_Shield();
    }
    public void Spawn_Hair()
    {
        foreach (EHair hairStyle in System.Enum.GetValues(typeof(EHair)))
        {
            if (hairStyle == EHair.None) continue;
            BtnHair btnHair = Instantiate(prefabBtnHair, content_Hair.transform);
            btnHair.OnInit(GameData.Ins.hairConfig.GetHair(hairStyle), hairStyle, (int)Skin.Hair, (int)hairStyle, this);
            btnHairs.Add(btnHair);
        }
    }
    public void ReLoadBtn_Hair(){
        for (int i = 0; i < btnHairs.Count ; i++){
            btnHairs[i].SetLockAndEquipped();
        }
    }
    public void ReLoadBtn_Shield(){
        for (int i = 0; i < btnShields.Count ; i++){
            btnShields[i].SetLockAndEquipped();
        }
    }
    public void Spawn_Shield()
    {
        foreach (EShield shieldStyle in System.Enum.GetValues(typeof(EShield)))
        {
            if (shieldStyle == EShield.None) continue;
            BtnShield btnShield = Instantiate(prefabBtnShield, content_Shield.transform);
            btnShield.OnInit(GameData.Ins.shieldConfig.GetShield(shieldStyle), shieldStyle, (int)Skin.Shiel, (int)shieldStyle, this);
            btnShields.Add(btnShield);
        }
    }
    public void Spawn_Pant()
    {

    }
    // public void Spawn_Hair()
    // {
    //     foreach (EHair hairStyle in System.Enum.GetValues(typeof(EHair)))
    //     {
    //         BtnHair btnHair = Instantiate(prefabBtnHair, content_Hair.transform);
    //         btnHair.OnInit(GameData.Ins.hairConfig.GetHair(hairStyle));
    //     }
    // }
    public void LoadButton()
    {
        Skin skin = (Skin)idSkin;
        if (skin == Skin.Hair)
        {
            LoadButtonHair();
            ReLoadBtn_Hair();
        }
        if (skin == Skin.Shiel)
        {
            LoadButtonShiel();
            ReLoadBtn_Shield();
        }
        // if(skin == Skin.Set){
        // }
        // if(skin == Skin.Pant){
        // }

    }
    public void SelectButton()
    {
        Skin skin = (Skin)idSkin;
        if (skin == Skin.Hair)
        {
            PlayerPrefs.SetInt(Constants.CURRENT_SKIN, idSkin);
            PlayerPrefs.SetInt(Constants.CURRENT_HAIR, id);
            LevelManager.Ins.playerCtl.ChangeSkinPlayer.LoadSkin();
            LevelManager.Ins.playerCtl.IsActiveSkin(true);
            LoadButton();
            
        }
        if (skin == Skin.Shiel)
        {
            PlayerPrefs.SetInt(Constants.CURRENT_SKIN, idSkin);
            PlayerPrefs.SetInt(Constants.CURRENT_SHIELD, id);
            LevelManager.Ins.playerCtl.ChangeSkinPlayer.LoadSkin();
            LevelManager.Ins.playerCtl.IsActiveSkin(true);
            LoadButton();
            
        }
        // if(skin == Skin.Set){
        // }
        // if(skin == Skin.Pant){
        // }


    }
    public void EquippedButton()
    {
        PlayerPrefs.SetInt(Constants.CURRENT_SKIN, 1);
        PlayerPrefs.SetInt(Constants.CURRENT_HAIR, 0);
        PlayerPrefs.SetInt(Constants.CURRENT_SHIELD, 0);
        LevelManager.Ins.playerCtl.ChangeSkinPlayer.LoadSkin();
        LevelManager.Ins.playerCtl.IsActiveSkin(false);
        LoadButton();
    }
    public void BuyButton()
    {
        Skin skin = (Skin)idSkin;
        if (skin == Skin.Hair)
        {
            EHair hair = (EHair)id;
            PlayerPrefs.SetInt(hair.ToString(), 1);
            LoadButton();
        }
        if (skin == Skin.Shiel)
        {
            EShield eShield = (EShield)id;
            PlayerPrefs.SetInt(eShield.ToString(), 1);
            LoadButton();
        }
        // if(skin == Skin.Set){
        // }
        // if(skin == Skin.Pant){
        // }   
    }

    public void ExitButton()
    {
        GameManager.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MianMenu>();
        LevelManager.Ins.playerCtl.ChangeSkinPlayer.LoadSkin();
        LevelManager.Ins.playerCtl.IsActiveSkin(true);
        Close(0);

    }

    public void BtnSelectHair()
    {
        idSkin = (int)Skin.Hair;
        if(PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin){
            id = PlayerPrefs.GetInt(Constants.CURRENT_SHIELD);
        }
        else{
            id = 1;
        }
        content_Hair.SetActive(true);
        content_Shield.SetActive(false);
        content_Set.SetActive(false);
        content_Pant.SetActive(false);
        LoadButton();
        LevelManager.Ins.playerCtl.IsActiveSkin(false);
        LevelManager.Ins.playerCtl.ChangeSkinPlayer.DelTestSkin();

    }
    public void BtnSelectShield()
    {
        idSkin = (int)Skin.Shiel;
         if(PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin){
            id = PlayerPrefs.GetInt(Constants.CURRENT_HAIR);
        }
        else{
            id = 1;
        }
        content_Hair.SetActive(false);
        content_Shield.SetActive(true);
        content_Set.SetActive(false);
        content_Pant.SetActive(false);
        LoadButton();
        LevelManager.Ins.playerCtl.IsActiveSkin(false);
        LevelManager.Ins.playerCtl.ChangeSkinPlayer.DelTestSkin();
    }

    private void LoadButtonHair()
    {
        EHair hair = (EHair)id;
        if (PlayerPrefs.GetInt(hair.ToString()) == 0)
        {
            buyButton.SetActive(true);
            equippedButton.SetActive(false);
            selectButton.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(hair.ToString()) == 1 )
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(false);
            selectButton.SetActive(true);
        }
        if (PlayerPrefs.GetInt(Constants.CURRENT_HAIR) == id)
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(true);
            selectButton.SetActive(false);
        }
    }
    private void LoadButtonShiel()
    {
        EShield eShield = (EShield)id;
        if (PlayerPrefs.GetInt(eShield.ToString()) == 0)
        {
            buyButton.SetActive(true);
            equippedButton.SetActive(false);
            selectButton.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(eShield.ToString()) == 1)
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(false);
            selectButton.SetActive(true);
        }
        if (PlayerPrefs.GetInt(Constants.CURRENT_SHIELD) == id)
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(true);
            selectButton.SetActive(false);
        }
    }

}
