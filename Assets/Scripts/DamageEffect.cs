using System.Collections;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    private int damage;
    private bool isCritical;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void Play(int damageValue, bool isCritical)
    {
        animator.SetTrigger("Play");
        this.damage = damageValue;
        this.isCritical = isCritical;
    }

    private void AnimEvent()
    {
        boxCollider.enabled = true;
        StartCoroutine(Co_EffectStop());
    }

    private IEnumerator Co_EffectStop()
    {
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Monster>().Hit(damage, isCritical);
    }
}
