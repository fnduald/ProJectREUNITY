using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 따라갈 플레이어 타겟
    public float smoothSpeed = 0.125f; // 카메라가 따라오는 부드러운 정도
    public Vector3 offset; // 카메라가 캐릭터에서 떨어져 있을 거리

    private void LateUpdate()
    {
        if (player != null)
        {
            // 목표 위치 계산
            Vector3 desiredPosition = player.position + offset;
            // 부드럽게 위치 보간 (Lerp)
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}