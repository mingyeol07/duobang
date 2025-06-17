using DG.Tweening;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public void Drop(Vector2 startPos)
    {
        transform.position = startPos;

        // 튕겨나갈 최종 목표 위치
        float xOffset = Random.Range(1.5f, 2.5f); // 오른쪽으로
        float yOffset = Random.Range(1f, 2f);     // 위로

        Vector2 midPos = startPos + new Vector2(xOffset, yOffset);    // 중간 포물선 위치
        Vector2 endPos = startPos + new Vector2(xOffset, 1.2f);      // 착지 위치

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(midPos, 0.3f).SetEase(Ease.OutQuad))   // 위로 튀기
           .Append(transform.DOMove(endPos, 0.3f).SetEase(Ease.InQuad));  // 아래로 착지
    }
}