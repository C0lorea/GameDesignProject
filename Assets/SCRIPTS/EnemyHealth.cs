// EnemyHealth.cs
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator component missing from this GameObject");
        }
    }

    public void TakeDamage(int damage)
    {
        // Handle damage
        // Trigger "Hurt" animation
        anim.SetTrigger("isHurt");
        // Handle other damage-related actions here
    }

    public void Die()
    {
        // Trigger "Die" animation
        anim.SetTrigger("isDead");
        // Additional death-related actions can be performed here
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
