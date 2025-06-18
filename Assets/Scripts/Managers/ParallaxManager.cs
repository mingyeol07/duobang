using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform[] backgrounds; // 7�� ���
    [SerializeField] private float[] multipliers;     // ��溰 �з����� ����

    private Vector3 previousCameraPos;

    void Start()
    {
        previousCameraPos = cameraTransform.position;

        if (backgrounds.Length != multipliers.Length)
        {
            Debug.LogError("��� ������ multiplier ���� �ٸ��ϴ�!");
        }
    }

    void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - previousCameraPos;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector3 bgPos = backgrounds[i].position;
            backgrounds[i].position = new Vector3(
                bgPos.x + delta.x * multipliers[i],
                bgPos.y, // Y�� ����
                bgPos.z
            );
        }

        previousCameraPos = cameraTransform.position;
    }
}