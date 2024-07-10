using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skin{
    None = 0,
    Hair = 1,
    Shiel = 2,
    Set = 3,
    Pant = 4,
}
public class ChangeSkinPlayer : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;
    [SerializeField] private HairHolder hairHolder;
    [SerializeField] private ShieldHolder shieldHolder;
    [SerializeField] private Pant pant;
    [SerializeField] private SetHolder setHolder;

    private void Start() {
        // PlayerPrefs.SetInt(Constants.CURRENT_HAIR,1);
        // PlayerPrefs.SetInt(Constants.CURRENT_SHIELD,1);

        LoadSkin();

    }
    public void LoadSkin(){
        ChangeSkinDefault();
        DelTestSkin();
        Skin CurrentSkin =  (Skin)PlayerPrefs.GetInt(Constants.CURRENT_SKIN);
        if(CurrentSkin == Skin.Hair){
            hairHolder.ChangeHair((EHair)PlayerPrefs.GetInt(Constants.CURRENT_HAIR));
        }
        if(CurrentSkin == Skin.Shiel){
            shieldHolder.ChangeShield((EShield)PlayerPrefs.GetInt(Constants.CURRENT_SHIELD));
        }
        // if(CurrentSkin == Skin.Set){
        // }
        // if(CurrentSkin == Skin.Pant){
        // }
    }

    public void ChangeSkinDefault(){
        if(owner.CurrentHair != null) hairHolder.DelHair();
        if(owner.CurrentShield != null) shieldHolder.DelShield();
    }

    public void DelTestSkin(){
        if(hairHolder.currentHairtest != null){ Destroy(hairHolder.currentHairtest.gameObject);}
        if(shieldHolder.currentShieldtest != null){ Destroy(shieldHolder.currentShieldtest.gameObject);}

    }   
}
