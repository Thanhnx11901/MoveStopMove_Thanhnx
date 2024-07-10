using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHolder : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;

    public Shield currentShieldtest;

    public void DelShield(){
        Destroy(owner.CurrentShield.gameObject);
    }

    public void ChangeShield(EShield eShield)
    {
        if(owner.CurrentShield != null) DelShield();
        PlayerPrefs.SetInt(Constants.CURRENT_SKIN,(int)Skin.Shiel);

        PlayerPrefs.SetInt(Constants.CURRENT_SHIELD, (int)eShield);

        Shield shield = Instantiate(GameData.Ins.shieldConfig.GetShield(eShield), transform);
        shield.OnInit();
        owner.CurrentShield = shield;
        owner.ECurrentShield = eShield;
    }

    public void ShieldTest(EShield eShield)
    {
        if(owner.CurrentShield != null) owner.CurrentShield.gameObject.SetActive(false);
        if(currentShieldtest != null) Destroy(currentShieldtest.gameObject);
        Shield shield = Instantiate(GameData.Ins.shieldConfig.GetShield(eShield), transform);
        shield.OnInit();
        currentShieldtest = shield;
    }
}
