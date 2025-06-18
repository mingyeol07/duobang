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
        int count = 4;
        int splitValue = value / count;

        for (int i = 0; i < count; i++)
        {
            DropItem Dia = pool.Get();
            Dia.transform.position = origin;
            Dia.Drop(splitValue, origin, ItemType.Diamond);
        }
    }

    public void DropCoins(int value, Vector2 origin)
    {
        int count = 4;
        int splitValue = value / count;

        for (int i = 0; i < count; i++)
        {
            DropItem coin = pool.Get();
            coin.transform.position = origin;
            coin.Drop(splitValue, origin, ItemType.Coin);
        }
    }
}
