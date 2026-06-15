using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveButton : Save
{
    public TextMeshProUGUI buttonSave1;
    public TextMeshProUGUI buttonSave2;
    public TextMeshProUGUI buttonSave3;

    public AudioSource audioSource;
    public AudioClip validateSound;
    public AudioClip backSound;
    

    public void Start()
    {
        save = GetSave(1);
        if (save == null)
        {
            buttonSave1.text = "Sauvegarde 1\nNew";
        }
        else
        {
            buttonSave1.text = "Sauvegarde 1\nNiveau = " + save.level;
        }

        save = GetSave(2);
        if (save == null)
        {
            buttonSave2.text = "Sauvegarde 2\nNew";
        }
        else
        {
            buttonSave2.text = "Sauvegarde 2\nNiveau = " + save.level;
        }

        save = GetSave(3);
        if (save == null)
        {
            buttonSave3.text = "Sauvegarde 3\nNew";
        }
        else
        {
            buttonSave3.text = "Sauvegarde 3\nNiveau = " + save.level;
        }
        
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu Principal");
        PlayBack();
    }

    public new void DeleteSave(int numSave)
    {
        PlayBack();
        base.DeleteSave(numSave);
        Start();
    }

    public void PlayValidate()
    {
        audioSource.PlayOneShot(validateSound);
    }

    public void PlayBack()
    {
        // audioSource.PlayOneShot(backSound);
    }

}
