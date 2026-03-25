using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 100f;
    public float currentHealth;
    HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        if (healthBar != null)
        {
            healthBar.UpdateBar(maxHealth, currentHealth);
        }
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        healthBar.UpdateBar(maxHealth, currentHealth);

    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth += amount; // Augmente la santé actuelle en même temps
        healthBar.UpdateBar(maxHealth, currentHealth);
    }

}
