using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private const float speed = 0.5f;
    private Color fadeColor = new Color(1, 1, 1, 0);
    private WaitForSeconds hideWaitTime = new WaitForSeconds(2f);

    public void Show(Vector3 worldPos, int damage, bool isCritical)
    {
        // ���� ĵ���� �������� �ٷ� ��ġ ����
        transform.position = worldPos + Vector3.up * 0.6f; // ĳ���� �Ӹ� ���ʿ� ��ġ

        text.text = damage.ToString();
        text.color = fadeColor;
        text.transform.localPosition = new Vector3(0, -0.3f, 0);

        transform.DOKill();
        text.transform.DOKill();

        // ���� ��ǥ �������� ���� �ε巴�� �̵��ϸ鼭 ���̵� ��
        text.DOFade(1f, speed).SetId(this);
        text.transform.DOLocalMoveY(0, 0.5f).SetId(this); // ���� Y�� �������� �ö�

        StartCoroutine(Co_Hide());
    }

    private IEnumerator Co_Hide()
    {
        yield return hideWaitTime;
        Hide();
    }

    public void Hide()
    {
        text.DOFade(0, speed).OnComplete(() => { UIManager.Instance.DamageTextPool.Return(this); });
        text.transform.DOLocalMoveY(0.3f, speed);
    }

    public void MoveUp()
    {
        transform.DOMoveY(transform.position.y + 0.5f, speed);
    }
}