using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform[] backgrounds; // 7개 배경
    [SerializeField] private float[] multipliers;     // 배경별 패럴렉스 비율

    private Vector3 previousCameraPos;

    void Start()
    {
        previousCameraPos = cameraTransform.position;

        if (backgrounds.Length != multipliers.Length)
        {
            Debug.LogError("배경 개수와 multiplier 수가 다릅니다!");
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
                bgPos.y, // Y축 고정
                bgPos.z
            );
        }

        previousCameraPos = cameraTransform.position;
    }
}