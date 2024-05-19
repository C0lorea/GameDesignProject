using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Collider2D playerCollider; // Reference to the player's collider

    private Animator anim;
    private Health playerHealth;
    private bool isDead = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private float cooldownTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && !isDead)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                                              new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                                              0, Vector2.left, 0, playerLayer);
        
        if(hit.collider != null )
            playerHealth = hit.transform.GetComponent<Health>();
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }

    // Method to be called when the enemy dies
    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            anim.SetTrigger("Die");

            // Disable the collider when the enemy dies
            boxCollider.enabled = false;

            // Stop movement by setting velocity to zero
            rb.velocity = Vector2.zero;

            // Freeze movement
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            // Adjust position to prevent floating
            rb.gravityScale = 1; // Enable gravity
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f); // Move down slightly

            // Ignore collisions with the player
            if (playerCollider != null)
            {
                Physics2D.IgnoreCollision(boxCollider, playerCollider, true);
            }

            // Adjust collider size and offset for visual purposes
            boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y / 2);
            boxCollider.offset = new Vector2(boxCollider.offset.x, boxCollider.offset.y - boxCollider.size.y / 4);
        }
    }
}