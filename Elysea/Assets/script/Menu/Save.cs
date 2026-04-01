using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;


public class Save:MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public int level;
        public int endLevel;
        public float currentHealth;
        public float pollution;
        public List<Tech> technology;
    }

    protected SaveData save;

    public void setNumSave(int numSave)
    {
        PlayerPrefs.SetInt("SaveSlot", numSave);
    }
    public int getNumSave()
    {
        return PlayerPrefs.GetInt("SaveSlot", 1);
    }
    public void DeleteSave(int numSave)
    {
        string path = Application.persistentDataPath + "/save" + numSave + ".json";

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Sauvegarde " + numSave + " supprimée.");
        }
        else
        {
            Debug.Log("Aucune sauvegarde trouvée pour le slot " + numSave + ".");
        }
    }
    public void WriteSave(int numSave, SaveData data)
    {
        string path = Application.persistentDataPath + "/save" + numSave + ".json";
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Sauvegarde " + numSave + " écrite : " + path);
    }
    public void LoadSave(int numSave)
    {
        string path = Application.persistentDataPath + "/save" + numSave + ".json";
        SaveData data;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log("Niveau : " + data.level);
        }
        else data = createNewGame(numSave);

        PlayerPrefs.SetInt("SaveSlot", numSave);
        LaunchGame(numSave);
    }
    protected SaveData GetSave(int numSave)
    {
        string path = Application.persistentDataPath + "/save" + numSave + ".json";
        if (!File.Exists(path))
        {
            Debug.Log("Aucune sauvegarde trouvée pour le slot " + numSave );
        }
        else
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;        
    }

    private SaveData createNewGame(int numSave)
    {
        SaveData data = new SaveData();

        // Valeurs de départ
        data.level = 1;
        data.endLevel = 2;
        data.currentHealth = 10f;
        data.pollution = 0f;
        data.technology = new List<Tech>();

        WriteSave(numSave, data);

        return data;
    }
    private void LaunchGame(int numSave)
    {
        setNumSave(numSave);
        SceneManager.LoadScene("Level" + GetSave(numSave).level);
    }
}
