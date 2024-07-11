using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class Pant : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;
    [SerializeField] private PantConfig pantConfig;

    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    public void ChangePant(EPant ePant){ 
        skinnedMeshRenderer.material = pantConfig.GetMaterialPant(ePant);
    }
}
