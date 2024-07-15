using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeapon
{
    Hammer = 0,
    Candy = 1,
    Boomerang = 2,
    Axe= 3,
    Knife = 4,
}

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig", order = 1)]

public class WeaponConfig : ScriptableObject
{
    public List<Weapon> weapons;

    public Weapon GetWeapon(EWeapon eWeapon){
        return weapons[(int)eWeapon];
    }
}
