using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private Health playerHealth;

    private void Start()
    {
        // Find and assign the player's Health component
        playerHealth = FindObjectOfType<PlayerMovements>().GetComponent<Health>();

        // Initialize the current health bar fill amount
        UpdateHealthBar();
    }

    private void Update()
    {
        // Update the current health bar fill amount continuously
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // Calculate the fill amount based on the player's current health
        float fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;

        // Update the current health bar fill amount
        currenthealthBar.fillAmount = fillAmount * 0.1f;
    }
}
