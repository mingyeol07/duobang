using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Object/Monster Data")]
public class MonsterData : ScriptableObject
{
    [SerializeField] private int hp;
    [SerializeField] private int power;
    [SerializeField] private int rewardAmount;
    [SerializeField] private MonsterType type;

    [SerializeField] private Sprite sprite;
    [SerializeField] private RuntimeAnimatorController animator;

    [SerializeField] private int hpMultipleAmount;
    [SerializeField] private int powerMultipleAmount;

    public int Hp => hp;
    public int Power => power;
    public int RewardAmount => rewardAmount;
    public MonsterType Type => type;

    public Sprite Sprite => sprite;
    public RuntimeAnimatorController Animator => animator;

    public int HpMultipleAmount => hpMultipleAmount;
    public int PowerMultipleAmount => powerMultipleAmount;
}