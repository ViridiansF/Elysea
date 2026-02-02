using UnityEngine;

public class ruinCollision : MonoBehaviour
{

    //[SerializeField] private GameObject choicePanel;
    [SerializeField] private SelectTechnologyPanel choicePanel;
    [SerializeField] private GameObject player;

    private void Start()
    {
        choicePanel.Button1.onClick.AddListener(Choice1Clicked);
        choicePanel.Button2.onClick.AddListener(Choice2Clicked);
        choicePanel.Button3.onClick.AddListener(Choice3Clicked);
    }

    private void Choice1Clicked()
    {
        choicePanel.gameObject.SetActive(false);
        Debug.Log("1 clicked");

        player.GetComponentInChildren<shootPlayer>().fireRate = 0.1f;

        Destroy(gameObject);
        Time.timeScale = 1;
    }

    private void Choice2Clicked()
    {
        choicePanel.gameObject.SetActive(false);
        Debug.Log("2 clicked");

        player.GetComponentInChildren<shootPlayer>().fireRate = 1.0f;

        Destroy(gameObject);
        Time.timeScale = 1;
    }

    private void Choice3Clicked()
    {
        choicePanel.gameObject.SetActive(false);
        Debug.Log("3 clicked");

        player.GetComponentInChildren<shootPlayer>().fireRate = 2.0f;

        Destroy(gameObject);
        Time.timeScale = 1;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if(other.name == "Body")
        {
            Debug.Log("ruin collision");
            choicePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }


}
