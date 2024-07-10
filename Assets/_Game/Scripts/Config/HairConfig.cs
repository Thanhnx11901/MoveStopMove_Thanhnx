using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EHair
{
    None = 0,
    Arrow = 1,
    Cowboy = 2,
    Crown = 3,
    Ear = 4,
    Hat = 5,
    Hat_Cap = 6,
    Hat_Yellow = 7,
    HeadPhone = 8,
    Rau = 9,
}
[CreateAssetMenu(fileName = "HairConfig", menuName = "ScriptableObjects/HairConfig", order = 1)]

public class HairConfig : ScriptableObject
{
    public List<Hair> hairs; 

    public Hair GetHair(EHair eHair){
        return hairs[(int)eHair];
    }
}
