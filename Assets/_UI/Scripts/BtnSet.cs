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
        image.sprite = GameData.Ins.setConfig.GetSpriteSet(eSet);
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
        if (PlayerPrefs.GetInt(eSet.ToString()) == 1)
        {
            imgLock.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(eSet.ToString()) == 0)
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