using DG.Tweening;
using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using UnityEngine;

public enum MonsterType
{
    Normal,
    Boss
}

public class Monster : Unit
{
    private int reward;
    private float attackDistance = 2f;

    private WaitForSeconds attackWaitTime = new WaitForSeconds(1);

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
        this.maxHp = hp;
        this.hp = maxHp;
        this.power = power;
        this.reward = reward;

        this.animator.runtimeAnimatorController = animator;
        this.type = type;

        if (type == MonsterType.Boss)
        {
            transform.localScale = Vector3.one * 2;
            transform.position = new Vector2(transform.position.x, 2.7625f);
            SetOutLineColor(Color.red);
            SetOutLineWidth(1);
        }
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
        SoundManager.Instance.PlaySFX(SoundName.MonsterHit);
        base.Hit(damage, isCritical);
    }

    private IEnumerator Co_AttackCycle()
    {
        while (true)
        {
            yield return attackWaitTime;

            if (isDead)
            {
                yield break;
            }

            animator.SetTrigger(hashAttack);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        }
    }

    // �ִϸ��̼�Ʈ����
    private void AttackAnimEvent()
    {
        if(transform.position.x - PlayerManager.Instance.Player.transform.position.x < attackDistance)
        {
            PlayerManager.Instance.Player.Hit(power);
        }
    }

    protected override void Die()
    {
        // Ǯ�� ���ư��� ���̾� ȹ��
        isDead = true;
        isHited = false;
        boxCollider.enabled = false;
        PlayerManager.Instance.Player.ResetTarget();

        if (type == MonsterType.Boss)
        {
            // ������������ ���� Ŭ��� �˸�
            DropItemManager.Instance.DropDiamonds(reward, transform.position);
        }
        else
        {
            DropItemManager.Instance.DropCoins(reward, transform.position);
            // ��� ȹ��
        }

        // �з����鼭 ����
        transform.DOMoveX(transform.position.x + 2, knockBackSpeed).SetEase(Ease.OutQuad);

        animator.SetTrigger(hashDeath);

        spriteRenderer.material.SetFloat(glowBlend, 1);
        spriteRenderer.material.SetColor(hitEffectColor, Color.red);

        spriteRenderer.material.DOFloat(0, glowBlend, deathWaitTime);
        spriteRenderer.DOFade(0, deathWaitTime);

        StartCoroutine(Co_ReturnPool());
    }

    public void Return()
    {
        isDead = false;
        isHited = false;

        boxCollider.enabled = true;
        animator.SetTrigger(hashRespawn);
        spriteRenderer.DOFade(1, 0);
        spriteRenderer.material.SetFloat(glowBlend, 0);
        spriteRenderer.material.SetColor(hitEffectColor, Color.white);

        transform.position = new Vector2(transform.position.x, 1.9625f);
        transform.localScale = Vector3.one;
        SetOutLineWidth(0);
    }

    IEnumerator Co_ReturnPool()
    {
        yield return new WaitForSeconds(deathWaitTime);

        StageManager.Instance.MonsterPool.Return(this);
        StageManager.Instance.TryClearStage();

        // �ٽ� ������ ���� ���� ��ó��
        Return();
    }
}
