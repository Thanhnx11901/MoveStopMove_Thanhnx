using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnShield : MonoBehaviour
{
    private SkinShop skinShop;

    public GameObject imgLock;
    public GameObject imgEquipped;
    private int idSkin;
    private int id;
    private EShield eShield;

    public GameObject imgSelect;

    public void OnInit(Shield shield, EShield eShield, int idSkin, int id, SkinShop skinShop)
    {
        Shield prefabShield = Instantiate(shield, transform);
        prefabShield.transform.localScale = prefabShield.ScaleShop;
        this.eShield = eShield;
        this.idSkin = idSkin;
        this.id = id;
        this.skinShop = skinShop;
        SetLockAndEquipped();

    }
    public void BtnShieldTest()
    {
        LevelManager.Ins.playerCtl.ShieldHolder.ShieldTest(eShield);
        skinShop.id = this.id;
        skinShop.idSkin = this.idSkin;
        skinShop.LoadButton();
    }
    public void SetLockAndEquipped()
    {
        if (PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin && PlayerPrefs.GetInt(Constants.CURRENT_SHIELD) == id)
        {
            imgEquipped.SetActive(true);
        }
        else
        {
            imgEquipped.SetActive(false);
        }
        if (PlayerPrefs.GetInt(eShield.ToString()) == 1)
        {
            imgLock.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(eShield.ToString()) == 0)
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