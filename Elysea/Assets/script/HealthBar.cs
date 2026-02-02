using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image fillImage;

    void Update()
    {
        fillImage.fillAmount =
            playerHealth.currentHealth / playerHealth.maxHealth;
    }
}
