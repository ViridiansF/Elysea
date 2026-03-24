using UnityEngine;

public class Tech
{
    private string nameTech;
    private bool duplicity;
    private string lockCondition;
    
    // Bonus des technologies (colonnes 4-10 du CSV)
    private float healthBonus;
    private float visionBonus;
    private float nuclearWasteBonus;
    private float damageBonus;
    private float electricityBonus;
    private float windSpeedBonus;
    private float speedBonus;
    private float pollutionBonus;

    public Tech(string name, string duplicity, string lockCondition, 
                string health = "", string vision = "", string nuclearWaste = "",
                string damage = "", string electricity = "", string windSpeed = "",
                string speed = "", string pollution = "")
    {
        nameTech = name;
        this.duplicity = !(duplicity == "");
        this.lockCondition = lockCondition;
        
        // Convertir les chaînes en float, 0 si vide
        healthBonus = ParseFloat(health);
        visionBonus = ParseFloat(vision);
        nuclearWasteBonus = ParseFloat(nuclearWaste);
        damageBonus = ParseFloat(damage);
        electricityBonus = ParseFloat(electricity);
        windSpeedBonus = ParseFloat(windSpeed);
        speedBonus = ParseFloat(speed);
        pollutionBonus = ParseFloat(pollution);
    }

    private static float ParseFloat(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0f;
        
        if (float.TryParse(value, out float result))
            return result;
        
        return 0f;
    }

    public string getName() => nameTech;
    public bool isDuplicable() => duplicity;
    public string getLockCondition() => lockCondition;
    
    // Getters pour les bonus
    public float GetHealthBonus() => healthBonus;
    public float GetVisionBonus() => visionBonus;
    public float GetNuclearWasteBonus() => nuclearWasteBonus;
    public float GetDamageBonus() => damageBonus;
    public float GetElectricityBonus() => electricityBonus;
    public float GetWindSpeedBonus() => windSpeedBonus;
    public float GetSpeedBonus() => speedBonus;
    public float GetPollutionBonus() => pollutionBonus;
    
    public bool HasAnyBonus()
    {
        return healthBonus != 0 || visionBonus != 0 || nuclearWasteBonus != 0 || 
               damageBonus != 0 || electricityBonus != 0 || windSpeedBonus != 0 || 
               speedBonus != 0 || pollutionBonus != 0;
    }

    public override bool Equals(object obj)
    {
        if (obj is Tech other)
        {
            return nameTech == other.nameTech;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return nameTech.GetHashCode();
    }
}