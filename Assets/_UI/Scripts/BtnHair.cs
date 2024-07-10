using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnHair : MonoBehaviour
{
    private SkinShop skinShop;
    private int idSkin;
    private int id;
    public GameObject imgLock;
    public GameObject imgEquipped;

    private EHair eHair;
    public void OnInit(Hair hair, EHair eHair, int idSkin, int id, SkinShop skinShop)
    {
        Hair prefabHair = Instantiate(hair, transform);
        prefabHair.transform.localScale = hair.ScaleShop;
        this.eHair = eHair;
        this.idSkin = idSkin;
        this.id = id;
        this.skinShop = skinShop;
        SetLockAndEquipped();

    }
    public void BtnHairTest()
    {
        LevelManager.Ins.playerCtl.HairHolder.HairTest(eHair);
        skinShop.id = this.id;
        skinShop.idSkin = this.idSkin;
        skinShop.LoadButton();
    }

    public void SetLockAndEquipped()
    {
        if (PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin && PlayerPrefs.GetInt(Constants.CURRENT_HAIR) == id)
        {
            imgEquipped.SetActive(true);
        }
        else
        {
            imgEquipped.SetActive(false);
        }
        if (PlayerPrefs.GetInt(eHair.ToString()) == 1)
        {
            imgLock.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(eHair.ToString()) == 0)
        {
            imgLock.SetActive(true);
        }
    }
}
