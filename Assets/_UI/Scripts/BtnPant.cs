using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPant : MonoBehaviour
{
    private SkinShop skinShop;

    public GameObject imgLock;
    public GameObject imgEquipped;
    private int idSkin;
    private int id;
    private EPant ePant;
    public GameObject imgSelect;


    [SerializeField] private MeshRenderer meshRenderer;
    public void OnInit(EPant ePant, int idSkin, int id, SkinShop skinShop)
    {
        meshRenderer.material = GameData.Ins.pantConfig.GetMaterialPant(ePant);
        this.ePant = ePant;
        this.idSkin = idSkin;
        this.id = id;
        this.skinShop = skinShop;
        SetLockAndEquipped();

    }
    public void BtnShieldTest()
    {
        LevelManager.Ins.playerCtl.Pant.ChangePant(ePant);
        skinShop.id = this.id;
        skinShop.idSkin = this.idSkin;
        skinShop.LoadButton();
    }
    public void SetLockAndEquipped()
    {
        if (PlayerPrefs.GetInt(Constants.CURRENT_SKIN) == idSkin && PlayerPrefs.GetInt(Constants.CURRENT_PANT) == id)
        {
            imgEquipped.SetActive(true);
        }
        else
        {
            imgEquipped.SetActive(false);
        }
        if (PlayerPrefs.GetInt(ePant.ToString()) == 1)
        {
            imgLock.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(ePant.ToString()) == 0)
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
