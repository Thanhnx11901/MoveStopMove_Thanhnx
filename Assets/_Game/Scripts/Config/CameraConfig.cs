using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/CameraConfig", order = 1)]

public class CameraConfig : ScriptableObject
{
    public List<CameraPosition> cameraPos;
    public CameraPosition GetCameraPosition(int index){
        return cameraPos[index];
    }
}

[System.Serializable]
public class CameraPosition
{
    public Vector3 offset;
    public Vector3 rotation;
}
