using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // References
    private Rigidbody2D rb;
    [SerializeField] private PlayerSpriteRenderer playerSpriteRenderer;

    // Moving vars
    [SerializeField] private float moveSpeed;
    private float moveInput;

    // Jumping vars
    [SerializeField] private float jumpForce;
    private float jumpTimeCounter;
    [SerializeField] private float jumpTime;

    // State vars
    public bool isRunning { get; private set; }
    public bool isJumping { get; private set; }
    public bool isFalling { get; private set; }
    public bool isDashing { get; private set; }
    public bool isCrouching { get; private set; }

    // Grounded vars
    public bool isGrounded { get; private set; }
    [SerializeField] private Transform feetPos;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;

    // Coyote time
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    // Jump buffer
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    // Fall Clamp
    [SerializeField] private float fallClamp = -10f;

    // Dust Particles


    // Animation vars
    const string PLAYER_IDLE = "Player Idle";
    const string PLAYER_RUN = "Player Run";
    const string PLAYER_JUMP = "Player Jump";
    const string PLAYER_FALL = "Player Fall";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);

        CheckCoyoteTime();

        CheckJumpBuffer();

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f) {
            isJumping = true;
            isFalling = false;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping == true) {
            
            if (jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
                isFalling = true;
            }

        }
    
        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
            isFalling = true;

            coyoteTimeCounter = 0f;
        }

        if (isFalling)
        {
            FallClampCheck();
        }

        UpdateStateBools();
    }

    // Handling Animation in LateUpdate


    private void CheckCoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void CheckJumpBuffer()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void FallClampCheck()
    {
        if (rb.velocity.y < fallClamp)
        {
            rb.velocity = new Vector2(rb.velocity.x, fallClamp);
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
}
