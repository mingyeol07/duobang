using UnityEngine;

public class IndomitableWill : SkillBase
{
    private bool burnningFlag = false;

    public IndomitableWill(float maxCool, Player player) : base(maxCool, player)
    {
    }

    public override void UpdateLogic()
    {
        // ü�� ���¿� ���� ���ο� ���� ����
        bool currentBurning = player.HpPercent <= 0.9f;

        // ���°� �ٲ���� ���� ���� ����
        if (burnningFlag != currentBurning)
        {
            burnningFlag = currentBurning;

            if (burnningFlag)
            {
                player.InitSkill2DamagePercent((int)damageCoefficient);
                player.BurnningEffect.SetActive(true);
                player.SetOutLineColor(new Color(1f, 0.5f, 0f)); // Color ���� 0~1 ����
                player.SetOutLineWidth(1);
            }
            else
            {
                player.InitSkill2DamagePercent(0);
                player.BurnningEffect.SetActive(false);
                player.SetOutLineColor(Color.white);
                player.SetOutLineWidth(0);
            }
        }
    }

    public override void Action()
    {
        
    }
}
