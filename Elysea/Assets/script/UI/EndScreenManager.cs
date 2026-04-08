using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndScreenManager : Save
{
    public TextMeshProUGUI endText;

    public GameObject replayButton;
    public GameObject nextButton;

    public void Show(string message)
    {
        gameObject.SetActive(true);
        endText.text = message;
    }

    public void WinConfig()
    {
        replayButton.SetActive(false);
        nextButton.SetActive(true);
    }

    public void EndConfig()
    {
        replayButton.SetActive(false);
        nextButton.SetActive(false);
    }

    public void Rejouer()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quitter()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu Principal");
    }

    public void Next()
    {
        int levelIndex = GetSave(getNumSave()).level;
        Debug.Log("Charger le niveau " + levelIndex);
        SceneManager.LoadScene("Level" + levelIndex);
    }

}