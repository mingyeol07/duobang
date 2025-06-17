using NUnit.Framework.Constraints;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    private int haveCoin;
    private int haveDiamond;

    private int powerLevel =1;
    private int hpLevel = 1;
    private int movespeedLevel = 1;
    private int criticalPercentLevel = 1;
    private int criticalDamageLevel = 1;

    private const int powerMaxLevel = 100;
    private const int hpMaxLevel = 100;
    private const int movespeedMaxLevel = 50;
    private const int criticalPercentMaxLevel = 30;
    private const int criticalDamageMaxLevel = 50;

    private const int hpDefault = 500;
    private const int powerDefault = 50;
    private const int criticalPerDefault = 5;
    private const int criticalPowerDefault = 50;
    private const float moveSpeedDefault = 1.0f;

    private int hpPrice = 120;
    private int powerPrice = 100;
    private int criticalPerPrice = 300;
    private int criticalPowerPrice = 250;
    private int moveSpeedPrice = 150;

    private const int hpPriceDefault = 120;
    private const int powerPriceDefault = 100;
    private const int criticalPerPriceDefault = 300;
    private const int criticalPowerPriceDefault = 250;
    private const int moveSpeedPriceDefault = 150;

    private const int hpPriceMultipleAmount = 60;
    private const int powerPriceMultipleAmount = 50;
    private const int criticalPerPriceMultipleAmount = 100;
    private const int criticalPowerPriceMultipleAmount = 90;
    private const int moveSpeedPriceMultipleAmount = 70;

    private const int hpAmount = 100;
    private const int powerAmount = 100;
    private const int criticalPerAmount = 1;
    private const int criticalPowerAmount = 5;
    private const float moveSpeedAmount = 0.2f;

    private int normalAttackLevel = 1;
    private int skill1Level = 0;
    private int skill2Level = 0;

    private const int NormalAttackMaxLevel = 50;
    private const int Skill1MaxLevel = 30;
    private const int Skill2MaxLevel = 25;

    private int normalAttackPrice = 2;
    private int skill1Price = 10;
    private int skill2Price = 15;

    private const int normalAttackPriceMultipleAmount = 2;
    private const int skill1PriceMultipleAmount = 3;
    private const int skill2PriceMultipleAmount = 4;

    private const float normalAttackDefaultAmount = 1.0f;
    private const float skill1DefaultAmount = 5.0f;
    private const int skill2DefaultAmount = 50;

    private float normalAttackAmount = 0.1f;
    private float skill1Amount = 0.5f;
    private int skill2Amount = 2;

    private void Awake()
    {
        Instance = this;
    }

    public void InitCoinAndDiamond(int coins, int diamonds)
    {
        haveCoin = coins;
        haveDiamond = diamonds;
    }

    public void InitAbilityLevel(int hpLv, int powerLv, int movespeedLv, int criPerLv, int criDamLv)
    {
        hpLevel = hpLv;
        powerLevel = powerLv;
        movespeedLevel = movespeedLv;
        criticalPercentLevel = criPerLv;
        criticalDamageLevel = criDamLv;
    }

    public void InitSkillLevel(int normalAttackLv, int skill1Lv, int skill2Lv)
    {
        normalAttackLevel = normalAttackLv;
        skill1Level = skill1Lv;
        skill2Level = skill2Lv;
    }

    public void InitPlayerStatAndUI(AbilityPanel hpPanel, AbilityPanel powerPanel, AbilityPanel moveSpeedPanel, AbilityPanel criticalPercentPanel, AbilityPanel criticalDamagePanel)
    {
        int hp = hpDefault + hpAmount * hpLevel;
        PlayerManager.Instance.Player.InitHp(hp);

        hpPrice = hpPriceDefault + (hpLevel * hpPriceMultipleAmount);
        hpPanel.Setup(hp, hpAmount, hpPrice, hpLevel);

        int power = powerDefault + powerAmount * powerLevel;
        PlayerManager.Instance.Player.InitPower(power);

        powerPrice = powerPriceDefault + (powerLevel * powerPriceMultipleAmount);
        powerPanel.Setup(power, powerAmount, powerPrice, powerLevel);

        float moveSpeed = moveSpeedDefault + moveSpeedAmount * movespeedLevel;
        PlayerManager.Instance.Player.InitMovespeed(moveSpeed);

        moveSpeedPrice = moveSpeedPriceDefault + (movespeedLevel * moveSpeedPriceMultipleAmount);
        moveSpeedPanel.Setup(moveSpeed, moveSpeedAmount, moveSpeedPrice, movespeedLevel);

        int criticalPercent = criticalPerDefault + criticalPerAmount * criticalPercentLevel;
        PlayerManager.Instance.Player.InitCriticalPercent(criticalPercent);

        criticalPerPrice = criticalPerPriceDefault + (criticalPercentLevel * criticalPerPriceMultipleAmount);
        criticalPercentPanel.Setup(criticalPercent, criticalPerAmount, criticalPerPrice, criticalPercentLevel, "%");

        int criticalDamage = criticalPowerDefault + criticalPowerAmount * criticalDamageLevel;
        PlayerManager.Instance.Player.InitCriticalDamage(criticalDamage);

        criticalPowerPrice = criticalPowerPriceDefault + (criticalDamageLevel * criticalPowerPriceMultipleAmount);
        criticalDamagePanel.Setup(criticalDamage, criticalPowerAmount, criticalPowerPrice, criticalDamageLevel, "%");

        UIManager.Instance.InitCoinAndDiaText(haveCoin, haveDiamond);
    }

    public void InitPlayerSkill(AbilityPanel normalAttack, AbilityPanel skill1, AbilityPanel skill2)
    {
        float NormalAttackCoefficient = normalAttackDefaultAmount + normalAttackAmount * (normalAttackLevel - 1);
        PlayerManager.Instance.Player.InitNormalAttack(NormalAttackCoefficient);

        normalAttackPrice = normalAttackLevel * normalAttackPriceMultipleAmount;
        normalAttack.Setup(NormalAttackCoefficient, normalAttackAmount, normalAttackPrice, normalAttackLevel, "X");

        if(skill1Level != 0)
        {
            float skill1Coefficient = skill1DefaultAmount + skill1Amount * (skill1Level - 1);
            PlayerManager.Instance.Player.InitSkill1(skill1Coefficient);

            skill1Price = skill1Level * skill1PriceMultipleAmount;
            skill1.Setup(skill1Coefficient, skill1Amount, skill1Price, skill1Level, "X");
        }
        else
        {
            skill1.Setup(0, 5.0f, 10, skill1Level, "X");
        }

        if (skill2Level != 0)
        {
            float skill2Coefficient = skill2DefaultAmount + skill2Amount * (skill2Level - 1);
            PlayerManager.Instance.Player.InitSkill2(skill2Coefficient);

            skill2Price = skill2Level * skill2PriceMultipleAmount;
            skill2.Setup(skill2Coefficient, skill2Amount, skill2Price, skill2Level, "X");
        }
        else
        {
            skill2.Setup(0, 50.0f, 15, skill2Level, "X");
        }
    }

    public bool TryUpgradeNormalAttack(AbilityPanel panel)
    {
        if (normalAttackLevel >= NormalAttackMaxLevel || haveDiamond < normalAttackPrice) return false;

        normalAttackLevel++;
        haveDiamond -= normalAttackPrice;
        UIManager.Instance.InitCoinAndDiaText(haveCoin, haveDiamond);

        float NormalAttackCoefficient = normalAttackDefaultAmount + normalAttackAmount * (normalAttackLevel - 1);
        PlayerManager.Instance.Player.InitNormalAttack(NormalAttackCoefficient);

        normalAttackPrice = normalAttackLevel * normalAttackPriceMultipleAmount;
        panel.Setup(NormalAttackCoefficient, normalAttackAmount, normalAttackPrice, normalAttackLevel, "X");

        return true;
    }

    public bool TryUpgradeSkill1(AbilityPanel panel)
    {
        if (skill1Level >= Skill1MaxLevel || haveDiamond < skill1Price) return false;

        skill1Level++;
        haveDiamond -= skill1Price;
        UIManager.Instance.InitCoinAndDiaText(haveCoin, haveDiamond);

        // 레벨이 1이면 기본값
        float skill1Coefficient = skill1DefaultAmount + skill1Amount * (skill1Level - 1);
        PlayerManager.Instance.Player.InitSkill1(skill1Coefficient);

        skill1Price = skill1Level * skill1PriceMultipleAmount;
        panel.Setup(skill1Coefficient, skill1Amount, skill1Price, skill1Level, "X");

        return true;
    }

    public bool TryUpgradeSkill2(AbilityPanel panel)
    {
        if (skill2Level >= Skill2MaxLevel || haveDiamond < skill2Price) return false;

        skill2Level++;
        haveDiamond -= skill2Price;
        UIManager.Instance.InitCoinAndDiaText(haveCoin, haveDiamond);

        // 레벨이 1이면 기본값
        float skill2Coefficient = skill2DefaultAmount + skill2Amount * (skill2Level - 1);
        PlayerManager.Instance.Player.InitSkill2(skill2Coefficient);

        skill2Price = skill2Level * skill2PriceMultipleAmount;
        panel.Setup(skill2Coefficient, skill2Amount, skill2Price, skill2Level, "X");

        return true;
    }

    public bool TryUpgradeHp(AbilityPanel panel)
    {
        if (hpLevel >= hpMaxLevel || haveCoin < hpPrice) return false;

        hpLevel++;
        haveCoin -= hpPrice;

        int hp = hpDefault + hpAmount * hpLevel;
        PlayerManager.Instance.Player.InitHp(hp);
        UIManager.Instance.InitCoinAndDiaText(haveCoin, haveDiamond);

        hpPrice = hpPriceDefault + (hpLevel * hpPriceMultipleAmount);
        panel.Setup(hp, hpAmount, hpPrice, hpLevel);

        return true;
    }

    public bool TryUpgradePower(AbilityPanel panel)
    {
        if (powerLevel >= powerMaxLevel || haveCoin < powerPrice) return false;

        powerLevel++;
        haveCoin -= powerPrice;

        int power = powerDefault + powerAmount * powerLevel;
        PlayerManager.Instance.Player.InitPower(power);
        UIManager.Instance.InitCoinAndDiaText(haveCoin, haveDiamond);

        powerPrice = powerPriceDefault + (powerLevel * powerPriceMultipleAmount);
        panel.Setup(power, powerAmount, powerPrice, powerLevel);

        return true;
    }

    public bool TryUpgradeMovespeed(AbilityPanel panel)
    {
        if (movespeedLevel >= movespeedMaxLevel || haveCoin < moveSpeedPrice) return false;

        movespeedLevel++;
        haveCoin -= moveSpeedPrice;

        float moveSpeed = moveSpeedDefault + moveSpeedAmount * movespeedLevel;
        PlayerManager.Instance.Player.InitMovespeed(moveSpeed);
        UIManager.Instance.InitCoinAndDiaText(haveCoin, haveDiamond);

        moveSpeedPrice = moveSpeedPriceDefault + (movespeedLevel * moveSpeedPriceMultipleAmount);
        panel.Setup(moveSpeed, moveSpeedAmount, moveSpeedPrice, movespeedLevel);

        return true;
    }

    public bool TryUpgradeCriticalPercent(AbilityPanel panel)
    {
        if (criticalPercentLevel >= criticalPercentMaxLevel || haveCoin < criticalPerPrice) return false;

        criticalPercentLevel++;
        haveCoin -= criticalPerPrice;

        int criticalPercent = criticalPerDefault + criticalPerAmount * criticalPercentLevel;
        PlayerManager.Instance.Player.InitCriticalPercent(criticalPercent);
        UIManager.Instance.InitCoinAndDiaText(haveCoin, haveDiamond);

        criticalPerPrice = criticalPerPriceDefault + (criticalPercentLevel * criticalPerPriceMultipleAmount);
        panel.Setup(criticalPercent, criticalPerAmount, criticalPerPrice, criticalPercentLevel, "%");

        return true;
    }

    public bool TryUpgradeCriticalDamage(AbilityPanel panel)
    {
        if (criticalDamageLevel >= criticalDamageMaxLevel || haveCoin < criticalPowerPrice) return false;

        criticalDamageLevel++;
        haveCoin -= criticalPowerPrice;

        int criticalDamage = criticalPowerDefault + criticalPowerAmount * criticalDamageLevel;
        PlayerManager.Instance.Player.InitCriticalDamage(criticalDamage);
        UIManager.Instance.InitCoinAndDiaText(haveCoin, haveDiamond);

        criticalPowerPrice = criticalPowerPriceDefault + (criticalDamageLevel * criticalPowerPriceMultipleAmount);
        panel.Setup(criticalDamage, criticalPowerAmount, criticalPowerPrice, criticalDamageLevel, "%");

        return true;
    }
}
