using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decor : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material material;
    [SerializeField] private Material materialShader;
    public void SetMetarial(){
        meshRenderer.material = material;
    }
    public void SetMetarialShader(){
        meshRenderer.material = materialShader;
    }
}
