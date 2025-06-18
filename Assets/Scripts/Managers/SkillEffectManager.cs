using Unity.VisualScripting;
using UnityEngine;

public class SkillEffectManager : MonoBehaviour
{
    public static SkillEffectManager Instance;

    private Player player;

    [SerializeField] private DamageEffect redDamageEffect;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = PlayerManager.Instance.Player;
    }

    public void ShowRedDamageEffect(int damage, bool isCritical)
    {
        float playerX = player.transform.position.x + 2;

        redDamageEffect.Play(damage, isCritical);
        redDamageEffect.transform.position = new Vector2(playerX, redDamageEffect.transform.position.y);
    }
}
