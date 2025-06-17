using DG.Tweening;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public void Drop(Vector2 startPos)
    {
        transform.position = startPos;

        // ƨ�ܳ��� ���� ��ǥ ��ġ
        float xOffset = Random.Range(1.5f, 2.5f); // ����������
        float yOffset = Random.Range(1f, 2f);     // ����

        Vector2 midPos = startPos + new Vector2(xOffset, yOffset);    // �߰� ������ ��ġ
        Vector2 endPos = startPos + new Vector2(xOffset, 1.2f);      // ���� ��ġ

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(midPos, 0.3f).SetEase(Ease.OutQuad))   // ���� Ƣ��
           .Append(transform.DOMove(endPos, 0.3f).SetEase(Ease.InQuad));  // �Ʒ��� ����
    }
}