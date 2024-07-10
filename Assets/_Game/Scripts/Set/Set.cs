using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public Material material;
    public void OnInit(){
        SetActive(true);
    }
    public void SetActive(bool isActive){
        for (int i = 0;i < gameObjects.Count; i++){
            gameObjects[i].SetActive(isActive);
        }
    }
}
