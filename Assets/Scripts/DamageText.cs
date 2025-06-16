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
        // 월드 캔버스 기준으로 바로 위치 설정
        transform.position = worldPos + Vector3.up * 0.6f; // 캐릭터 머리 위쪽에 위치

        text.text = damage.ToString();
        text.color = fadeColor;
        text.transform.localPosition = new Vector3(0, -0.3f, 0);

        transform.DOKill();
        text.transform.DOKill();

        // 월드 좌표 기준으로 위로 부드럽게 이동하면서 페이드 인
        text.DOFade(1f, speed).SetId(this);
        text.transform.DOLocalMoveY(0, 0.5f).SetId(this); // 월드 Y축 기준으로 올라감

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