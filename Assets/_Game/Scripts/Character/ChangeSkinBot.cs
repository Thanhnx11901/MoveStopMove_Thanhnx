using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ChangeSkinBot : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;
    [SerializeField] private HairHolder hairHolder;
    [SerializeField] private ShieldHolder shieldHolder;
    [SerializeField] private Pant pant;
    [SerializeField] private SetHolder setHolder;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;



    public void LoadSkinRandom(){
        ChangeSkinDefault();
        DelTestSkin();
        skinnedMeshRenderer.material.color =  UnityEngine.Random.ColorHSV();
        Skin CurrentSkin =  GetRandomEnumValue<Skin>();
        if(CurrentSkin == Skin.Hair){
            hairHolder.ChangeHair(GetRandomEnumValue<EHair>());
        }
        if(CurrentSkin == Skin.Shiel){
            shieldHolder.ChangeShield(GetRandomEnumValue<EShield>());
        }
        if(CurrentSkin == Skin.Set){
            setHolder.ChangeSet(GetRandomEnumValue<ESet>());
        }
        if(CurrentSkin == Skin.Pant){
            pant.ChangePant(GetRandomEnumValue<EPant>());
        }
    }

    public void ChangeSkinDefault(){
        if(owner.CurrentHair != null) hairHolder.DelHair();
        if(owner.CurrentShield != null) shieldHolder.DelShield();
        if(owner.Pant != null) pant.ChangePant(EPant.None);
        if(owner.SetHolder != null) { owner.SetHolder.DeActiveSet();}

    }

    public void DelTestSkin(){
        if(hairHolder.currentHairtest != null){ Destroy(hairHolder.currentHairtest.gameObject);}
        if(shieldHolder.currentShieldtest != null){ Destroy(shieldHolder.currentShieldtest.gameObject);}
        if(owner.Pant != null) pant.ChangePant(EPant.None);
        if(owner.SetHolder != null) { owner.SetHolder.DeActiveSet();}
    }   
    public T GetRandomEnumValue<T>() where T : Enum
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        int randomIndex = UnityEngine.Random.Range(0, values.Length);
        return values[randomIndex];
    }
}
