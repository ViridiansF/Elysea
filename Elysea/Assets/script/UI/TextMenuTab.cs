using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextMenuTab : MonoBehaviour
{
    [HideInInspector]
    public SelectTechnologyPanel TechPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (TechPanel == null){
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null){
                TechPanel = canvas.GetComponentInChildren<SelectTechnologyPanel>(includeInactive: true);
                if (TechPanel == null)
                    Debug.LogWarning("ruinCollision: TechPanel non trouvé");
            }
            else
                Debug.LogWarning("ruinCollision: Canvas non trouvé");
        }
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
