using UnityEngine;

public class MenuTab : MonoBehaviour
{
    public GameObject menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
