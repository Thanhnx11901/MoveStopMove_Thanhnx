using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPant
{
    None = 0,
    Batman = 1,
    Chambi = 2,
    Comy = 3,
    Dabao = 4,
    Onion = 5,
    Pokemon = 6,
    Rainbow = 7,
    Skull = 8,
    Vantim = 9,
}

[CreateAssetMenu(fileName = "PantConfig", menuName = "ScriptableObjects/PantConfig", order = 1)]

public class PantConfig : ScriptableObject
{
    public List<Material> materialPant; 

    public Material GetMaterialPant(EPant ePant){
        return materialPant[(int)ePant];
    }
}
