using UnityEngine;

public class ruinCollision : MonoBehaviour
{

    //[SerializeField] private GameObject choicePanel;
    [SerializeField] private SelectTechnologyPanel choicePanel;
    [SerializeField] private GameObject player;

    private void Start()
    {

    }

    private void Choice1Clicked()
    {
        Debug.Log("1 clicked: " + choicePanel.getRandomTechnology()[0].getName());
        choicePanel.setActualTechnology(choicePanel.getRandomTechnology()[0]);
        Destroy(transform.root.gameObject);
        choicePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void Choice2Clicked()
    {
        Debug.Log("2 clicked: " + choicePanel.getRandomTechnology()[1].getName());
        choicePanel.setActualTechnology(choicePanel.getRandomTechnology()[1]);
        Destroy(transform.root.gameObject);
        choicePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void Choice3Clicked()
    {
        Debug.Log("3 clicked: " + choicePanel.getRandomTechnology()[2].getName());
        choicePanel.setActualTechnology(choicePanel.getRandomTechnology()[2]);
        Destroy(transform.root.gameObject);
        choicePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if(other.CompareTag("Player"))
        {
            choicePanel.Button1.onClick.AddListener(Choice1Clicked);
            choicePanel.Button2.onClick.AddListener(Choice2Clicked);
            choicePanel.Button3.onClick.AddListener(Choice3Clicked);

            //Debug.Log("ruin collision");
            choicePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }


}
