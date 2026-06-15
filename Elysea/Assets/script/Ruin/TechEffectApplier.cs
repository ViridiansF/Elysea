using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class TechEffectApplier : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private movBateauPlayer boatMovement;
    private List<shootBullet> shootSystems;
    private Transform playerTransform;
    private GameManager gameManager;
    private Camera playerCamera;

    private void Awake()
    {
        // Trouver les composants du joueur
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        boatMovement = FindAnyObjectByType<movBateauPlayer>();
        gameManager = FindAnyObjectByType<GameManager>();
        playerCamera = Camera.main;
        playerTransform = boatMovement?.transform;
        
        // Trouver tous les systèmes de tir du joueur
        shootSystems = new List<shootBullet>();
        if (playerTransform != null)
            shootSystems.AddRange(playerTransform.GetComponentsInChildren<shootBullet>());
    }

    public void ApplyTechEffect(Tech tech)
    {
        if (tech == null || !tech.HasAnyBonus())
            return;

        Debug.Log($"Application de la technologie: {tech.getName()}");

        // Activer une arme si la technologie la déverrouille
        if (!string.IsNullOrWhiteSpace(tech.GetWeaponToEnable()))
        {
            EnableWeapon(tech.GetWeaponToEnable());
        }

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
            if (playerCamera != null)
            {
                PixelPerfectCamera pixelPerfectCamera = playerCamera.GetComponent<PixelPerfectCamera>();
                if (pixelPerfectCamera != null)
                {
                    float bonusPercentage = tech.GetVisionBonus();
                    pixelPerfectCamera.assetsPPU = (int)(pixelPerfectCamera.assetsPPU / (1 + bonusPercentage / 1000f));
                    Debug.Log($"  → Vision +{bonusPercentage}% (Nouvelle PPU: {pixelPerfectCamera.assetsPPU})");
                }
                else
                {
                    Debug.LogWarning("Composant PixelPerfectCamera non trouvé!");
                }
            }
            else
            {
                Debug.LogWarning("Caméra principale non trouvée!");
            }
        }

        // Déchet nucléaire
        if (tech.GetNuclearWasteBonus() > 0)
        {
            RessourceManager.Instance.AddNuclearWaste(tech.GetNuclearWasteBonus());
            // Debug.Log($"  → Déchet nucléaire +{tech.GetNuclearWasteBonus()} (à implémenter)");
        }

        // Électricité
        if (tech.GetElectricityBonus() != 0)
        {
            if(tech.GetElectricityBonus() > 0)
                RessourceManager.Instance.IncreaseMaxElectricity(tech.GetElectricityBonus());
            else
                RessourceManager.Instance.AddElectricity(tech.GetElectricityBonus());
            // Debug.Log($"  → Électricité +{tech.GetElectricityBonus()} (à implémenter)");
        }

        // Pollution
        if (tech.GetPollutionBonus() != 0)
        {
            RessourceManager.Instance.AddPollution(tech.GetPollutionBonus());
            // Debug.Log($"  → Pollution {tech.GetPollutionBonus()} (à implémenter)");
        }
    }

    private void EnableWeapon(string weaponName)
    {
        if (playerTransform == null)
            return;

        // Chercher le GameObject enfant avec le nom spécifié
        Transform weaponTransform = playerTransform.Find(weaponName);


        //si lance rocket
        if(weaponName == "LanceRocket" || weaponName == "LanceRocketNuke") {
            Transform normal = weaponTransform.Find("Normal");

            if (normal != null)
            {
                if(normal.gameObject.activeSelf) {
                    Transform nuklear = weaponTransform.Find("Nuklear");
                    Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBB : " + weaponTransform);
                    if(nuklear != null) {
                        weaponTransform = nuklear;
                        normal.gameObject.SetActive(false);
                    }
                } else {
                    weaponTransform = normal;
                }
            }
        }

        //active une arme
        if (weaponTransform != null)
        {
            weaponTransform.gameObject.SetActive(true);
            Debug.Log($"  → Arme activée: {weaponName}");
            
            // Rafraîchir les systèmes de tir au cas où de nouveaux composants shootBullet auraient été activés
            RefreshShootSystems();
        }
        else
        {
            Debug.LogWarning($"Arme '{weaponName}' introuvable sous le joueur!");
        }
    }

    // Pour rafraîchir les références aux systèmes de tir (au cas où ils seraient chargés dynamiquement)
    public void RefreshShootSystems()
    {
        shootSystems.Clear();
        if (playerTransform != null)
            shootSystems.AddRange(playerTransform.GetComponentsInChildren<shootBullet>());
    }
}
