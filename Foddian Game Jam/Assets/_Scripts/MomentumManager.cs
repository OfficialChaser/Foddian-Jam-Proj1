using UnityEngine;
using UnityEngine.UI;

public class MomentumManager : MonoBehaviour
{
    public static MomentumManager Instance;
    // References
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Slider momentumBar;

    [SerializeField] private float maxMomentum;
    public float currentMomentum { get; private set; }

    [SerializeField] private float momentumIncrease;

    private void Awake()
    {
        currentMomentum = 0f;
        momentumBar.value = 0f;

        // Basic Singleton
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        currentMomentum -= Time.deltaTime;

        CheckMomentumStatus();
        UpdateMomentumBar();
    }

    public void ModifyMomentum(float change)
    {
        currentMomentum += change;
    }

    private void CheckMomentumStatus()
    {
        if (currentMomentum < 0)
        {
            currentMomentum = 0;
        }
        player.CalculateMovementSpeed(this);
    }

    private void UpdateMomentumBar()
    {
        momentumBar.value = currentMomentum / maxMomentum;
    }
}
