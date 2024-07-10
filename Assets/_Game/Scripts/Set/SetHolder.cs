using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkinCharacter
{
    None = 0,
    PhuThuy = 1,
    Deadpool = 2,
}
public class SetHolder : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;

    public List<Set> sets;

    public SkinnedMeshRenderer skinnedMeshRenderer;
    
    public Set curentSet;
    public void ActiveSet(SkinCharacter skinCharacter){
        for(int i = 0; i < sets.Count; i++){
            sets[i].SetActive(false);
        }
        sets[(int)skinCharacter].SetActive(true);
        curentSet = sets[(int)skinCharacter];
        skinnedMeshRenderer.material = sets[(int)skinCharacter].material;   

    }
    public void ChangeSet(SkinCharacter skinCharacter){
        owner.CurrentSkinCharacter = skinCharacter;
        ActiveSet(skinCharacter);
    }
}
