using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class levelUp : MonoBehaviour
{
    
    public SelectTechnologyPanel panel;
    private List<Tech> CurrentTechnology;

    // Update is called once per frame

    void Start()
    {
        CurrentTechnology = panel.getCurrentTechnology();
    }
    void Update()
    {
        if(CurrentTechnology != panel.getCurrentTechnology())
        {
            CurrentTechnology = panel.getCurrentTechnology();
        }
    }
        
}
