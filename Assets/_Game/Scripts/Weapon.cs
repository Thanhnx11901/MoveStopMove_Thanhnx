using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] protected CharacterCtl oner;
    public virtual void Fire(CharacterCtl enemy)
    {
        Debug.Log("fire");
    }
}
