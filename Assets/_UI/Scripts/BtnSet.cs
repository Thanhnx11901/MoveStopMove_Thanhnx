using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSet : MonoBehaviour
{
    private SkinShop skinShop;
    public GameObject imgLock;
    public GameObject imgEquipped;
    private int idSkin;
    private int id;
    private ESet eSet;

    public GameObject imgSelect;


    [SerializeField] private Image image;
    public void OnInit(ESet eSet, int idSkin, int id, SkinShop skinShop)
    {
        ItemData itemData = GameData.Ins.itemDataConfig.GetItemData(ESkin.Set,id);
        image.sprite = itemData.icon;
        this.eSet = eSet;
        this.idSkin = idSkin;
        this.id = id;
        this.skinShop = skinShop;
        SetLockAndEquipped();

    }
    public void BtnShieldTest()
    {
        LevelManager.Ins.playerCtl.SetHolder.ChangeSet(eSet);
        skinShop.id = this.id;
        skinShop.idSkin = this.idSkin;
        skinShop.LoadButton();
    }
    public void SetLockAndEquipped()
    {
        if (PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin && PlayerPrefs.GetInt(Constants.CURRENT_SET) == id)
        {
            imgEquipped.SetActive(true);
        }
        else
        {
            imgEquipped.SetActive(false);
        }
        string data = PlayerPrefs.GetString(Constants.SET).ToString();
        if (data.Contains(id.ToString()))
        {
            imgLock.SetActive(false);
        }
        else
        {
            imgLock.SetActive(true);
        }
        if(skinShop.idSkin == idSkin && skinShop.id == id){
            imgSelect.SetActive(true);
        }
        else{
            imgSelect.SetActive(false);
        }
    }
    
}