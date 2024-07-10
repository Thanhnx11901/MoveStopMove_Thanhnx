using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EShield
{
    None = 0,
    Khien = 1,
    Shield = 2,
}

[CreateAssetMenu(fileName = "ShieldConfig", menuName = "ScriptableObjects/ShieldConfig", order = 1)]

public class ShieldConfig : ScriptableObject
{
    public List<Shield> shields; 

    public Shield GetShield(EShield eShield){
        return shields[(int)eShield];
    }
}
