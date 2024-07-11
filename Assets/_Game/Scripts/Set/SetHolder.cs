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

    public List<Set> sets;

    public SkinnedMeshRenderer skinnedMeshRenderer;
    
    public Set curentSet;
    public void ActiveSet(ESet eSet){
        for(int i = 0; i < sets.Count; i++){
            sets[i].SetActive(false);
        }
        sets[(int)eSet].SetActive(true);
        curentSet = sets[(int)eSet];
        skinnedMeshRenderer.material = sets[(int)eSet].material;   

    } 
    public void DeActiveSet(){
        for(int i = 0; i < sets.Count; i++){
            sets[i].SetActive(false);
        }
        skinnedMeshRenderer.material = sets[(int)ESet.None].material;   
    }
    public void ChangeSet(ESet eSet){
        owner.ECurrentSet = eSet;
        ActiveSet(eSet);
    }

    public void ChangeSetBot(ESet eSet){
        owner.ECurrentSet = eSet;
        ActiveSet(eSet);
    }
}
