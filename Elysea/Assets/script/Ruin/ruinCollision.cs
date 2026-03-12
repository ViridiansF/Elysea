using UnityEngine;

public class ruinCollision : MonoBehaviour
{

    //[SerializeField] private GameObject choicePanel;
    [SerializeField] private SelectTechnologyPanel choicePanel;
    [SerializeField] private GameObject player;

    private void Start()
    {

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
