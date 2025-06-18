using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    public static DropItemManager Instance;

    private ObjectPool<DropItem> pool;
    public ObjectPool<DropItem> Pool => pool;
    [SerializeField] private DropItem dropItemPrefab;

    private void Awake()
    {
        Instance = this;
        pool = new ObjectPool<DropItem>(dropItemPrefab, 50, transform);
    }

    public void DropDiamonds(int value, Vector2 origin)
    {
        DropItem Dia = pool.Get();
        Dia.transform.position = origin;
        Dia.Drop(value, origin, ItemType.Diamond);
    }

    public void DropCoins(int value, Vector2 origin)
    {
        DropItem coin = pool.Get();
        coin.transform.position = origin;
        coin.Drop(value, origin, ItemType.Coin);
    }
}
