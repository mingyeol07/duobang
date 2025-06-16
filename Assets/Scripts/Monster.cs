using DG.Tweening;
using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public enum MonsterType
{
    Normal,
    Boss
}

public class Monster : Unit
{
    private int reward;
    private float attackDistance = 1.5f;

    private WaitForSeconds attackWaitTime = new WaitForSeconds(2);

    private bool isHited;
    private MonsterType type;

    private bool isDead;

    private const float knockBackSpeed = 0.5f;
    private const float deathWaitTime = 2;

    private BoxCollider2D boxCollider;

    protected override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Setup(Sprite sprite, RuntimeAnimatorController animator, int hp, int power, int reward, MonsterType type, float spawnPosX)
    {
        this.hp = hp;
        this.power = power;
        this.reward = reward;

        this.animator.runtimeAnimatorController = animator;
        this.type = type;

        spriteRenderer.sprite = sprite;

        transform.position = new Vector2(spawnPosX, transform.position.y);
    }

    public override void Hit(int damage, bool isCritical)
    {
        if (!isHited)
        {
            isHited = true;
            StartCoroutine(Co_AttackCycle());
        }
        base.Hit(damage, isCritical);
    }

    private IEnumerator Co_AttackCycle()
    {
        while (true)
        {
            yield return attackWaitTime;

            if (!isHited)
            {
                yield break;
            }

            animator.SetTrigger(hashAttack);
        }
    }

    // 애니메이션트리거
    private void AttackAnimEvent()
    {
        if(transform.position.x - PlayerManager.Instance.Player.transform.position.x < attackDistance)
        {
            PlayerManager.Instance.Player.Hit(power);
        }
    }

    protected override void Die()
    {
        // 풀로 돌아가고 다이아 획득
        isDead = true;
        isHited = false;
        boxCollider.enabled = false;
        PlayerManager.Instance.Player.ResetTarget();

        if (type == MonsterType.Boss)
        {
            // 스테이지에게 보스 클리어를 알림
            // 다이아 획득
        }
        else
        {
            // 골드 획득
        }

        // 밀려나면서 죽음
        transform.DOMoveX(transform.position.x + 2, knockBackSpeed).SetEase(Ease.OutQuad);

        animator.SetTrigger(hashDeath);

        spriteRenderer.material.SetFloat(glowBlend, 1);
        spriteRenderer.material.SetColor(hitEffectColor, Color.red);

        spriteRenderer.material.DOFloat(0, glowBlend, deathWaitTime);
        spriteRenderer.DOFade(0, deathWaitTime);

        StartCoroutine(Co_ReturnPool());
    }

    IEnumerator Co_ReturnPool()
    {
        yield return new WaitForSeconds(deathWaitTime);

        StageManager.Instance.MonsterPool.Return(this);

        // 다시 생성될 때를 위한 후처리
        isDead = false;
        boxCollider.enabled = true;
        animator.SetTrigger(hashRespawn);
        spriteRenderer.DOFade(1, 0);
        spriteRenderer.material.SetFloat(glowBlend, 0);
        spriteRenderer.material.SetColor(hitEffectColor, Color.white);
    }
}
