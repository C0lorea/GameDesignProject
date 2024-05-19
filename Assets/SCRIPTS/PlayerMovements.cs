using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float slideSpeedMultiplier = 2f;
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float wallJumpCooldownTime = 0.5f;  
    [SerializeField] private float deathYThreshold = -0.1f;
    [SerializeField] private float startingHealth = 1f; // Adjust as needed
    private float currentHealth; // Player's current health

    private float wallJumpCooldown;
    private float horizontalInput;
    private bool isSliding;
    private float slideTimer;
    public GemManager cm;

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private Vector2 respawnPoint;

    private bool isDead; // Flag to track if the player is dead

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;
        respawnPoint = transform.position;
        currentHealth = startingHealth; // Initialize current health
    }

    private void Update()
    {
        if (!isDead)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            HandleMovement();
            HandleJump();
            HandleSlide();

            anim.SetBool("Grounded", isGrounded());

            if (transform.position.y < deathYThreshold)
                Die();
            
            // Optionally, decrease health over time or due to other factors
        }
    }

    private void HandleMovement()
    {
        if (!isSliding)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            anim.SetBool("Run", horizontalInput != 0);

            if (horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandleSlide()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded() && !isSliding)
        {
            isSliding = true;
            slideTimer = slideDuration;

            // Adjust the collider size and offset for sliding
            boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y / 2);
            boxCollider.offset = new Vector2(boxCollider.offset.x, originalColliderOffset.y - boxCollider.size.y / 4);

            body.velocity = new Vector2(horizontalInput * speed * slideSpeedMultiplier, body.velocity.y);
            anim.SetTrigger("Slide");
        }

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0)
            {
                isSliding = false;

                // Reset the collider size and offset after sliding
                boxCollider.size = originalColliderSize;
                boxCollider.offset = originalColliderOffset;
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded())
            {
                Jump();
            }
            else if (onWall() && wallJumpCooldown <= 0)
            {
                WallJump();
            }
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        anim.SetTrigger("Jump");
    }

    private void WallJump()
    {
        // Add a horizontal force for the wall jump
        Vector2 jumpDirection = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, jumpPower);
        body.velocity = jumpDirection;
        wallJumpCooldown = wallJumpCooldownTime;  // Reset the cooldown
        anim.SetTrigger("Jump");  // Trigger jump animation
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    
    public void Die()
    {
        isDead = true; // Set the flag to indicate that the player is dead
        anim.SetTrigger("Die"); // Trigger the death animation
        // Optionally, disable player movement or other inputs here
    }

    // New method to handle animation event for respawning
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            cm.gemCount++;
            // Perform any desired effects when collecting the gem
        }
    }
        public void ResetMovementInput()
    {
        // Reset any movement-related variables or flags here
        horizontalInput = 0f; // Reset horizontal input to stop movement
        isSliding = false; // Reset sliding flag
        // Add any other variables or flags that need to be reset
    }

}
