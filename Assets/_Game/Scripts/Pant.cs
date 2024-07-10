using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class Pant : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;
    [SerializeField] private PantConfig pantConfig;

    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    // private void Awake() {
    //     PlayerPrefs.SetInt(Constants.CURRENT_PANT, 3);
    // }
    // private void Start() {
    //     ChangePant((EPant)PlayerPrefs.GetInt(Constants.CURRENT_PANT));
    // }
    public void ChangePant(EPant ePant){ 
        skinnedMeshRenderer.material = pantConfig.GetMaterialPant(ePant);
    }
}
