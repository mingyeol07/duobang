using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class Player : Unit
{
    private int criticalPer = 5;
    public int CriticalPer => criticalPer;
    private int criticalPower = 50;
    private float moveSpeed = 1.0f;
    private float normalAttackCoefficient = 1.0f;

    private int skill2PlusDamagePercent;

    private float attackWaitTime = 1f;

    private Monster targetMonster;

    private readonly int hashAttack2 = Animator.StringToHash("Attack2");
    private readonly int hashIdle = Animator.StringToHash("Idle");

    // ������ ������ ��
    private bool isAttacked = false;
    private bool isDead = false;
    private bool isComboAttacked = false;
    private bool isAttackAnimPlaying = false;

    private Queue<SkillBase> skillQueue = new();
    private List<SkillBase> skillList = new List<SkillBase>();

    private StrokeOfExecution strokeOfExecution;
    private IndomitableWill indomitableWill;

    private bool isAttackReady = false;
    private const float attackCool = 1;
    private float coolTime = 0;

    private RaycastHit2D hit;

    [SerializeField] private GameObject burnningEffect;
    public GameObject BurnningEffect => burnningEffect;

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

        hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);

        if (targetMonster == null)
        {
            if(!isAttackAnimPlaying) Move();
        }
        else
        {
            if (!isAttackReady)
            {
                if (coolTime > attackCool)
                {
                    isAttackReady = true;
                }
                else
                {
                    coolTime += Time.deltaTime;
                }
            }
            else if(!isAttacked)
            {
                isAttacked = true;

                Attack();
            }
        }
    }

    // ��Ÿ���� ���� �غ�� ��ų�� �޾� ť�� �־� ���
    public void AddSkillAtQueue(SkillBase skill)
    {
        skillQueue.Enqueue(skill);
    }

    public void Respawn(float playStartX)
    {
        ResetTarget();
        transform.position = new Vector2(playStartX, 2.4f);

        if(isDead) animator.SetTrigger(hashRespawn);
        isDead = false;
        hp = maxHp;
        for (int i = 0; i < skillList.Count; i++)
        {
            AddSkillAtQueue(skillList[i]);
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        if (hit)
        {
            isAttacked = false;
            isComboAttacked = false;
            isAttackReady = true;
            targetMonster = hit.collider.gameObject.GetComponent<Monster>();

            while(skillQueue.Count > 0)
            {
                skillQueue.Dequeue().Action();
            }
        }
    }

    private void Attack()
    {
        isAttackAnimPlaying = true;
        animator.SetBool(hashIdle, true);

        if (!isComboAttacked)
        {
            //SoundManager.Instance.PlaySFX(SoundName.Sword1);
            animator.SetTrigger(hashAttack);
        }
        else
        {
            //SoundManager.Instance.PlaySFX(SoundName.Sword2);
            animator.SetTrigger(hashAttack2);
        }
    }

    // �ִϸ��̼� Ʈ����
    private void AttackAnimEvent()
    {
        if (targetMonster == null) return;

        int ran = Random.Range(0, 100);

        if (ran < criticalPer)
        {
            targetMonster.Hit(GetCriticalDamage(), true);
        }
        else
        {
            targetMonster.Hit(GetDamage(), false);
        }

        if (!isComboAttacked)
        {
            SoundManager.Instance.PlaySFX(SoundName.Sword1);
        }
        else
        {
            SoundManager.Instance.PlaySFX(SoundName.Sword2);
        }
    }

    public int GetCriticalDamage()
    {
        int damage = GetDamage();
        damage += (int)(damage * (criticalPower / 100f));
        return damage;
    }

    public int GetDamage()
    {
        int damage = Mathf.FloorToInt(power * normalAttackCoefficient);
        damage += (int)(damage * (skill2PlusDamagePercent / 100f));
        return damage;
    }

    // �ִϸ��̼��� ���� �� �� ���� �׾����� �˻��ؼ�
    // ���� ������ ������ �� ����
    private void AttackStopAnimEvent()
    {
        isAttackAnimPlaying = false;

        if (targetMonster == null)
        {
            coolTime = 0;
            animator.SetBool(hashIdle, false);
        }
        else
        {
            if (!isComboAttacked)
            {
                isComboAttacked = true;
                Attack();
            }
            else
            {
                coolTime = 0;
                isAttacked = false;
                isAttackReady = false;
                isComboAttacked = false;
            }
        }
    }

    public override void Hit(int damage, bool isCritical = false)
    {
        SoundManager.Instance.PlaySFX(SoundName.PlayerHit);
        base.Hit(damage, isCritical);
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
        animator.SetBool(hashIdle, false);
        targetMonster = null;
    }

    public void InitNormalAttack(float value)
    {
        normalAttackCoefficient = value;
    }

    public void InitSkill1(float value)
    {
        if(strokeOfExecution == null)
        {
            strokeOfExecution = new StrokeOfExecution(15, this);
            skillList.Add(strokeOfExecution);
            AddSkillAtQueue(strokeOfExecution);
        }

        strokeOfExecution.InitDamageCoefficient(value);
    }

    public void InitSkill2(float value)
    {
        if(indomitableWill == null)
        {
            indomitableWill = new IndomitableWill(0, this);
            skillList.Add(indomitableWill);
            AddSkillAtQueue(indomitableWill);
        }

        indomitableWill.InitDamageCoefficient(value);
    }

    public void InitSkill2DamagePercent(int value)
    {
        skill2PlusDamagePercent = value;
    }

    public void InitHp(int value)
    {
        int plus = value - maxHp;
        maxHp = value;
        // �����ȸ�ŭ ���� hp�� ����
        hp += plus;
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