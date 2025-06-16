using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private ObjectPool<DamageText> damageTextPool;
    public ObjectPool<DamageText> DamageTextPool => damageTextPool;

    [SerializeField] private DamageText damageTextPrefab;
    [SerializeField] private Transform damageTextParent;

    private Dictionary<Unit, List<DamageText>> damageTextDict;

    private void Awake()
    {
        Instance = this;
        damageTextPool = new ObjectPool<DamageText>(damageTextPrefab, 10, damageTextParent);
        damageTextDict = new Dictionary<Unit, List<DamageText>>();
    }
    
    public void SpawnDamageText(Unit unit, int damage, bool isCritical)
    {
        DamageText damageText = damageTextPool.Get();
        damageText.Show(unit.transform.position, damage, isCritical);

        // 그 전에 생성되었던 데미지는 위로 올리기
        if (damageTextDict.TryGetValue(unit, out List<DamageText> prevDamages))
        {
            for(int i =0; i < prevDamages.Count; i++)
            {
                prevDamages[i].MoveUp();
            }
            prevDamages.Add(damageText);
        }
        else
        {
            prevDamages = new List<DamageText>();
            damageTextDict.Add(unit, prevDamages);
            prevDamages.Add(damageText);
        }
    }
    // 똥
    public void ClearStage()
    {
        damageTextDict.Clear();
    }
}
