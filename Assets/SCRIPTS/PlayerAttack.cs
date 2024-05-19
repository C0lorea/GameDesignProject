using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float basicAttackCooldown = 1f; // Cooldown for basic attack
    [SerializeField] private float specialAttackCooldown = 3f; // Cooldown for special attack
    [SerializeField] private float range = 1f; // Range of the attack
    [SerializeField] private float colliderDistance = 1f; // Distance of the collider
    [SerializeField] private int basicAttackDamage = 1; // Damage of basic attack
    [SerializeField] private int specialAttackDamage = 2; // Damage of special attack
    [SerializeField] private BoxCollider2D boxCollider; // Collider for attack
    [SerializeField] private LayerMask enemyLayer; // Layer for enemy
    private Animator anim;
    private Health enemyHealth;

    private float basicAttackTimer = 0f;
    private float specialAttackTimer = 0f;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        basicAttackTimer += Time.deltaTime;
        specialAttackTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) && basicAttackTimer >= basicAttackCooldown)
        {
            Debug.Log("Basic attack key pressed");
            PerformRandomBasicAttack();
            basicAttackTimer = 0f;
        }
        if (Input.GetKeyDown(KeyCode.R) && specialAttackTimer >= specialAttackCooldown)
        {
            Debug.Log("Special attack key pressed");
            PerformSpecialAttack();
            specialAttackTimer = 0f;
        }
    }

    private void PerformRandomBasicAttack()
    {
        int attackNumber = Random.Range(1, 3); // Generates 1 or 2
        Debug.Log("Random attack number: " + attackNumber);
        if (attackNumber == 1)
        {
            anim.SetTrigger("BasicAttack1");
        }
        else
        {
            anim.SetTrigger("BasicAttack2");
        }

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(boxCollider.bounds.center, new Vector2(range, colliderDistance), 0, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(basicAttackDamage);
            }
        }
    }

    private void PerformSpecialAttack()
    {
        anim.SetTrigger("SpecialAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(boxCollider.bounds.center, new Vector2(range, colliderDistance), 0, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(specialAttackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (boxCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right *range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        }
    }
}
