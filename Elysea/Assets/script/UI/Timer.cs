using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameManager gameManager;
    private TextMeshProUGUI text;
    private int minutes;
    private int seconds;
    private int totalSeconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        totalSeconds = gameManager.getActualTime();
        minutes = totalSeconds / 60;
        seconds = totalSeconds % 60;
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(gameManager.getIsExplorationPhase())
        {
            text.color = Color.green;
        }
        else if(gameManager.getIsWavePhase())
        {
            text.color = Color.yellow;
        }
        else if(gameManager.getIsBossPhase())
        {
            text.color = Color.red;
        }
    }
}
