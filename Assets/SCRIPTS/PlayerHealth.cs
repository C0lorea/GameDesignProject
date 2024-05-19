// PlayerHealth.cs
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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

    public void Die()
    {
        // Trigger "Die" animation
        anim.SetTrigger("Dead");
        // Additional death-related actions can be performed here
    }
}
