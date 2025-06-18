using DG.Tweening;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Unit : MonoBehaviour
{
    protected int hp;
    protected int maxHp;

    public int Hp => hp;
    public int MaxHp => maxHp;
    public float HpPercent => (float)hp / (float)maxHp;

    protected int power;

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected readonly int hashAttack = Animator.StringToHash("Attack");
    protected readonly int hashDeath = Animator.StringToHash("Death");
    protected readonly int hashRespawn = Animator.StringToHash("Respawn");

    protected const string glowBlend = "_HitEffectBlend";
    protected const string hitEffectColor = "_HitEffectColor";

    protected const string outLineColor = "_OutlineColor";
    protected const string outLineWidth = "_OutlinePixelWidth";

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void SetOutLineWidth(int value)
    {
        spriteRenderer.material.SetFloat(outLineWidth, value);
    }

    public void SetOutLineColor(Color color)
    {
        spriteRenderer.material.SetColor(outLineColor, color);
    }

    public virtual void Hit(int damage, bool isCritical = false)
    {
        Camera.main.GetComponent<MoveCamera>().Shake(0.1f, 0.1f);
        UIManager.Instance.SpawnDamageText(this, damage, isCritical);
        // UIManager.In 

        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
        else
        {
            spriteRenderer.material.SetFloat(glowBlend, 1);
            spriteRenderer.material.DOColor(Color.red, hitEffectColor, 0.2f).OnComplete(() => 
            {
                spriteRenderer.material.SetFloat(glowBlend, 0.06f);
                spriteRenderer.material.DOFloat(0, glowBlend, 0.2f).OnComplete(() =>
                {
                    spriteRenderer.material.SetColor(hitEffectColor, Color.white);
                });
            });
        }
    }

    protected abstract void Die();
}
