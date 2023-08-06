using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // References
    public Rigidbody2D rb;
    [SerializeField] private PlayerSpriteRenderer playerSpriteRenderer;
	public Animator animator;

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
    public bool isJumping { get; private set; }
    public bool isFalling { get; private set; }
    public bool isBigFalling { get; private set; }

    // Grounded vars
    public bool isGrounded { get; private set; }
    [SerializeField] private Transform feetPos;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;

    // Fall
    [SerializeField] private float fallClamp = -10f;
    [SerializeField] private float maxFallTime;
    private float fallTimeCounter;
    private bool checkingFall;


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
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);

        CalculateMovementSpeed();
        CalculateJumpForce();

        CheckJumpTime();

        if (canJump && isGrounded) {
                if (!isJumping) {
                    isJumping = true;
                    jumpTimer = 0f;
                    isFalling = false;
                    canJump = false;
                    rb.velocity = Vector2.up * jumpForce;
                }
        }

        FallClampCheck();
        UpdateStateBools();

        if (isFalling && !checkingFall)
        {
            StartCoroutine(BigFallCheck());
        }
    }

    void LateUpdate()
    {
        // Player Animation
        if (isGrounded)
        {
            playerSpriteRenderer.ChangeAnimationState("Player_Jump");
        } 
        else 
        {
            if (isJumping)
            {
                playerSpriteRenderer.ChangeAnimationState("Player_Jump_Loop");
            }
            else if (isFalling)
            {
                if (isBigFalling)
                {
                    playerSpriteRenderer.ChangeAnimationState("Player_BigFall_Loop");
                }
                else 
                {
                    playerSpriteRenderer.ChangeAnimationState("Player_Fall_Loop");
                }
            }
        }

        if (moveInput < 0f)
        {
            this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (moveInput > 0f)
        {
            this.transform.eulerAngles = Vector3.zero;
        }
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
        }
    }

    private IEnumerator BigFallCheck()
    {
        checkingFall = true;
        fallTimeCounter = 0f;
        
        while (isFalling)
        {
            fallTimeCounter += Time.deltaTime;
            if (fallTimeCounter > maxFallTime)
            {
                isBigFalling = true;
                horizontalMovement = false;
            }

            yield return null;
        }

        horizontalMovement = true;
        fallTimeCounter = 0f;
        checkingFall = false;
    }

    private void UpdateStateBools()
    {
        if (isJumping && rb.velocity.y <= 0) // Check if player is falling after jumping
        {
            isJumping = false;
            isFalling = true;
        }

        if (isGrounded && !isJumping) // Check if player is on the ground and not jumping
        {
            isFalling = false;
            isBigFalling = false;
        }

        if (!isFalling)
        {
            isBigFalling = false;
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
