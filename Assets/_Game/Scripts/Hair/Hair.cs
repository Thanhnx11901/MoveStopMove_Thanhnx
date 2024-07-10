using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hair : MonoBehaviour
{
    [SerializeField] private Vector3 position; 
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 scale;
    [SerializeField] private Vector3 scaleShop;


    public Vector3 Position => position;
    public Vector3 Rotation => rotation;
    public Vector3 Scale => scale;

    public Vector3 ScaleShop => scaleShop;


    
    public void OnInit(){
        transform.localPosition = Position;
        transform.localRotation = Quaternion.Euler(Rotation);
        transform.localScale = Scale;
    }
}
