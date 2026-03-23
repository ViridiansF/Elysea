using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextMenuTab : MonoBehaviour
{
    public SelectTechnologyPanel TechPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string text = "";
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();

        if (tmp == null)
        {
            Debug.LogError("TextMeshProUGUI non trouvé sur cet objet !");
            return;
        }

    
        foreach(Tech tech in TechPanel.getCurrentTechnology())
        {
            text += tech.getName() + "\n";
        }
        tmp.text=text;
    }
}
