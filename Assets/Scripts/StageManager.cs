using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Overlays;
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
    private List<float> spawnPositions = new List<float>();

    [SerializeField] private MonsterData monsterDataInStage1_50;
    [SerializeField] private MonsterData monsterDataInStage51_100;
    [SerializeField] private MonsterData monsterDataInStage101_150;
    [SerializeField] private MonsterData monsterDataInStage151_200;

    private Queue<Monster> spawnedMonsters = new Queue<Monster>();

    private const int initMonsterLength = 20;

    private void Awake()
    {
        Instance = this;

        monsterPool = new ObjectPool<Monster>(monsterBasePrefab, initMonsterLength, transform);
    }

    private void Start()
    {
        LoadStage();
    }

    public void InitStage(int stageNumber)
    {
        curStage = stageNumber;
    }

    // �÷��̾ ���͸� �� ����� �ؽ�Ʈ ���������� �ҷ���
    public void TryClearStage()
    {
        if (monsterPool.Pool.Count < initMonsterLength) return;

        if (curStage <= 200) curStage++;
 
        LoadStage();
    }

    public void LoadStage()
    {
        UIManager.Instance.Fade(() =>
        {
            PlayerManager.Instance.Player.Respawn(-3f);
            SpawnMonsters();
        });
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

        spawnPositions.Clear();
        int count = 15;
        float minX = spawnStartX;
        float maxX = 30f;
        float step = (maxX - minX) / count;

        for (int i = 0; i < count; i++)
        {
            float baseX = minX + step * i;
            float jitter = Random.Range(-step / 3f, step / 3f); // �ణ�� ������
            float finalX = Mathf.Clamp(baseX + jitter, minX, maxX);
            spawnPositions.Add(finalX);
        }

        // ����Ʈ�� ����
        spawnPositions = spawnPositions.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < count; i++)
        {
            SpawnNormal(stageMonsterData, spawnPositions[i]);
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

        MonsterType type = MonsterType.Boss;

        Monster monster = monsterPool.Get();
        monster.Setup(
            stageMonsterData.Sprite,
            stageMonsterData.Animator,
            hp,
            power,
            reward,
            type,
            13
        );
    }

    private void SpawnNormal(MonsterData stageMonsterData, float spawnPosX)
    {
        Sprite sprite = stageMonsterData.Sprite;
        RuntimeAnimatorController animator = stageMonsterData.Animator;

        int hp = stageMonsterData.Hp + curStage * stageMonsterData.HpMultipleAmount;
        int power = stageMonsterData.Power + curStage * stageMonsterData.PowerMultipleAmount;
        int reward = curStage * stageMonsterData.RewardAmount;

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

    private void SaveStage()
    {

    }
}
