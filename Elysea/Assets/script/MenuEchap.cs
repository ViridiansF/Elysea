using UnityEngine;

public class MenuEchap : MonoBehaviour
{
    private bool isMenuEchapActive = false;
    private bool isKeeDown = false;
    [HideInInspector]
    public GameObject menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (menu == null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null){
                menu = canvas.transform.Find("EndScreenPanel").gameObject;
                if (menu == null)
                    Debug.LogWarning("EndScreenPanel: EndScreenPanel non trouvé");
            }
            else
                Debug.LogWarning("EndScreenPanel: Canvas non trouvé");
        }
    }

    // Update is called once per frame
    void Update()
    {
        isKeeDown = Input.GetKeyDown(KeyCode.Escape);
        if(isKeeDown)
        {
            if(!menu.activeSelf && isKeeDown)
            {
                menu.SetActive(true);
                isMenuEchapActive = true;
                Time.timeScale = 0f;
                Debug.Log("MenuEchap: menu activeSelf = " + menu.activeSelf);
                isKeeDown = false;
            }
            if(menu.activeSelf && isMenuEchapActive && isKeeDown)
            {
                menu.SetActive(false);
                isMenuEchapActive = false;
                Time.timeScale = 1f;
                Debug.Log("MenuEchap: menu activeSelf = " + menu.activeSelf);
                isKeeDown = false;
            }
        }
        
    }
}
