using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveButton : Save
{
    public TextMeshProUGUI buttonSave1;
    public TextMeshProUGUI buttonSave2;
    public TextMeshProUGUI buttonSave3;

    

    public void Start()
    {
        
        save = GetSave("save1.json");
        if (save == null)
        {
            buttonSave1.text = "Sauvegarde 1\nNew";
        }
        else
        {
            buttonSave1.text = "Sauvegarde 1\nNiveau = " + save.niveau;
        }

        save = GetSave("save2.json");
        if (save == null)
        {
            buttonSave2.text = "Sauvegarde 2\nNew";
        }
        else
        {
            buttonSave2.text = "Sauvegarde 2\nNiveau = " + save.niveau;
        }

        save = GetSave("save3.json");
        if (save == null)
        {
            buttonSave3.text = "Sauvegarde 3\nNew";
        }
        else
        {
            buttonSave3.text = "Sauvegarde 3\nNiveau = " + save.niveau;
        }
        
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu Principal");
    }

    public new void DeleteSave(int numSave)
    {
        base.DeleteSave(numSave);
        Start();
    }


}
