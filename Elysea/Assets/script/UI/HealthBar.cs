using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [HideInInspector]
    public Image healthBarImage;
    [HideInInspector]
    public TextMeshProUGUI healthText;

    private void Start()
    {
        if (healthBarImage == null)
        {
            GameObject healthBarFilled = GameObject.Find("Canvas/HealthBarBackground/HealthBarFilled");

            if (healthBarFilled != null)
            {
                healthBarImage = healthBarFilled.GetComponent<Image>();
                if (healthBarImage == null)
                    Debug.LogWarning("HealthBar: pas de composante Image");
            }
            else
                Debug.LogWarning("HealthBar: GameObject non trouvé");
        }

        if (healthText == null)
        {
            GameObject healthTextObj = GameObject.Find("Canvas/HealthBarBackground/HealthText");

            if (healthTextObj != null)
            {
                healthText = healthTextObj.GetComponent<TextMeshProUGUI>();
                if (healthText == null)
                    Debug.LogWarning("HealthBar: pas de composante TextMeshProUGUI");
            }
            else
                Debug.LogWarning("HealthBar: GameObject non trouvé");
        }

    }

    internal void UpdateBar(float maxHealth, float currentHealth)
    {
        if (healthBarImage == null)
            return;

        float fillValue = currentHealth / maxHealth;
        healthBarImage.fillAmount = fillValue;

        if (healthText != null)
        {
            healthText.text = $"{currentHealth:F0} / {maxHealth:F0}";
        }
    }
}
