using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip validateSound;
    public AudioClip backSound;

    public void LancerJeu()
    {
        SceneManager.LoadScene("Tuto");
        PlayValidate();
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
        PlayValidate();
    }

    public void Quitter()
    {
        PlayBack();
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void Sauvegardes()
    {
        SceneManager.LoadScene("Save");
        PlayValidate();
    }




    public void PlayValidate()
    {
        audioSource.PlayOneShot(validateSound);
    }

    public void PlayBack()
    {
        audioSource.PlayOneShot(backSound);
    }

}

