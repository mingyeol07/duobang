using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 20f;

    private void LateUpdate()
    {
        if (target == null) return;

        // 현재 카메라 위치에서 X만 플레이어 따라가고 나머지는 그대로 유지
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(target.position.x, minX, maxX);

        transform.position = newPosition;
    }
}