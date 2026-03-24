using UnityEngine;
using System.Collections.Generic;

public class TechEffectApplier : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private movBateauPlayer boatMovement;
    private List<shootBullet> shootSystems;

    private void Awake()
    {
        // Trouver les composants du joueur
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        boatMovement = FindAnyObjectByType<movBateauPlayer>();
        
        // Trouver tous les systèmes de tir du joueur
        shootSystems = new List<shootBullet>();
        shootSystems.AddRange(FindObjectsByType<shootBullet>(FindObjectsSortMode.None));
    }

    public void ApplyTechEffect(Tech tech)
    {
        if (tech == null || !tech.HasAnyBonus())
            return;

        Debug.Log($"Application de la technologie: {tech.getName()}");

        // Santé
        if (tech.GetHealthBonus() > 0 && playerHealth != null)
        {
            playerHealth.IncreaseMaxHealth(tech.GetHealthBonus());
            // Debug.Log($"  → Santé +{tech.GetHealthBonus()} (Total: {playerHealth.maxHealth})");
        }

        // Vitesse du bateau
        if (tech.GetSpeedBonus() > 0 && boatMovement != null)
        {
            boatMovement.IncreaseMaxSpeed(tech.GetSpeedBonus());
            // Debug.Log($"  → Vitesse +{tech.GetSpeedBonus()} (Total: {boatMovement.maxSpeed})");
        }

        // Dégâts des projectiles
        if (tech.GetDamageBonus() > 0)
        {
            foreach (shootBullet shooter in shootSystems)
            {
                if (shooter != null)
                {
                    shooter.IncrementDamage((int)tech.GetDamageBonus());
                }
            }
            // Debug.Log($"  → Dégâts +{tech.GetDamageBonus()}");
        }

        // Vitesse du vent
        if (tech.GetWindSpeedBonus() > 0 && boatMovement != null)
        {
            // TODO
            Debug.Log($"  → Bonus vent +{tech.GetWindSpeedBonus()} (à implémenter)");
        }

        // Portée de vision
        if (tech.GetVisionBonus() > 0)
        {
            // TODO
            Debug.Log($"  → Vision +{tech.GetVisionBonus()} (à implémenter)");
        }

        // Déchet nucléaire
        if (tech.GetNuclearWasteBonus() > 0)
        {
            // TODO
            Debug.Log($"  → Déchet nucléaire +{tech.GetNuclearWasteBonus()} (à implémenter)");
        }

        // Électricité
        if (tech.GetElectricityBonus() != 0)
        {
            // TODO
            Debug.Log($"  → Électricité +{tech.GetElectricityBonus()} (à implémenter)");
        }

        // Pollution
        if (tech.GetPollutionBonus() != 0)
        {
            // TODO
            Debug.Log($"  → Pollution {tech.GetPollutionBonus()} (à implémenter)");
        }
    }
}
