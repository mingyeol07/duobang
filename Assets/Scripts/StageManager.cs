using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    private ObjectPool<Monster> monsterPool;
    public ObjectPool<Monster> MonsterPool => monsterPool;

    [SerializeField] private Monster monsterBasePrefab;

    private int curStage = 0;
    private const float spawnPosMinDistance = 1;
    private const float spawnStartX = 7;
    private float spawnPosX;

    [SerializeField] private MonsterData monsterDataInStage1_50;
    [SerializeField] private MonsterData monsterDataInStage51_100;
    [SerializeField] private MonsterData monsterDataInStage101_150;
    [SerializeField] private MonsterData monsterDataInStage151_200;

    private void Awake()
    {
        Instance = this;

        monsterPool = new ObjectPool<Monster>(monsterBasePrefab, 30, transform);
    }

    private void Start()
    {
        ClearStage();
    }

    public void InitStage(int stageNumber)
    {
        curStage = stageNumber;
    }

    // �÷��̾ ���͸� �� ����� �ؽ�Ʈ ���������� �ҷ���
    public void ClearStage()
    {
        if (curStage <= 200) curStage++;

        // UI�� ������ٰ� ���������� �ҷ����� �ٽ� �Ͼ���
        SaveStage();

        SpawnMonsters();
    }

    // �����߾��µ� ��� ������ ������ �ٷ� ������ �� ������������ �̵�
    public void ChallangeBoss()
    {

    }
    private void SpawnMonsters()
    {
        MonsterData stageMonsterData = null;
        if (curStage <= 50)
        {
            stageMonsterData = monsterDataInStage1_50;
        }
        else if (curStage <= 100)
        {
            stageMonsterData = monsterDataInStage51_100;
        }
        else if (curStage <= 150)
        {
            stageMonsterData = monsterDataInStage101_150;
        }
        else if (curStage <= 200)
        {
            stageMonsterData = monsterDataInStage151_200;
        }

        spawnPosX = spawnStartX;

        for (int i = 0; i < 15; i++)
        {
            SpawnNormal(stageMonsterData);
        }
        if (curStage % 5 == 0)
        {
            SpawnBoss(stageMonsterData);
        }
    }

    // ���������� ���� ���� ����
    private void SpawnBoss(MonsterData stageMonsterData)
    {
        Sprite sprite = stageMonsterData.Sprite;
        RuntimeAnimatorController animator = stageMonsterData.Animator;

        int hp = (stageMonsterData.Hp + curStage * stageMonsterData.HpMultipleAmount) * 5;
        int power = (stageMonsterData.Power + curStage * stageMonsterData.PowerMultipleAmount) * 2;
        int reward = (curStage * stageMonsterData.RewardAmount) * 5;

        spawnPosX = 13;

        MonsterType type = MonsterType.Boss;

        Monster monster = monsterPool.Get();
        monster.Setup(
            stageMonsterData.Sprite,
            stageMonsterData.Animator,
            hp,
            power,
            reward,
            type,
            spawnPosX
        );
    }

    private void SpawnNormal(MonsterData stageMonsterData)
    {
        Sprite sprite = stageMonsterData.Sprite;
        RuntimeAnimatorController animator = stageMonsterData.Animator;

        int hp = stageMonsterData.Hp + curStage * stageMonsterData.HpMultipleAmount;
        int power = stageMonsterData.Power + curStage * stageMonsterData.PowerMultipleAmount;
        int reward = curStage * stageMonsterData.RewardAmount;

        // ��ġ Ȯ�� �� ���� ����
        spawnPosX = Random.Range(spawnStartX, 30f);

        MonsterType type = MonsterType.Normal;

        Monster monster = monsterPool.Get();
        monster.Setup(
            stageMonsterData.Sprite,
            stageMonsterData.Animator,
            hp,
            power,
            reward,
            type,
            spawnPosX
        );
    }

    private void SpawnBoss()
    {

    }
    
    private void SaveStage()
    {

    }

    private void DrawBackground()
    {
        // ��� �ҷ�����
    }
}
