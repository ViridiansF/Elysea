using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveButton : MonoBehaviour
{
    public TextMeshProUGUI buttonSave1;
    public TextMeshProUGUI buttonSave2;
    public TextMeshProUGUI buttonSave3;

    [System.Serializable]
    public class SaveData
    {
        public int niveau;
        public float vie;
        public List<Tech> technology;
    }
    SaveData save1;
    SaveData save2;
    SaveData save3;

    public void Start()
    {
        





        if (save1 == null)
        {
            buttonSave1.text = "Sauvegarde 1\nNew";
        }
        else
        {
            buttonSave1.text = "Sauvegarde 1\nNiveau = " + save1.niveau;
        }

        if (save2 == null)
        {
            buttonSave2.text = "Sauvegarde 2\nNew";
        }
        else
        {
            buttonSave2.text = "Sauvegarde 2\nNiveau = " + save2.niveau;
        }

        if (save3 == null)
        {
            buttonSave3.text = "Sauvegarde 3\nNew";
        }
        else
        {
            buttonSave3.text = "Sauvegarde 3\nNiveau = " + save3.niveau;
        }
        
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu Principal");
    }

    public void Save(int numSave)
    {
        string path = Application.persistentDataPath + "/save" + numSave + ".json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log("Niveau : " + data.niveau);
        }
        else createGame(numSave);
    }

    public void createGame(int numSave)
    {
        
    }
}
