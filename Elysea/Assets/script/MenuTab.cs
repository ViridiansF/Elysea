using UnityEngine;

public class MenuTab : MonoBehaviour
{
    // [HideInInspector]
    public GameObject menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (menu == null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null){
                menu = canvas.transform.Find("MenuTab").gameObject;
                if (menu == null)
                    Debug.LogWarning("MenuTab: MenuTab non trouvé");
            }
            else
                Debug.LogWarning("MenuTab: Canvas non trouvé");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
        }
        
    }
}
