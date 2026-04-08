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

        public SaveData(int level=1, int endLevel=2, float currentHealth=10f, float pollution=0f, List<Tech> technology=null)
        {
            this.level = level;
            this.endLevel = endLevel;
            this.currentHealth = currentHealth;
            this.pollution = pollution;
            this.technology = technology ?? new List<Tech>();
        }
    }

    protected SaveData save;

    /// <summary>
    /// Permet de définir le numéro de la sauvegarde à utiliser pour le jeu en cours. Par défaut, la sauvegarde 1 est utilisée.
    /// </summary>
    /// <param name="numSave"></param>
    public void setNumSave(int numSave)
    {
        PlayerPrefs.SetInt("SaveSlot", numSave);
    }
    /// <summary>
    /// Permet de récupérer le numéro de la sauvegarde à utiliser pour le jeu en cours. Par défaut, la sauvegarde 1 est utilisée.
    /// </summary>
    /// <returns></returns>
    public int getNumSave()
    {
        return PlayerPrefs.GetInt("SaveSlot", 1);
    }
    /// <summary>
    /// Permet de supprimer la sauvegarde correspondant au numéro de sauvegarde donné en paramètre.
    /// </summary>
    /// <param name="numSave"></param>
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
    /// <summary>
    /// Permet d'écrire les données de sauvegarde dans un fichier JSON correspondant au numéro de sauvegarde donné en paramètre.
    /// </summary>
    /// <param name="numSave"></param>
    /// <param name="data"></param>
    public void WriteSave(int numSave, SaveData data)
    {
        string path = Application.persistentDataPath + "/save" + numSave + ".json";
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Sauvegarde " + numSave + " écrite : " + path);
    }
    /// <summary>
    /// Permet de charger les données de sauvegarde correspondant au numéro de sauvegarde donné en paramètre.
    /// </summary>
    /// <param name="numSave"></param>
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
    /// <summary>
    /// Permet de récuérir les données de sauvegarde correspondant au numéro de sauvegarde donné en paramètre. 
    /// Si aucune sauvegarde n'est trouvée, une nouvelle partie est créée.
    /// </summary>
    /// <param name="numSave"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Permet de créer une nouvelle partie avec les données de sauvegarde par défaut 
    /// et de les écrire dans un fichier JSON correspondant au numéro de sauvegarde donné en paramètre.
    /// </summary>
    /// <param name="numSave"></param>
    /// <returns></returns>
    public SaveData createNewGame(int numSave)
    {
        SaveData data = new SaveData();

        if (numSave == 0) data.endLevel = 1;

        WriteSave(numSave, data);

        return data;
    }
    /// <summary>
    /// Permet de lancer le jeu en chargeant la scène correspondant au niveau de la sauvegarde donnée en paramètre.
    /// </summary>
    /// <param name="numSave"></param>
    private void LaunchGame(int numSave)
    {
        setNumSave(numSave);
        SceneManager.LoadScene("Level" + GetSave(numSave).level);
    }
}
