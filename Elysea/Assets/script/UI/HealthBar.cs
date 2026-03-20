using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image healthBarImage;

    internal void UpdateBar(float maxHealth, float currentHealth)
    {
        Debug.Log("1 clicked");
        float fillValue = currentHealth / maxHealth;

        healthBarImage.fillAmount = fillValue;
    }
}
