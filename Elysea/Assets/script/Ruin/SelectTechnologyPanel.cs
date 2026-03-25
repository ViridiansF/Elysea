using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using System;

public class SelectTechnologyPanel : MonoBehaviour
{
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public TextMeshProUGUI TitleText;
    public TextAsset TechFile;
    private List<Tech> AllTechnologies = new List<Tech>();
    private List<Tech> AllowTechnologies = new List<Tech>();
    private List<Tech> AllowTechnologiesNoDouble = new List<Tech>();
    private List<Tech> CurrentTechnologies = new List<Tech>();  
    private bool isAllowUr = false;
    private List<Tech> randomValues = new List<Tech>();
    private Tech techTemp;
    private bool match = false;

    private void Awake()
    {
        // Load technology data from the text file
        foreach(string line in TechFile.text.Split('\n'))
        {
            string[] data = line.Split(',');

            if (int.TryParse(data[0], out int result) && data[15] == "")
            {
                techTemp = new Tech(data[2], data[11], data[12]);
                AllTechnologies.Add(techTemp);
                //Debug.Log("Test création " + techTemp.getName() +" "+ techTemp.getDuplicity() +" "+ techTemp.getLockCondition());
            }
        }
    }

    private void OnEnable()
    {
        AllowTechnologies.Clear();
        AllowTechnologiesNoDouble.Clear();
        
        // Allow technologie with uranium
        foreach (Tech tech in CurrentTechnologies) if(tech.getName() == "Lab. Nucléaire") isAllowUr = true;

        // Technologies that are currently available to the player
        foreach (Tech tech in AllTechnologies)
        {
            if (tech.getLockCondition()=="" || (isAllowUr && tech.getLockCondition() == "Lab. Nucléaire"))
            {
                AllowTechnologies.Add(tech);
                //Debug.Log(tech.getName());
            }
        }

        
        foreach (Tech tech in AllowTechnologies)
        {
            match = false;
            if (!tech.isDuplicable() || CurrentTechnologies.Count==0)
            {
                foreach (Tech currentTech in CurrentTechnologies)
                {
                    if(tech.getName() == currentTech.getName())
                    {
                        match = true;
                        Debug.Log("Supp " + tech.getName() + " "+ currentTech.getName());
                    }
                }
            }
            if (!match)
            {
                AllowTechnologiesNoDouble.Add(tech);
            }
            
        }
        //DebugTechnologies();

        System.Random rand = new System.Random();
        randomValues = AllowTechnologiesNoDouble.OrderBy(x => rand.Next()).Take(3).ToList();
        
        Button1.GetComponentInChildren<TextMeshProUGUI>().text = randomValues[0].getName();
        Button2.GetComponentInChildren<TextMeshProUGUI>().text = randomValues[1].getName();
        Button3.GetComponentInChildren<TextMeshProUGUI>().text = randomValues[2].getName();
    
    }

    public void setActualTechnology(Tech tech)
    {
        CurrentTechnologies.Add(tech);
        /*
        if (!tech.isDuplicable())
        {
            AllTechnologies.Remove(tech);
            Debug.Log("Remove: "+ tech.getName() + " "+ tech.isDuplicable());
        } 
        */
    }

    public List<Tech> getRandomTechnology()
    {
        return randomValues;
    }

    public List<Tech> getCurrentTechnology()
    {
        return CurrentTechnologies;
    }


    private void DebugTechnologies()
    {
        Debug.Log(
            "AllowTechnologiesNoDouble: [" + string.Join(", ", AllowTechnologiesNoDouble.Select(t => t.getName())) + "]\n" +
            "CurrentTechnologies: [" + string.Join(", ", CurrentTechnologies.Select(t => t.getName())) + "]"
        );
    }
}

