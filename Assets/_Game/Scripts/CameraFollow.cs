using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // Tham chiếu đến transform của người chơi
    public float smoothSpeed = 0.125f; // Tốc độ mượt của camera
    public Vector3 offset; // Độ lệch vị trí giữa camera và người chơi
    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Tính toán vị trí mục tiêu của camera dựa trên vị trí hiện tại của người chơi và offset
            Vector3 desiredPosition = playerTransform.position + offset;

            // Sử dụng phương pháp Interpolation để làm cho camera mượt mà hơn
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Cập nhật vị trí của camera đến vị trí mục tiêu mượt mà hơn
            transform.position = smoothedPosition;
        }
        
        if (GameManager.IsState(GameState.MainMenu))
        {
            CameraPosition CameraPosition = GameData.Ins.cameraConfig.GetCameraPosition(0);
            offset = CameraPosition.offset; 
            transform.rotation = Quaternion.Euler(CameraPosition.rotation);
        }
        if (GameManager.IsState(GameState.GamePlay))
        {
            CameraPosition CameraPosition = GameData.Ins.cameraConfig.GetCameraPosition(1);
            offset = CameraPosition.offset;
            transform.rotation = Quaternion.Euler(CameraPosition.rotation);
        }
        if (GameManager.IsState(GameState.ShopSkin))
        {
            CameraPosition CameraPosition = GameData.Ins.cameraConfig.GetCameraPosition(2);
            offset = CameraPosition.offset;
            transform.rotation = Quaternion.Euler(CameraPosition.rotation); 
        }
        if (GameManager.IsState(GameState.Victory))
        {
            CameraPosition CameraPosition = GameData.Ins.cameraConfig.GetCameraPosition(3);
            offset = CameraPosition.offset;
            transform.rotation = Quaternion.Euler(CameraPosition.rotation); 
        }
    }
}
