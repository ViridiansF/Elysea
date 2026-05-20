using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 100f;
    public float currentHealth;
    HealthBar healthBar;

    public int gruid;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip sound;

    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.UpdateBar(maxHealth, currentHealth);
        }
    }

    public void takeDamage(float damage)
    {
        if (audioSource != null && sound != null)
            audioSource.PlayOneShot(sound);

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        healthBar.UpdateBar(maxHealth, currentHealth);

    }

    public void SetHealth(float currentHealthGive)
    {
        //Quand on charge une partie sans sauvegarde
        if(currentHealthGive == 0)
        {
            currentHealthGive = maxHealth;
        }
        currentHealth = currentHealthGive;
        healthBar.UpdateBar(maxHealth, currentHealth);
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth += amount; // Augmente la santé actuelle en même temps
        healthBar.UpdateBar(maxHealth, currentHealth);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

}
