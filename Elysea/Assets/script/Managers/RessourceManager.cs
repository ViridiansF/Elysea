using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RessourceManager : MonoBehaviour
{
    public static RessourceManager Instance { get; private set; }
    // Pollution (max fixed at 100)
    private float pollution = 0f;
    private float maxPollution = 100f;
    private Image pollutionBar;
    private TextMeshProUGUI pollutionText;
    // Nuclear Waste (max fixed at 100)
    private float nuclearWaste = 0f;
    private float maxNuclearWaste = 100f;
    private Image nuclearWasteBar;
    private TextMeshProUGUI nuclearWasteText;
    // Electricity
    private float electricity = 0f;
    private float maxElectricity = 0f;
    private Image electricityBar;
    private TextMeshProUGUI electricityText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            FindUIElements();
            UpdateElectricityUI();
            UpdatePollutionUI();
            UpdateNuclearWasteUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FindUIElements()
    {
        // Chercher Canvas
        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogWarning("Canvas not found in scene!");
            return;
        }

        // Chercher Pollution UI
        Transform pollutionBarTransform = canvas.transform.Find("PolutionBarBackground/PolutionBarFilled");
        if (pollutionBarTransform != null)
            pollutionBar = pollutionBarTransform.GetComponent<Image>();
        else
            Debug.LogWarning("PolutionBarBackground/PolutionBarFilled not found in Canvas");

        Transform pollutionTextTransform = canvas.transform.Find("PolutionBarBackground/PolutionText");
        if (pollutionTextTransform != null)
            pollutionText = pollutionTextTransform.GetComponent<TextMeshProUGUI>();
        else
            Debug.LogWarning("PolutionBarBackground/PolutionText not found in Canvas");

        // Chercher Nuclear Waste UI
        Transform nuclearWasteBarTransform = canvas.transform.Find("UrWasteBarBackground/UrWasteBarFilled");
        if (nuclearWasteBarTransform != null)
            nuclearWasteBar = nuclearWasteBarTransform.GetComponent<Image>();
        else
            Debug.LogWarning("UrWasteBarBackground/UrWasteBarFilled not found in Canvas");

        Transform nuclearWasteTextTransform = canvas.transform.Find("UrWasteBarBackground/UrWasteText");
        if (nuclearWasteTextTransform != null)
            nuclearWasteText = nuclearWasteTextTransform.GetComponent<TextMeshProUGUI>();
        else
            Debug.LogWarning("UrWasteBarBackground/UrWasteText not found in Canvas");

        // Chercher Electricity UI
        Transform electricityBarTransform = canvas.transform.Find("ElecBarBackground/ElecBarFilled");
        if (electricityBarTransform != null)
            electricityBar = electricityBarTransform.GetComponent<Image>();
        else
            Debug.LogWarning("ElecBarBackground/ElecBarFilled not found in Canvas");

        Transform electricityTextTransform = canvas.transform.Find("ElecBarBackground/ElecText");
        if (electricityTextTransform != null)
            electricityText = electricityTextTransform.GetComponent<TextMeshProUGUI>();
        else
            Debug.LogWarning("ElecBarBackground/ElecText not found in Canvas");
    }

    // Pollution methods
    public float GetPollution() => pollution;
    public float GetMaxPollution() => maxPollution;
    public void AddPollution(float amount)
    {
        pollution += amount;
        UpdatePollutionUI();
    }
    public void ReducePollution(float amount)
    {
        pollution = Mathf.Max(0f, pollution - amount);
        UpdatePollutionUI();
    }
    public void SetPollution(float amount)
    {
        pollution = amount;
        UpdatePollutionUI();
    }

    void UpdatePollutionUI()
    {
        if (pollutionText != null)
            pollutionText.text = $"<rotate=90>{pollution:F0}/{maxPollution:F0}";
        if (pollutionBar != null)
            pollutionBar.fillAmount = maxPollution == 0f ? 0f : pollution / maxPollution;
    }


    // Nuclear Waste methods
    public float GetNuclearWaste() => nuclearWaste;
    public float GetMaxNuclearWaste() => maxNuclearWaste;
    public void AddNuclearWaste(float amount)
    {
        nuclearWaste += amount;
        UpdateNuclearWasteUI();
    }
    public void ReduceNuclearWaste(float amount)
    {
        nuclearWaste = Mathf.Max(0f, nuclearWaste - amount);
        UpdateNuclearWasteUI();
    }
    public void SetNuclearWaste(float amount)
    {
        nuclearWaste = amount;
        UpdateNuclearWasteUI();
    }

    void UpdateNuclearWasteUI()
    {
        if (nuclearWasteText != null)
            nuclearWasteText.text = $"{nuclearWaste:F0}/{maxNuclearWaste:F0}";
        if (nuclearWasteBar != null)
            nuclearWasteBar.fillAmount = maxNuclearWaste == 0f ? 0f : nuclearWaste / maxNuclearWaste;
    }

    // Electricity methods
    public float GetElectricity() => electricity;
    public float GetMaxElectricity() => maxElectricity;
    public void SetMaxElectricity(float amount) => maxElectricity = amount;
    public void IncreaseMaxElectricity(float amount)
    {
        maxElectricity += amount;
        electricity += amount;
        UpdateElectricityUI();
    }
    public void AddElectricity(float amount)
    {
        electricity += amount;
        UpdateElectricityUI();
    }
    public void ReduceElectricity(float amount)
    {
        electricity = Mathf.Max(0f, electricity - amount);
        UpdateElectricityUI();
    }
    public void SetElectricity(float amount)
    {
        electricity = amount;
        UpdateElectricityUI();
    }

    void UpdateElectricityUI()
    {
        // if (maxElectricity == 0f)
        // {
        //     // TODO : désactiver les éléments UI d'électricité
        // }
        // else
        // {
            if (electricityText != null)
                electricityText.text = $"{electricity:F0}/{maxElectricity:F0}";
            if (electricityBar != null)
                electricityBar.fillAmount = maxElectricity == 0f ? 0f : electricity / maxElectricity;
        // }
    }

    // Reset 
    public void ResetAll()
    {
        pollution = 0f;
        nuclearWaste = 0f;
        electricity = 0f;
        maxPollution = 0f;
        maxNuclearWaste = 0f;
        maxElectricity = 0f;
    }
}
