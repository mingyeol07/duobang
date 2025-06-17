using UnityEngine;

public class IndomitableWill : SkillBase
{
    private bool burnningFlag = false;

    public IndomitableWill(float maxCool, Player player) : base(maxCool, player)
    {
    }

    public override void UpdateLogic()
    {
        // 체력 상태에 따라 새로운 상태 결정
        bool currentBurning = player.HpPercent <= 0.9f;

        // 상태가 바뀌었을 때만 로직 실행
        if (burnningFlag != currentBurning)
        {
            burnningFlag = currentBurning;

            if (burnningFlag)
            {
                player.InitSkill2DamagePercent((int)damageCoefficient);
                player.BurnningEffect.SetActive(true);
                player.SetOutLineColor(new Color(1f, 0.5f, 0f)); // Color 값은 0~1 범위
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
