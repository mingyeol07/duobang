using UnityEngine;

public abstract class SkillBase
{
    protected float curCool = 0;
    private float maxCool;

    protected float damageCoefficient;
    protected Player player;

    public void InitDamageCoefficient(float value)
    {
        damageCoefficient = value;
    }

    public SkillBase(float maxCool, Player player)
    {
        this.maxCool = maxCool;
        this.player = player;
    }

    public virtual void UpdateLogic()
    {
        if (curCool > maxCool)
        {
            PlayerManager.Instance.Player.AddSkillAtQueue(this);
        }
        else
        {
            curCool += Time.deltaTime;
        }
    }

    public abstract void Action();
}
