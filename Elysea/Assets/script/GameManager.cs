using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Save
{
    public float explorationTime = 180;
    public float waveTime = 60;
    public int enemiesToSpawn = 20;
    private float tempsPasse = 0;
    private bool isExplorationPhase = true;
    private bool isWavePhase = false;
    private bool isBossPhase = false;
    public int spawnRadius = 10;
    [HideInInspector]
    public Transform player;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    // [Header("UI Settings")]
    [HideInInspector]
    public EndScreenManager endScreen;
    private Save.SaveData dataSave;
    private SelectTechnologyPanel panel;

    void Start()
    {
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
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Générer un angle aléatoire
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            // Calculer la position autour du joueur
            Vector3 spawnPos = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * spawnRadius;

            // Instancier l'ennemi
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
    void SpawnBoss()
    {

        // Générer un angle aléatoire
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        // Calculer la position autour du joueur
        Vector3 spawnPos = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * spawnRadius;

        // Instancier le boss
        Instantiate(bossPrefab, spawnPos, Quaternion.identity);

    }

    void WinGame()
    {
        if(dataSave.level +1 >= dataSave.endLevel)
        {
            WriteDataSave();
        }
        endScreen.Show("BRAVO, TU AS SURVÉCU !");
        PauseGame();
    }

    private void ReadDataSave()
    {
        player.GetComponent<PlayerHealth>().SetHealth(dataSave.health, dataSave.currentHealth);
        
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
        dataSave.health = player.GetComponent<PlayerHealth>().GetMaxHealth();
        if(panel.getCurrentTechnology() != null)
        {
            dataSave.technology = panel.getCurrentTechnology();
        }
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

