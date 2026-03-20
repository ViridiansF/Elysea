using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float explorationTime = 180;
    public float waveTime = 60;
    public int enemiesToSpawn = 20;
    private float tempsPasse = 0;
    private bool isExplorationPhase = true;
    private bool isWavePhase = false;
    private bool isBossPhase = false;
    public int spawnRadius = 10;
    public Transform player;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

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
        if(isBossPhase && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            PauseGame();
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

    void PauseGame()
    {
        // Met le jeu en pause
        Time.timeScale = 0f;
        Debug.Log("Aucun ennemi restant ! Jeu en pause.");
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

