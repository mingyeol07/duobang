using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 20f;

    private void LateUpdate()
    {
        if (target == null) return;

        // ���� ī�޶� ��ġ���� X�� �÷��̾� ���󰡰� �������� �״�� ����
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(target.position.x, minX, maxX);

        transform.position = newPosition;
    }
}