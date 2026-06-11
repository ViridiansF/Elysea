using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBarBoss : MonoBehaviour
{
    public Image healthBarImage;

    void Start()
    {
    }

    internal void UpdateBar(float maxHealth, float currentHealth)
    {
        if (healthBarImage == null)
            return;

        float fillValue = currentHealth / maxHealth;
        healthBarImage.fillAmount = fillValue;
    }
}
