// Health.cs
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private Rigidbody2D rb; // Rigidbody2D reference
    private BoxCollider2D boxCollider; // BoxCollider2D reference
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
        
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Assign Rigidbody2D component
        boxCollider = GetComponent<BoxCollider2D>(); // Assign BoxCollider2D component
        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;
    }

    public void TakeDamage(float damageAmount)
    {
        if (!dead) // Check if the object is already dead
        {
            currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, startingHealth);

            if (currentHealth <= 0)
            {
                anim.SetTrigger("Die");

                // Disable relevant components
                var playerMovements = GetComponent<PlayerMovements>();
                if (playerMovements != null)
                    playerMovements.enabled = false;

                var enemyPatrol = GetComponent<enemyPatrol>();
                if (enemyPatrol != null)
                    enemyPatrol.enabled = false;

                var meleeEnemy = GetComponent<MeleeEnemy>();
                if (meleeEnemy != null)
                    meleeEnemy.enabled = false;

                // Ignore collisions with enemies
                Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);

                // Stop movement
                rb.velocity = Vector2.zero;

                // Adjust collider size and offset
                boxCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y / 2);
                boxCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - originalColliderSize.y / 4);

                dead = true;
            }
            else
            {
                anim.SetTrigger("Hit");
            }
        }
    }
        public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public void Respawn()
    {
        AddHealth(startingHealth);
        anim.ResetTrigger("Die");
        anim.Play("Idle");
        dead = false;

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }
}
