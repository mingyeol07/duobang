using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private Player player;
    public Player Player => player;

    private void Awake()
    {
        Instance = this;
    }
}
