using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class EndScreenManager : MonoBehaviour
{
    public TextMeshProUGUI endText;

    public void Show(string message)
    {
        gameObject.SetActive(true);
        endText.text = message;
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

}