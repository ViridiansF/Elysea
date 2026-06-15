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
    
    private levelUp levelUpScript;


    public AudioSource audioSource;
    public AudioClip newWaveSound;

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

        if (levelUpScript == null)
        {
            levelUpScript = FindAnyObjectByType<levelUp>();
            if (levelUpScript == null)
                Debug.LogWarning("GameManager: levelUp script non trouvé dans la scène");
        }

        if (sceneName == "Tuto")
        {
            setNumSave(0);
            createNewGame(0);
        }

        dataSave = GetSave(getNumSave());
        ReadDataSave();
        // Debug.Log($"GameManager: Niveau {dataSave.level} chargé avec {dataSave.currentHealth} PV et {dataSave.technology?.Count ?? 0} technologie(s).");
        applyEffectNuclearWaste();
        applyEffectPollution();

        Time.timeScale = 1f;
    }

    void Update()
    {
        tempsPasse += Time.deltaTime;


        if (tempsPasse >= explorationTime && isExplorationPhase)
        {
            audioSource.PlayOneShot(newWaveSound);
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

    bool IsValidSpawn(Vector3 pos)
    {
        float radius = 5f;  // Augmenté pour plus de sécurité
        int wallLayer = LayerMask.GetMask("wallLayer");
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, radius, wallLayer);
        
        if (hits.Length > 0)
        {
            Debug.LogWarning($"SPAWN INVALIDE! {hits.Length} murs à {pos.ToString("F2")}");
        }
        
        return hits.Length == 0;
    }


    void SpawnEnemies()
    {
        // ENEMIES TYPE 1
        for (int i = 0; i < enemiesToSpawn1; i++)
        {
            Vector3 spawnPos = Vector3.zero;
            int tries = 0;
            bool isValid = false;

            do
            {
                float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
                float distance = Random.Range(spawnRadiusMin1, spawnRadiusMax1);

                spawnPos = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;
                isValid = IsValidSpawn(spawnPos);
                tries++;

            } while (!isValid && tries < 20);

            if (isValid)
            {
                Instantiate(enemyPrefab1, spawnPos, Quaternion.identity);
            }
        }

        // ENEMIES TYPE 2
        for (int i = 0; i < enemiesToSpawn2; i++)
        {
            Vector3 spawnPos = Vector3.zero;
            int tries = 0;
            bool isValid = false;

            do
            {
                float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
                float distance = Random.Range(spawnRadiusMin2, spawnRadiusMax2);

                spawnPos = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;
                isValid = IsValidSpawn(spawnPos);
                tries++;

            } while (!isValid && tries < 20);

            if (isValid)
            {
                Instantiate(enemyPrefab2, spawnPos, Quaternion.identity);
            }
        }
    }
    void SpawnBoss()
    {
        Vector3 spawnPos = Vector3.zero;
        int tries = 0;
        bool isValid = false;

        do
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            spawnPos = player.position + new Vector3(
                Mathf.Cos(angle),
                Mathf.Sin(angle),
                0
            ) * spawnRadiusBoss;

            isValid = IsValidSpawn(spawnPos);
            tries++;

        } while (!isValid && tries < 20);

        if (isValid)
        {
            Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        }
    }

    void WinGame()
    {
        //Debug.Log("dataSave.level : " + dataSave.level  + " dataSave.endLevel : " + dataSave.endLevel);
        if (dataSave.level < dataSave.endLevel)
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
                // Debug.Log("Tech chargée : " + tech.getName());
                panel.setActualTechnology(tech);
                levelUpScript.updateTechnology(); // Forcer la mise à jour des technologies appliquées
            }
        }
    }

    private void WriteDataSave()
    {
        dataSave.currentHealth = player.GetComponent<PlayerHealth>().GetCurrentHealth();
        dataSave.level += 1;
        dataSave.technology = panel.getCurrentTechnology();

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

    private void applyEffectPollution()
    {
        float pollution = RessourceManager.Instance.GetPollution();
        int level = getPollutionLevel(pollution);
        // Debug.Log($"Niveau de pollution : {pollution}");
        // Debug.Log($"Niveau d'effet : {level}");

        switch (level)
        {
            case 11:
            // Effets de niveau 11 (au-delà de 100)
            // TODO : MORT
            case 10:
                increaseEnemyContactDamage(50f);
                increaseEnemyShootDamage(50f);
                increaseEnemyHealth(50f); // Augmente les PV des ennemis de 50%
                increaseEnemyNumber(50f);
                // TODO : + 50% vitesse ennemis
                goto case 9;
            case 9:
                increaseEnemyContactDamage(50f);
                increaseEnemyShootDamage(50f);
                // TODO : + 50% vitesse ennemis
                goto case 8;
            case 8:
                increaseEnemyNumber(50f); // Augmente le nombre d'ennemis de 50%
                increaseEnemyHealth(50f); // Augmente les PV des ennemis de 50%
                goto case 7;
            case 7:
                // TODO : + 75% vitesse ennemis
                goto case 6;
            case 6:
                increaseEnemyContactDamage(50f); // Augmente les dégâts de contact des ennemis de 50%
                increaseEnemyShootDamage(50f); // Augmente les dégâts de tir des ennemis
                increaseEnemyHealth(50f); // Augmente les PV des ennemis de 20%
                goto case 5;
            case 5:
                // TODO : Brouillard
                goto case 4;
            case 4:
                increaseEnemyNumber(50f); // Augmente le nombre d'ennemis de 50%
                goto case 3;
            case 3:
                increaseEnemyContactDamage(50f); // Augmente les dégâts de contact des ennemis de 50%
                increaseEnemyShootDamage(50f); // Augmente les dégâts de tir des ennemis de 50%
                goto case 2;
            case 2:
                // TODO : vitesse ennemy + 50 %
                goto case 1;
            case 1:
                increaseEnemyHealth(20f); // Augmente les PV des ennemis de 20%
                break;
        }
    }

    private void applyEffectNuclearWaste()
    {
        float nuclearWaste = RessourceManager.Instance.GetNuclearWaste();
    }

    private void increaseEnemyHealth(float pourcentage)
    {
        if (pourcentage <= 0f)
        {
            Debug.LogWarning("increaseEnemyHealth: le pourcentage doit être strictement positif.");
            return;
        }

        float multiplier = 1f + (pourcentage / 100f);
        int updatedCount = 0;

        bool TryIncreaseTargetHealth(GameObject target)
        {
            if (target == null)
            {
                return false;
            }
            // Debug.Log($"Tentative d'augmentation des PV pour {target.name}");

            colisionEnemy enemyCollision = target.GetComponentInChildren<colisionEnemy>(true);
            if (enemyCollision == null)
            {
                return false;
            }

            int oldPv = enemyCollision.pv;
            enemyCollision.pv = Mathf.Max(1, Mathf.CeilToInt(oldPv * multiplier));
            enemyCollision.maxPv = Mathf.Max(1, Mathf.CeilToInt(enemyCollision.maxPv * multiplier));
            return true;
        }

        if (TryIncreaseTargetHealth(enemyPrefab1)) updatedCount++;
        if (TryIncreaseTargetHealth(enemyPrefab2)) updatedCount++;
        if (TryIncreaseTargetHealth(bossPrefab)) updatedCount++;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (TryIncreaseTargetHealth(enemy))
            {
                updatedCount++;
            }
        }

        Debug.Log($"PV ennemis augmentés de {pourcentage}% sur {updatedCount} cible(s).");
    }

    private void increaseEnemyContactDamage(float pourcentage)
    {
        if (pourcentage <= 0f)
        {
            Debug.LogWarning("increaseEnemyContactDamage: le pourcentage doit être strictement positif.");
            return;
        }

        float multiplier = 1f + (pourcentage / 100f);
        int updatedCount = 0;

        int TryIncreaseTargetContactDamage(GameObject target)
        {
            if (target == null)
            {
                return 0;
            }

            colisionEnemy[] collisions = target.GetComponentsInChildren<colisionEnemy>(true);
            if (collisions == null || collisions.Length == 0)
            {
                return 0;
            }

            int localCount = 0;
            foreach (colisionEnemy collision in collisions)
            {
                int oldDamage = collision.damageContact;
                collision.damageContact = Mathf.Max(1, Mathf.CeilToInt(oldDamage * multiplier));
                localCount++;
            }

            return localCount;
        }

        updatedCount += TryIncreaseTargetContactDamage(enemyPrefab1);
        updatedCount += TryIncreaseTargetContactDamage(enemyPrefab2);
        updatedCount += TryIncreaseTargetContactDamage(bossPrefab);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            updatedCount += TryIncreaseTargetContactDamage(enemy);
        }

        Debug.Log($"Dégâts de contact augmentés de {pourcentage}% sur {updatedCount} composant(s) ennemi(s).");
    }

    private void increaseEnemyShootDamage(float pourcentage)
    {
        if (pourcentage <= 0f)
        {
            Debug.LogWarning("increaseEnemyShootDamage: le pourcentage doit être strictement positif.");
            return;
        }

        float multiplier = 1f + (pourcentage / 100f);
        int updatedCount = 0;

        int TryIncreaseTargetShootDamage(GameObject target)
        {
            if (target == null)
            {
                return 0;
            }

            shootEnemy[] weapons = target.GetComponentsInChildren<shootEnemy>(true);
            if (weapons == null || weapons.Length == 0)
            {
                return 0;
            }

            int localCount = 0;
            foreach (shootEnemy weapon in weapons)
            {
                int oldDamage = weapon.damage;
                weapon.damage = Mathf.Max(1, Mathf.CeilToInt(oldDamage * multiplier));
                localCount++;
            }

            return localCount;
        }

        updatedCount += TryIncreaseTargetShootDamage(enemyPrefab1);
        updatedCount += TryIncreaseTargetShootDamage(enemyPrefab2);
        updatedCount += TryIncreaseTargetShootDamage(bossPrefab);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            updatedCount += TryIncreaseTargetShootDamage(enemy);
        }

        Debug.Log($"Dégâts de tir augmentés de {pourcentage}% sur {updatedCount} arme(s) ennemie(s).");
    }

    private void increaseEnemyNumber(float pourcentage)
    {
        enemiesToSpawn1 = Mathf.CeilToInt(enemiesToSpawn1 * (1f + pourcentage / 100f));
        enemiesToSpawn2 = Mathf.CeilToInt(enemiesToSpawn2 * (1f + pourcentage / 100f));
        Debug.Log($"Nombre d'ennemis à spawn augmenté de {pourcentage}%. Nouveau nombre pour prefab1 : {enemiesToSpawn1}, prefab2 : {enemiesToSpawn2}.");
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

    /// <summary>
    /// Détermine le pallier de pollution (1-10) basé sur le niveau actuel.
    /// Les palliers sont rapprochés vers le haut.
    /// Seuils: 15, 28, 40, 50, 60, 68, 75, 82, 89, 96
    /// Écarts: 13, 12, 10, 10, 8, 7, 7, 7, 7 (se resserrent progressivement)
    /// </summary>
    public int getPollutionLevel(float pollutionValue)
    {
        // Debug.Log($"Calcul du niveau de pollution pour une valeur de {pollutionValue}");
        return pollutionValue switch
        {
            >= 100 => 10,
            >= 96 => 10,
            >= 89 => 9,
            >= 82 => 8,
            >= 75 => 7,
            >= 68 => 6,
            >= 60 => 5,
            >= 50 => 4,
            >= 40 => 3,
            >= 28 => 2,
            >= 15 => 1,
            _ => 0
        };
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

