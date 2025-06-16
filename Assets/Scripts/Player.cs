using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class Player : Unit
{
    private int criticalPer = 5;
    private int criticalPower = 50;
    private float moveSpeed = 1.0f;

    private const int hpAmount = 100;
    private const int powerAmount = 100;
    private const int criticalPerAmount = 1;
    private const int criticalPowerAmount = 5;
    private const float moveSpeedAmount = 0.2f;

    private float attackWaitTime = 1f;

    private Monster targetMonster;

    private readonly int hashAttack2 = Animator.StringToHash("Attack2");
    private readonly int hashIdle = Animator.StringToHash("Idle");

    // ������ ������ ��
    private bool isAttacked = false;
    private bool isAttackedFirst = false;
    private bool isDead = false;

    protected override void Awake()
    {
        base.Awake();
        hp = 500;
        power = 50;
    }

    private void Update()
    {
        if (isDead) return;

        if (targetMonster == null)
        {
            Move();
        }
        else
        {
            if (!isAttacked)
            {
                isAttacked = true;
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
        if(hit)
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

    // �ִϸ��̼� Ʈ����
    private void AttackAnimEvent()
    {
        if (targetMonster == null) return;

        int ran = Random.Range(0, 100);

        if(ran < criticalPer)
        {
            targetMonster.Hit(power + power * (criticalPower / 100), true);
        }
        else
        {
            targetMonster.Hit(power, false);
        }
        
        // ���� ������ �� �� ����
        if (targetMonster == null) return;

        // ù�� ° ������ �ϰ� �� �ڶ��
        if (isAttackedFirst)
        {
            isAttackedFirst = false;
            animator.SetTrigger(hashAttack2);
        }
        else
        { 
            // �ι� ° ������ �ϰ� �ٽ� ����
            StartCoroutine(AttackReCycle());
        }
    }

    protected override void Die()
    {
        isDead = true;
        ResetTarget();
        // �ڷ� �з����鼭 ����
        animator.SetTrigger(hashDeath);
        transform.DOMoveX(transform.position.x - 2, 0.5f).SetEase(Ease.InOutQuad);
    }

    public void ResetTarget()
    {
        isAttacked = false;
        targetMonster = null;
    }

    public void UpgradeHp()
    {
        hp += hpAmount;
    }

    public void UpgradePower()
    {
        power += powerAmount;
    }

    public void UpgradeMoveSpeed()
    {
        moveSpeed += moveSpeedAmount;
    }

    public void UpgradeCriticalPer()
    {
        criticalPer += criticalPerAmount;
    }

    public void UpgradeCriticalPower()
    {
        criticalPower += criticalPowerAmount;
    }
}
