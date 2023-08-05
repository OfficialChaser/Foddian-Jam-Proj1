using UnityEngine;
using UnityEngine.UI;

public class MomentumManager : MonoBehaviour
{
    public static MomentumManager Instance;
    // References
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Slider horizontalMomentumBar;
    [SerializeField] private Slider jumpMomentumBar;

    public float currentHorizontalMomentum { get; private set; }
    [SerializeField] private float maxHorizontalMomentum;

    public float currentJumpMomentum { get; private set; }
    [SerializeField] private float maxJumpMomentum;

    private void Awake()
    {
        currentHorizontalMomentum = 0f;
        currentJumpMomentum = 0f;
        horizontalMomentumBar.value = 0f;
        jumpMomentumBar.value = 0f;

        // Basic Singleton
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        currentHorizontalMomentum -= Time.deltaTime;
        currentJumpMomentum -= 2 * Time.deltaTime;

        CheckMomentumStatus();
        UpdateMomentumBar();
    }

    public void ModifyHorizontalMomentum(float change)
    {
        currentHorizontalMomentum += change;
    }

    public void ModifyJumpForce(float change)
    {
        currentJumpMomentum += change;
    }

    private void CheckMomentumStatus()
    {
        // Horizontal
        if (currentHorizontalMomentum < 0)
        {
            currentHorizontalMomentum = 0;
        }
        else if (currentHorizontalMomentum > maxHorizontalMomentum)
        {
            currentHorizontalMomentum = maxHorizontalMomentum;
        }

        if (currentJumpMomentum < 0)
        {
            currentJumpMomentum = 0;
        }
        else if (currentJumpMomentum > maxHorizontalMomentum)
        {
            currentJumpMomentum = maxJumpMomentum;
        }

        player.CalculateMovementSpeed();
        player.CalculateJumpForce();
    }

    private void UpdateMomentumBar()
    {
        horizontalMomentumBar.value = currentHorizontalMomentum / maxHorizontalMomentum;
        jumpMomentumBar.value = currentJumpMomentum / maxJumpMomentum;
    }
}
