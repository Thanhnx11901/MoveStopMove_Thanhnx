using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SetConfig", menuName = "ScriptableObjects/SetConfig", order = 1)]

public class SetConfig : ScriptableObject
{
    public List<Set> sets; 

    public Set GetSet(ESet eSet){
        return sets[(int)eSet];
    }
}
