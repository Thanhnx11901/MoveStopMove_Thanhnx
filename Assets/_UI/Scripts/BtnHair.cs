using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnHair : MonoBehaviour
{
    private SkinShop skinShop;
    private int idSkin;
    private int id;
    public GameObject imgLock;
    public GameObject imgEquipped;

    public GameObject imgSelect;

    public Image image;

    private EHair eHair;
    public void OnInit(Hair hair, EHair eHair, int idSkin, int id, SkinShop skinShop)
    {
        ItemData itemData = GameData.Ins.itemDataConfig.GetItemData(ESkin.Hair, id);
        image.sprite = itemData.icon;
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

        string data = PlayerPrefs.GetString(Constants.HAIR).ToString();
        if (data.Contains(id.ToString()))
        {
            imgLock.SetActive(false);
        }
        else
        {
            imgLock.SetActive(true);
        }

        if (skinShop.idSkin == idSkin && skinShop.id == id)
        {
            imgSelect.SetActive(true);
        }
        else
        {
            imgSelect.SetActive(false);
        }
    }
}
