using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SaveData SaveData;

    private void Awake()
    {
        Instance = this;
        SaveData = JsonSaveLoader.Load();
    }

    private void Start()
    {
        UpgradeManager.Instance.InitAbilityLevel(SaveData.HpLevel, SaveData.PowerLevel, SaveData.MovespeedLevel, SaveData.CriticalPercentLevel, SaveData.CriticalDamageLevel);
        UpgradeManager.Instance.InitCoinAndDiamond(SaveData.HaveCoin, SaveData.HaveDiamond);
        UpgradeManager.Instance.InitSkillLevel(SaveData.NormalAttackLevel, SaveData.Skill1Level, SaveData.Skill2Level);
        StageManager.Instance.InitStage(SaveData.CurrentStage);
        UIManager.Instance.InitStatAndPanel();
    }

    private float saveDelay = 3f; // ������� 5�� �� ����
    private float lastChangeTime;
    private bool isSaveScheduled = false;

    public void OnChangedData()
    {
        MarkDirty(); // ���� �ʿ� �÷��׸� ����
    }

    private void MarkDirty()
    {
        if (!isSaveScheduled)
        {
            lastChangeTime = Time.time;
            isSaveScheduled = true;
            StartCoroutine(SaveAfterDelay());
        }
    }

    IEnumerator SaveAfterDelay()
    {
        while (Time.time - lastChangeTime < saveDelay)
            yield return null;

        StageManager.Instance.SaveStage();
        UpgradeManager.Instance.SaveData();

        Debug.Log("�����");
        JsonSaveLoader.Save(SaveData); // ��¥ ����
        isSaveScheduled = false;
    }
}
