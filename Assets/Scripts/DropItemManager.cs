using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    public static DropItemManager Instance;

    private ObjectPool<DropItem> pool;
    [SerializeField] private DropItem dropItemPrefab;

    private void Awake()
    {
        Instance = this;
        pool = new ObjectPool<DropItem>(dropItemPrefab, 50, transform);
    }

    //public void 
}
