using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void LancerJeu()
    {
        SceneManager.LoadScene("Tuto");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Quitter()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void Sauvegardes()
    {
        SceneManager.LoadScene("Save");
    }
}
