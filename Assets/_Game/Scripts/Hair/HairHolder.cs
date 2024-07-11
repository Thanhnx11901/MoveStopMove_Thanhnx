using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairHolder : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;

    public Hair currentHairtest;

    public void DelHair(){
        //if(owner.ECurrentHair != EHair.None ) owner.DelAttackRange(0.05f);
        Destroy(owner.CurrentHair.gameObject);
    }

    public void ChangeHair(EHair eHair)
    {
        //if (eHair != EHair.None) owner.AddAttackRange(0.05f);
        
        if(owner.CurrentHair != null) DelHair();

        Hair hair = Instantiate(GameData.Ins.hairConfig.GetHair(eHair), transform);
        hair.OnInit();

        owner.CurrentHair = hair;
        owner.ECurrentHair = eHair;
    }

    public void HairTest(EHair eHair)
    {
        if(owner.CurrentHair != null) owner.CurrentHair.gameObject.SetActive(false);
        if(currentHairtest != null) Destroy(currentHairtest.gameObject);
        Hair hair = Instantiate(GameData.Ins.hairConfig.GetHair(eHair), transform);
        currentHairtest = hair;
        hair.OnInit();
    }
}
