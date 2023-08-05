using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // References
    public Rigidbody2D rb;
    [SerializeField] private PlayerSpriteRenderer playerSpriteRenderer;

    // Moving vars
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float moveSpeed;
    private float moveInput;
    private bool horizontalMovement = true;

    // Jumping vars
    [SerializeField] private float jumpForce;
    private bool canJump = true;
    private float baseJumpForce;
    
    
    [SerializeField] private float timeBtwJumps;
    private float baseTimeBtwJumps;
    private float jumpTimer;

    // State vars
    public bool isRunning { get; private set; }
    public bool isJumping { get; private set; }
    public bool isFalling { get; private set; }

    // Grounded vars
    public bool isGrounded { get; private set; }
    [SerializeField] private Transform feetPos;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;

    // Fall Clamp
    [SerializeField] private float fallClamp = -10f;

    // Animation vars
    const string PLAYER_IDLE = "Player Idle";
    const string PLAYER_RUN = "Player Run";
    const string PLAYER_JUMP = "Player Jump";
    const string PLAYER_FALL = "Player Fall";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseMoveSpeed = moveSpeed;
        baseJumpForce = jumpForce;
        baseTimeBtwJumps = timeBtwJumps;
        jumpTimer = timeBtwJumps;
    }

    void FixedUpdate()
    {
        if (horizontalMovement)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);

        CheckJumpTime();

        if (canJump && isGrounded) {
            isJumping = true;
            jumpTimer = 0f;
            isFalling = false;
            canJump = false;
            rb.velocity = Vector2.up * jumpForce;
        }
    
        if (rb.velocity.y < 0f) {
            isJumping = false;
            isFalling = true;
        }

        if (isFalling)
        {
            FallClampCheck();
        }

        UpdateStateBools();
    }

    private void CheckJumpTime()
    {
        jumpTimer += Time.deltaTime;
        if (jumpTimer > timeBtwJumps)
        {
            canJump = true;
        }
    }

    private void FallClampCheck()
    {
        if (rb.velocity.y < fallClamp)
        {
            rb.velocity = new Vector2(rb.velocity.x, fallClamp);
            horizontalMovement = false;
        }
        else
        {
            horizontalMovement = true;
        }
    }

    private void UpdateStateBools()
    {
        isRunning = rb.velocity.x != 0;

        if (isJumping)
        {
            isRunning = false;
            isFalling = false;
        }

        if (isGrounded)
        {
            isJumping = false;
            isFalling = false;
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void CalculateMovementSpeed()
    {
        moveSpeed = baseMoveSpeed + (MomentumManager.Instance.currentHorizontalMomentum / 3.5f);
    }

    public void CalculateJumpForce()
    {
        timeBtwJumps = baseTimeBtwJumps - (MomentumManager.Instance.currentHorizontalMomentum / 30f);
        jumpForce = baseJumpForce + (MomentumManager.Instance.currentJumpMomentum);
    }
}
