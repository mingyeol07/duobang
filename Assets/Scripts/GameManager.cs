using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private SaveData saveData;

    private void Awake()
    {
        Instance = this;
        saveData = JsonSaveLoader.Load();
    }

    private void Start()
    {
        UpgradeManager.Instance.InitAbilityLevel(saveData.HpLevel, saveData.PowerLevel, saveData.MovespeedLevel, saveData.CriticalPercentLevel, saveData.CriticalDamageLevel);
        UpgradeManager.Instance.InitCoinAndDiamond(saveData.HaveCoin, saveData.HaveDiamond);
        UpgradeManager.Instance.InitSkillLevel(saveData.NormalAttackLevel, saveData.Skill1Level, saveData.Skill2Level);
        StageManager.Instance.InitStage(saveData.CurrentStage);
        UIManager.Instance.InitStatAndPanel();

        JsonSaveLoader.Save(saveData);


    }
}
