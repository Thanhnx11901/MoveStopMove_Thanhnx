using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // Tham chiếu đến transform của người chơi
    public float smoothSpeed = 0.125f; // Tốc độ mượt của camera
    public Vector3 offset_GamePlay; // Độ lệch vị trí giữa camera và người chơi

    public Vector3 offset_GameMainNenu;
    public Vector3 offset_GameSkin;



    void FixedUpdate()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            if (playerTransform != null)
            {
                // Tính toán vị trí mục tiêu của camera dựa trên vị trí hiện tại của người chơi và offset
                Vector3 desiredPosition = playerTransform.position + offset_GamePlay;

                // Sử dụng phương pháp Interpolation để làm cho camera mượt mà hơn
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

                // Cập nhật vị trí của camera đến vị trí mục tiêu mượt mà hơn
                transform.position = smoothedPosition;

                transform.rotation = Quaternion.Euler(new Vector3(40f,0,0));
            }
        }
        if (GameManager.IsState(GameState.MainMenu)) {

            if (playerTransform != null)
            {
                // Tính toán vị trí mục tiêu của camera dựa trên vị trí hiện tại của người chơi và offset
                Vector3 desiredPosition = playerTransform.position + offset_GameMainNenu;

                // Sử dụng phương pháp Interpolation để làm cho camera mượt mà hơn
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

                // Cập nhật vị trí của camera đến vị trí mục tiêu mượt mà hơn
                transform.position = smoothedPosition;

                transform.rotation = Quaternion.Euler(new Vector3(25,0,0));
            }
        }

        if (GameManager.IsState(GameState.ShopSkin)) {

            if (playerTransform != null)
            {
                // Tính toán vị trí mục tiêu của camera dựa trên vị trí hiện tại của người chơi và offset
                Vector3 desiredPosition = playerTransform.position + offset_GameSkin;

                // Sử dụng phương pháp Interpolation để làm cho camera mượt mà hơn
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

                // Cập nhật vị trí của camera đến vị trí mục tiêu mượt mà hơn
                transform.position = smoothedPosition;

                transform.rotation = Quaternion.Euler(new Vector3(35,0,0));
            }
        }
    }
}
