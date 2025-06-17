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

        // ���� ��ġ�� �׻� ��鸲 ���� �÷��̾� ���� ��ġ���� ��
        float targetX = Mathf.Clamp(target.position.x + 1, minX, maxX);
        basePosition = new Vector3(targetX, 0f, transform.position.z); // y = 0 �Ǵ� ���ϴ� ���������� ����

        // ����ũ ���
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