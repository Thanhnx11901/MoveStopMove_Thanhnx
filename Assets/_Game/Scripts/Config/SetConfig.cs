using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SetConfig", menuName = "ScriptableObjects/SetConfig", order = 1)]

public class SetConfig : ScriptableObject
{
    public List<Sprite> spriteSets; 

    public Sprite GetSpriteSet(ESet eSet){
        return spriteSets[(int)eSet];
    }
}
