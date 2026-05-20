using UnityEngine;

public class ruinCollision : MonoBehaviour
{

    //[SerializeField] private GameObject choicePanel;
    [SerializeField]
    [HideInInspector]
    private SelectTechnologyPanel choicePanel;
    [SerializeField]
    [HideInInspector]
    private GameObject player;


    public AudioSource audioSource;
    public AudioClip ruinSound;


    private void Start()
    {
        if (choicePanel == null){
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null){
                choicePanel = canvas.GetComponentInChildren<SelectTechnologyPanel>(includeInactive: true);
                if (choicePanel == null)
                    Debug.LogWarning("ruinCollision: ChoicePanel non trouvé");
            }
            else
                Debug.LogWarning("ruinCollision: Canvas non trouvé");
        }
        
        if (player == null)
        {
            GameObject playerBoat = GameObject.Find("PlayerBoat");
            if (playerBoat != null)
                player = playerBoat;
            else
                Debug.LogWarning("ruinCollision: PlayerBoat non trouvé");
        }
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
            audioSource.PlayOneShot(ruinSound);
            choicePanel.Button1.onClick.AddListener(Choice1Clicked);
            choicePanel.Button2.onClick.AddListener(Choice2Clicked);
            choicePanel.Button3.onClick.AddListener(Choice3Clicked);

            //Debug.Log("ruin collision");
            choicePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }


}
