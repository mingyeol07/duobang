using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class Player : Unit
{
    private int criticalPer = 5;
    private int criticalPower = 50;
    private float moveSpeed = 1.0f;

    private float attackWaitTime = 1f;

    private Monster targetMonster;

    private readonly int hashAttack2 = Animator.StringToHash("Attack2");
    private readonly int hashIdle = Animator.StringToHash("Idle");

    // 공격을 시작할 시
    private bool isAttacked = false;
    private bool isAttackedFirst = false;
    private bool isDead = false;

    private Queue<SkillBase> skillQueue;
    private List<SkillBase> skillList;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (isDead) return;

        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i].UpdateLogic();
        }

        if (targetMonster == null)
        {
            Move();
        }
        else
        {
            if (!isAttacked)
            {
                isAttacked = true;
                // 타겟이 죽을 때까지 공격중인 상태는 끝나지 않음

                if (skillQueue.Count > 0)
                {
                    SkillBase skill = skillQueue.Dequeue();
                    skill.Action();
                }
                isAttackedFirst = true;
                animator.SetTrigger(hashAttack);
            }
        }
    }

    public void Respawn()
    {
        ResetTarget();
        isDead = false;
        animator.SetTrigger(hashRespawn);
    }

    private void Move()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);
        if (hit)
        {
            targetMonster = hit.collider.gameObject.GetComponent<Monster>();
        }
    }

    private IEnumerator AttackReCycle()
    {
        animator.SetBool(hashIdle, true);
        yield return new WaitForSeconds(attackWaitTime);
        animator.SetBool(hashIdle, false);

        if (targetMonster == null) yield break;

        isAttackedFirst = true;
        animator.SetTrigger(hashAttack);
    }

    // 애니메이션 트리거
    private void AttackAnimEvent()
    {
        if (targetMonster == null) return;

        int ran = Random.Range(0, 100);

        if (ran < criticalPer)
        {
            targetMonster.Hit(power + power * (criticalPower / 100), true);
        }
        else
        {
            targetMonster.Hit(power, false);
        }

        // 다음 공격을 할 지 결정
        if (targetMonster == null)
        {
            return;
        }

        // 첫번 째 공격을 하고 난 뒤라면
        if (isAttackedFirst)
        {
            isAttackedFirst = false;
            animator.SetTrigger(hashAttack2);
        }
        else
        {
            // 두번 째 공격을 하고 다시 돌기
            StartCoroutine(AttackReCycle());
        }
    }

    protected override void Die()
    {
        isDead = true;
        ResetTarget();
        // 뒤로 밀려나면서 죽음
        animator.SetTrigger(hashDeath);
        transform.DOMoveX(transform.position.x - 2, 0.5f).SetEase(Ease.InOutQuad);
    }

    public void ResetTarget()
    {
        isAttacked = false;
        targetMonster = null;
    }

    public void InitHp(int value)
    {
        hp = value;
    }

    public void InitPower(int value)
    {
        power = value;
    }

    public void InitMovespeed(float value)
    {
        moveSpeed = value;
    }

    public void InitCriticalPercent(int value)
    {
        criticalPer = value;
    }

    public void InitCriticalDamage(int value)
    {
        criticalPower = value;
    }
}