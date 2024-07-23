using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ESet
{
    None = 0,
    PhuThuy = 1,
    Deadpool = 2,
    Thor = 3,
}
public class SetHolder : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;

    public SkinnedMeshRenderer skinnedMeshRenderer;
    
    public Set curentSet;

    public void SpawnSet(ESet eSet){
        if(curentSet != null) Destroy(curentSet.gameObject);
        Set setNew = Instantiate(GameData.Ins.setConfig.GetSet(eSet), transform);
        curentSet = setNew;
        skinnedMeshRenderer.material = curentSet.material;   

    } 
    public void ChangeSet(ESet eSet){
        owner.ECurrentSet = eSet;
        SpawnSet(eSet);
    }
}
