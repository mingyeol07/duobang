using DG.Tweening;
using UnityEngine;

public enum ItemType
{
    Coin, Diamond, Potion
}

public class DropItem : MonoBehaviour
{
    private ItemType type;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite coinSprite;
    [SerializeField] private Sprite DiaSprite;
    private int value;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Drop(int value, Vector2 startPos, ItemType type)
    {
        if (type == ItemType.Coin)
        {
            spriteRenderer.sprite = coinSprite;
        }
        else if (type == ItemType.Diamond)
        {
            spriteRenderer.sprite = DiaSprite;
        }

        this.type = type;
        this.value = value;

        transform.position = startPos;

        float xOffset = Random.Range(1.5f, 2.5f);
        float yOffset = Random.Range(1.6f, 2.2f);

        Vector2 midPos = new Vector2(startPos.x + xOffset / 2f, yOffset);
        Vector2 endPos = new Vector2(startPos.x + xOffset, 1.2f); // ¹Ù´Ú y = 0

        Vector3[] path = new Vector3[] { startPos, midPos, endPos };

        transform.DOPath(path, 0.6f, PathType.CatmullRom)
                 .SetEase(Ease.InOutSine)
                 .SetOptions(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Color textColor = Color.white;

        if(type == ItemType.Coin)
        {
            UpgradeManager.Instance.GetCoins(value);
            textColor = Color.yellow;
        }
        else if(type == ItemType.Diamond)
        {
            UpgradeManager.Instance.GetDiamonds(value);
            textColor = Color.cyan;
        }

        DropItemManager.Instance.Pool.Return(this);
        UIManager.Instance.SpawnGetText(transform.position, value, textColor);
    }
}