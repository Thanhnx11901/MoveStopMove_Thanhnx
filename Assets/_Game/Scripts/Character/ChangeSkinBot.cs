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
    [SerializeField] public SkinnedMeshRenderer skinnedMeshRenderer;



    public void LoadSkinRandom()
    {
        ChangeSkinDefault();
        DelTestSkin();
        skinnedMeshRenderer.material.color = UnityEngine.Random.ColorHSV();
        ESkin CurrentSkin = GetRandomEnumValue<ESkin>();
        if (CurrentSkin == ESkin.Hair)
        {
            hairHolder.ChangeHair(GetRandomEnumValue<EHair>());
            owner.AddAttackRange(0.05f);
        }
        if (CurrentSkin == ESkin.Shiel)
        {
            shieldHolder.ChangeShield(GetRandomEnumValue<EShield>());
            owner.AddAttackSpeed(0.05f);
        }
        if (CurrentSkin == ESkin.Set)
        {
            setHolder.ChangeSet(GetRandomEnumValue<ESet>());
            owner.AddAttackRange(0.08f);

        }
        if (CurrentSkin == ESkin.Pant)
        {
            pant.ChangePant(GetRandomEnumValue<EPant>());
            owner.AddMoveSpeed(0.08f);

        }
    }

    public void ChangeSkinDefault()
    {
        if (owner.CurrentHair != null) hairHolder.DelHair();
        if (owner.CurrentShield != null) shieldHolder.DelShield();
        if (owner.Pant != null) pant.ChangePant(EPant.None);
        if (owner.SetHolder != null) { owner.SetHolder.ChangeSet(ESet.None); }

    }

    public void DelTestSkin()
    {
        if (hairHolder.currentHairtest != null) { Destroy(hairHolder.currentHairtest.gameObject); }
        if (shieldHolder.currentShieldtest != null) { Destroy(shieldHolder.currentShieldtest.gameObject); }
        if (owner.Pant != null) pant.ChangePant(EPant.None);
        if (owner.SetHolder != null) { owner.SetHolder.ChangeSet(ESet.None); }
    }
    public T GetRandomEnumValue<T>() where T : Enum
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        int randomIndex = UnityEngine.Random.Range(0, values.Length);
        return values[randomIndex];
    }
}
