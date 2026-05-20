using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class levelUp : MonoBehaviour
{
    
    public SelectTechnologyPanel selectTechnolyPanel;
    private List<Tech> CurrentTechnology;
    private TechEffectApplier effectApplier;
    private int nbTechActual = 0;

    void Start()
    {
        if (selectTechnolyPanel == null)
        {
            Debug.LogError("levelUp: SelectTechnologyPanel n'est pas assignée dans l'inspecteur!");
            return;
        }

        effectApplier = FindAnyObjectByType<TechEffectApplier>();
        nbTechActual = 0; 
    }

    void Update()
    {
        updateTechnology();
    }

    public void updateTechnology()
    {
        if (selectTechnolyPanel == null)
            return;

        // Vérifier si une nouvelle technologie a été acquise
        List<Tech> latestTechs = selectTechnolyPanel.getCurrentTechnology();
        if (latestTechs == null)
            return;

        // Comparer le count pour détecter l'ajout
        if (latestTechs.Count > nbTechActual)
        {
            Tech newTech = latestTechs[latestTechs.Count - 1];
            Debug.Log("Nouvelle technologie acquise: " + newTech.getName());
            
            // Appliquer les effets
            if (effectApplier != null)
            {
                effectApplier.ApplyTechEffect(newTech);
            }
            else
            {
                Debug.LogWarning("TechEffectApplier introuvable! Assurez-vous qu'il est attaché à un GameObject dans la scène.");
            }
            
            nbTechActual = latestTechs.Count;
        }
    }
        
}
