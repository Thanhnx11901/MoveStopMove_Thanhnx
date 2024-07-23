using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESkin{
    None = 0,
    Hair = 1,
    Shiel = 2,
    Set = 3,
    Pant = 4,
    Weapon=5,
}
public class ChangeSkinPlayer : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;
    [SerializeField] private HairHolder hairHolder;
    [SerializeField] private ShieldHolder shieldHolder;
    [SerializeField] private Pant pant;
    [SerializeField] private SetHolder setHolder;

    public void LoadSkin(){
        ChangeSkinDefault();
        DelTestSkin();
        ESkin CurrentSkin =  (ESkin)PlayerPrefs.GetInt(Constants.CURRENT_SKIN);
        if(CurrentSkin == ESkin.Hair && PlayerPrefs.GetInt(Constants.CURRENT_HAIR) != 0){
            hairHolder.ChangeHair((EHair)PlayerPrefs.GetInt(Constants.CURRENT_HAIR));
            owner.AddAttackRange(0.05f);
        }
        if(CurrentSkin == ESkin.Shiel && PlayerPrefs.GetInt(Constants.CURRENT_SHIELD) != 0){
            shieldHolder.ChangeShield((EShield)PlayerPrefs.GetInt(Constants.CURRENT_SHIELD));
            owner.AddAttackSpeed(0.05f);
        }
        if(CurrentSkin == ESkin.Set && PlayerPrefs.GetInt(Constants.CURRENT_SET) != 0){
            setHolder.ChangeSet((ESet)PlayerPrefs.GetInt(Constants.CURRENT_SET));
            owner.AddAttackRange(0.08f);
        }
        if(CurrentSkin == ESkin.Pant && PlayerPrefs.GetInt(Constants.CURRENT_PANT) != 0){
            pant.ChangePant((EPant)PlayerPrefs.GetInt(Constants.CURRENT_PANT));
            owner.AddMoveSpeed(0.08f);
        }
    }

    public void ChangeSkinDefault(){
        if(owner.CurrentHair != null) hairHolder.DelHair();
        if(owner.CurrentShield != null) shieldHolder.DelShield();
        if(owner.Pant != null) pant.ChangePant(EPant.None);
        if(owner.SetHolder != null) { owner.SetHolder.ChangeSet(ESet.None);}

    }

    public void DelTestSkin(){
        if(hairHolder.currentHairtest != null){ Destroy(hairHolder.currentHairtest.gameObject);}
        if(shieldHolder.currentShieldtest != null){ Destroy(shieldHolder.currentShieldtest.gameObject);}
        if(owner.Pant != null) pant.ChangePant(EPant.None);
        if(owner.SetHolder != null) { owner.SetHolder.ChangeSet(ESet.None);}
    }   
}
