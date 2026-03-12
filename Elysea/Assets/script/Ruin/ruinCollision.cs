using UnityEngine;

public class ruinCollision : MonoBehaviour
{

    //[SerializeField] private GameObject choicePanel;
    [SerializeField] private SelectTechnologyPanel choicePanel;
    [SerializeField] private GameObject player;
    public SelectTechnologyPanel panel;

    private void Start()
    {

    }

    private void Choice1Clicked()
    {
        choicePanel.gameObject.SetActive(false);
        Debug.Log("1 clicked");
        panel.setActualTechnology(panel.getRandomTechnology()[0]);
        Destroy(transform.root.gameObject);
        Time.timeScale = 1;
    }

    private void Choice2Clicked()
    {
        choicePanel.gameObject.SetActive(false);
        Debug.Log("2 clicked");
        panel.setActualTechnology(panel.getRandomTechnology()[1]);
        Destroy(transform.root.gameObject);
        Time.timeScale = 1;
    }

    private void Choice3Clicked()
    {
        choicePanel.gameObject.SetActive(false);
        Debug.Log("3 clicked");
        panel.setActualTechnology(panel.getRandomTechnology()[2]);
        Destroy(transform.root.gameObject);
        Time.timeScale = 1;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if(other.CompareTag("Player"))
        {
            Debug.Log("ruin collision");
            choicePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }


}
