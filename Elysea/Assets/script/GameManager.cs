using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Save
{
    public float explorationTime = 180;
    public float waveTime = 60;
    public int enemiesToSpawn1 = 20;
    public int enemiesToSpawn2 = 10;
    private float tempsPasse = 0;
    private bool isExplorationPhase = true;
    private bool isWavePhase = false;
    private bool isBossPhase = false;
    public int spawnRadiusMin1 = 10;
    public int spawnRadiusMax1 = 20;
    public int spawnRadiusMin2 = 10;
    public int spawnRadiusMax2 = 20;
    public int spawnRadiusBoss = 30;
    [HideInInspector]
    public Transform player;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject bossPrefab;
    private string sceneName;

    // [Header("UI Settings")]
    [HideInInspector]
    public EndScreenManager endScreen;
    private SaveData dataSave;
    private SelectTechnologyPanel panel;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        
        if (player == null)
        {
            GameObject playerBoat = GameObject.Find("PlayerBoat");
            if (playerBoat != null)
                player = playerBoat.transform;
            else
                Debug.LogWarning("GameManager: PlayerBoat non trouvé");
        }
        
        if (endScreen == null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                Transform endScreenTransform = canvas.transform.Find("EndScreenPanel");
                if (endScreenTransform != null)
                    endScreen = endScreenTransform.GetComponent<EndScreenManager>();
            }
            
            if (endScreen == null)
                Debug.LogWarning("GameManager: EndScreenPanel non trouvé");
        }

        if (panel == null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                Transform choicePanelTransform = canvas.transform.Find("ChoicePanel");
                if (choicePanelTransform != null)
                    panel = choicePanelTransform.GetComponent<SelectTechnologyPanel>();
            }
            
            if (panel == null)
                Debug.LogWarning("GameManager: ChoicePanel non trouvé");
        }

        if (sceneName == "Tuto") 
        {
            setNumSave(0);
            createNewGame(0);
        }
        
        dataSave = GetSave(getNumSave());
        ReadDataSave();      

        
        Time.timeScale = 1f;
    }

    void Update()
    {
        tempsPasse += Time.deltaTime;


        if (tempsPasse >= explorationTime && isExplorationPhase)
        {
            isExplorationPhase = false;
            isWavePhase = true;
            tempsPasse = 0;
            SpawnEnemies();
        }
        else if (tempsPasse >= waveTime && isWavePhase)
        {
            isWavePhase = false;
            isBossPhase = true;
            tempsPasse = 0;
            SpawnBoss();
        }
        if (isBossPhase && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            WinGame();
            isBossPhase = false;
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn1; i++)
        {
            // Génération aléatoire
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(spawnRadiusMin1, spawnRadiusMax1);

            // Calculer la position autour du joueur
            Vector3 spawnPos = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;

            // Instancier l'ennemi
            Instantiate(enemyPrefab1, spawnPos, Quaternion.identity);
        }

        for (int i = 0; i < enemiesToSpawn2; i++)
        {
            // Génération aléatoire
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(spawnRadiusMin2, spawnRadiusMax2);

            // Calculer la position autour du joueur
            Vector3 spawnPos = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;

            // Instancier l'ennemi
            Instantiate(enemyPrefab2, spawnPos, Quaternion.identity);
        }

    }
    void SpawnBoss()
    {

        // Générer un angle aléatoire
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        // Calculer la position autour du joueur
        Vector3 spawnPos = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * spawnRadiusBoss;

        // Instancier le boss
        Instantiate(bossPrefab, spawnPos, Quaternion.identity);

    }

    void WinGame()
    {
        //Debug.Log("dataSave.level : " + dataSave.level  + " dataSave.endLevel : " + dataSave.endLevel);
        if(dataSave.level < dataSave.endLevel)
        {
            WriteDataSave();
            endScreen.WinConfig();
        }
        else
        {
            DeleteSave(getNumSave());
            endScreen.EndConfig();
        }
        
        endScreen.Show("BRAVO, TU AS GAGNÉ !");
        PauseGame();
    }

    private void ReadDataSave()
    {
        player.GetComponent<PlayerHealth>().SetHealth(dataSave.currentHealth);
        
        if (dataSave.technology != null)
        {
            foreach (Tech tech in dataSave.technology)
            {
                panel.setActualTechnology(tech);
            }
        }
    }

    private void WriteDataSave()
    {
        dataSave.currentHealth = player.GetComponent<PlayerHealth>().GetCurrentHealth();
        dataSave.level += 1;
        //Debug.Log("Sauvegarde du niveau " + dataSave.level);
        //Debug.Log("Tech sélectionnée à sauvegarder : " + panel.getCurrentTechnology()?.Count);
        dataSave.technology = panel.getCurrentTechnology();
        //Debug.Log("Tech sélectionnée sauvegardée : " + dataSave.technology?.Count);

        WriteSave(getNumSave(), dataSave);
    }

    public void GameOver()
    {
        endScreen.Show("VOUS AVEZ COULÉ...");
        PauseGame();
    }

    void PauseGame()
    {
        // Met le jeu en pause
        Time.timeScale = 0f;
    }

    public int getActualTime()
    {

        if (isExplorationPhase)
        {
            return (int)(explorationTime - tempsPasse);
        }
        else if (isWavePhase)
        {
            return (int)(waveTime - tempsPasse);
        }
        else if (isBossPhase)
        {
            return (int)tempsPasse;
        }
        return 0;
    }

    public bool getIsExplorationPhase()
    {
        return isExplorationPhase;
    }

    public bool getIsWavePhase()
    {
        return isWavePhase;
    }

    public bool getIsBossPhase()
    {
        return isBossPhase;
    }
}

