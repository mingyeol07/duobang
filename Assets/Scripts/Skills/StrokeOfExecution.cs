using UnityEngine;

public class StrokeOfExecution : SkillBase
{
    public StrokeOfExecution(float maxCool, Player player) : base(maxCool, player)
    {
    }

    public override void Action()
    {
        curCool = 0;

        int damage;
        bool isCri = false;

        float rand = Random.Range(0, 100);
        if(rand < player.CriticalPer)
        {
            damage = player.GetCriticalDamage();
            isCri = true;
        }
        else
        {
            damage = player.GetDamage();
        }

        SkillEffectManager.Instance.ShowRedDamageEffect((int)(damage * damageCoefficient), isCri);
    }
}
