using NUnit.Framework.Constraints;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    private int haveCoin;
    private int haveDiamond;

    private int powerLevel;
    private int hpLevel;
    private int movespeedLevel;
    private int criticalPercentLevel;
    private int criticalDamageLevel;

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

    private int normalAttackLevel;
    private int skill1Level;
    private int skill2Level;

    private const int NormalAttackMaxLevel = 50;
    private const int Skill1MaxLevel = 30;
    private const int Skill2MaxLevel = 25;

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
        criticalPercentPanel.Setup(criticalPercent, criticalPerAmount, criticalPerPrice, criticalPercentLevel);

        int criticalDamage = criticalPowerDefault + criticalPowerAmount * criticalDamageLevel;
        PlayerManager.Instance.Player.InitCriticalDamage(criticalDamage);

        criticalPowerPrice = criticalPowerPriceDefault + (criticalDamageLevel * criticalPowerPriceMultipleAmount);
        criticalDamagePanel.Setup(criticalDamage, criticalPowerAmount, criticalPowerPrice, criticalDamageLevel);

        UIManager.Instance.InitCoinAndDiaText(haveCoin, haveDiamond);
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
        panel.Setup(criticalPercent, criticalPerAmount, criticalPerPrice, criticalPercentLevel);

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
        panel.Setup(criticalDamage, criticalPowerAmount, criticalPowerPrice, criticalDamageLevel);

        return true;
    }
}
