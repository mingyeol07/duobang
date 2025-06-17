using UnityEngine;


public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 20f;
    [SerializeField] private float shakeDuration = 0f;
    [SerializeField] private float shakeMagnitude = 0.2f;

    private Vector3 shakeOffset = Vector3.zero;
    private Vector3 basePosition = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        // 기준 위치는 항상 흔들림 없는 플레이어 기준 위치여야 함
        float targetX = Mathf.Clamp(target.position.x + 1, minX, maxX);
        basePosition = new Vector3(targetX, 0f, transform.position.z); // y = 0 또는 원하는 고정값으로 설정

        // 쉐이크 계산
        if (shakeDuration > 0f)
        {
            shakeOffset = (Vector3)(Random.insideUnitCircle * shakeMagnitude);
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeOffset = Vector3.zero;
        }

        transform.position = basePosition + shakeOffset;
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}