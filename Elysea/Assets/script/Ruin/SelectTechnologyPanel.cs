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
    private List<string[]> AllTechnologies = new List<string[]>();
    private List<string[]> AllowTechnologies = new List<string[]>();
    private List<string[]> CurrentTechnologies = new List<string[]>();  
    private bool isAllowUr = false;

    private void Start()
    {

        // Load technology data from the text file
        foreach(string line in TechFile.text.Split('\n'))
        {
            string[] data = line.Split(',');

            if (int.TryParse(data[0], out int result) && data[15] == "")
            {
                AllTechnologies.Add(data);
            }
        }

        foreach (string[] tech in CurrentTechnologies) if(tech[2] == "Lab. Nucléaire") isAllowUr = true;


        // Technologies that are currently available to the player
        foreach (string[] tech in AllTechnologies)
        {
            if (tech[12]=="" || (isAllowUr && tech[12] == "Lab. Nucléaire"))
            {
                AllowTechnologies.Add(tech);
            }
        }

        System.Random rand = new System.Random();
        var randomValues = AllowTechnologies.OrderBy(x => rand.Next()).Take(3).ToList();

        
        Button1.GetComponentInChildren<TextMeshProUGUI>().text = randomValues[0][2];
        Button2.GetComponentInChildren<TextMeshProUGUI>().text = randomValues[1][2];
        Button3.GetComponentInChildren<TextMeshProUGUI>().text = randomValues[2][2];
        
    
    }

}

