using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public GameObject Boss;
    public float BossSpawnTime = 90f;
    public float levelTimeCounter;
    [Space(5)]
    public float EnemySpawnRate = 3f;
    float enemySpawnerCounter;
    [Space(5)]
    public int MaxEnemiesInScene = 5;
    public int CurrentEnemiesInScene;
    [Space(5)]
    public GameObject[] MediumEnemies;
    [Space(5)]
    public GameObject[] BasicEnemies;
    [Space(5)]
    public Transform[] EnemySpawnPointsUP;
    public Transform BossSpawnPointUP;
    [Space(5)]
    public Transform[] EnemySpawnPointsDOWN;
    public Transform BossSpawnPointDOWN;
    [Space(5)]
    public Transform[] EnemySpawnPointsRIGHT;
    public Transform BossSpawnPointRIGHT;
    [Space(5)]
    public Transform[] EnemySpawnPointsLEFT;
    public Transform BossSpawnPointLEFT;
    [Space(5)]
    public float CurrentSpiralAngle;

    void Awake() 
    {
        instance = this;
    }

    void Update()
    {
        if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.UP) 
        {
            enemySpawnerCounter += Time.deltaTime;
            levelTimeCounter += Time.deltaTime;
            if (levelTimeCounter < BossSpawnTime && CurrentEnemiesInScene < MaxEnemiesInScene) 
            {
                if(levelTimeCounter < BossSpawnTime / 2) 
                {
                    foreach(GameObject enemy in BasicEnemies) 
                    {
                        if (!enemy.activeInHierarchy && enemySpawnerCounter >= EnemySpawnRate) 
                        {
                            enemy.transform.position = EnemySpawnPointsUP[UnityEngine.Random.Range(0, EnemySpawnPointsUP.Length -1)].position;
                            enemy.SetActive(true);
                            enemySpawnerCounter = 0;
                        }
                    }
                }
                else 
                {
                    foreach (GameObject enemy in MediumEnemies) 
                    {
                        if (!enemy.activeInHierarchy && enemySpawnerCounter >= EnemySpawnRate)
                        {
                            enemy.transform.position = EnemySpawnPointsUP[UnityEngine.Random.Range(0, EnemySpawnPointsUP.Length - 1)].position;
                            enemy.SetActive(true);
                            enemySpawnerCounter = 0;
                        }
                    }
                }
            }
            else if (levelTimeCounter >= BossSpawnTime) 
            {
                enemySpawnerCounter = 0;
                levelTimeCounter = BossSpawnTime;
                Boss.transform.position = BossSpawnPointUP.position;
                Boss.SetActive(true);
            }
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.DOWN) 
        {
            enemySpawnerCounter += Time.deltaTime;
            levelTimeCounter += Time.deltaTime;
            if (levelTimeCounter < BossSpawnTime && CurrentEnemiesInScene < MaxEnemiesInScene)
            {
                if (levelTimeCounter < BossSpawnTime / 2)
                {
                    foreach (GameObject enemy in BasicEnemies)
                    {
                        if (!enemy.activeInHierarchy && enemySpawnerCounter >= EnemySpawnRate)
                        {
                            enemy.transform.position = EnemySpawnPointsDOWN[UnityEngine.Random.Range(0, EnemySpawnPointsDOWN.Length - 1)].position;
                            enemy.SetActive(true);
                            enemySpawnerCounter = 0;
                        }
                    }
                }
                else
                {
                    foreach (GameObject enemy in MediumEnemies)
                    {
                        if (!enemy.activeInHierarchy && enemySpawnerCounter >= EnemySpawnRate)
                        {
                            enemy.transform.position = EnemySpawnPointsDOWN[UnityEngine.Random.Range(0, EnemySpawnPointsDOWN.Length - 1)].position;
                            enemy.SetActive(true);
                            enemySpawnerCounter = 0;
                        }
                    }
                }
            }
            else if (levelTimeCounter >= BossSpawnTime)
            {
                enemySpawnerCounter = 0;
                levelTimeCounter = BossSpawnTime;
                Boss.transform.position = BossSpawnPointDOWN.position;
                Boss.SetActive(true);
            }
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.RIGHT)
        {
            enemySpawnerCounter += Time.deltaTime;
            levelTimeCounter += Time.deltaTime;
            if (levelTimeCounter < BossSpawnTime && CurrentEnemiesInScene < MaxEnemiesInScene)
            {
                if (levelTimeCounter < BossSpawnTime / 2)
                {
                    foreach (GameObject enemy in BasicEnemies)
                    {
                        if (!enemy.activeInHierarchy && enemySpawnerCounter >= EnemySpawnRate)
                        {
                            enemy.transform.position = EnemySpawnPointsRIGHT[UnityEngine.Random.Range(0, EnemySpawnPointsRIGHT.Length - 1)].position;
                            enemy.SetActive(true);
                            enemySpawnerCounter = 0;
                        }
                    }
                }
                else
                {
                    foreach (GameObject enemy in MediumEnemies)
                    {
                        if (!enemy.activeInHierarchy && enemySpawnerCounter >= EnemySpawnRate)
                        {
                            enemy.transform.position = EnemySpawnPointsRIGHT[UnityEngine.Random.Range(0, EnemySpawnPointsRIGHT.Length - 1)].position;
                            enemy.SetActive(true);
                            enemySpawnerCounter = 0;
                        }
                    }
                }
            }
            else if (levelTimeCounter >= BossSpawnTime)
            {
                enemySpawnerCounter = 0;
                levelTimeCounter = BossSpawnTime;
                Boss.transform.position = BossSpawnPointRIGHT.position;
                Boss.SetActive(true);
            }
        }
        else if (FakeLevelManager.instance.currentLevel == FakeLevelManager.LevelState.LEFT)
        {
            enemySpawnerCounter += Time.deltaTime;
            levelTimeCounter += Time.deltaTime;
            if (levelTimeCounter < BossSpawnTime && CurrentEnemiesInScene < MaxEnemiesInScene)
            {
                if (levelTimeCounter < BossSpawnTime / 2)
                {
                    foreach (GameObject enemy in BasicEnemies)
                    {
                        if (!enemy.activeInHierarchy && enemySpawnerCounter >= EnemySpawnRate)
                        {
                            enemy.transform.position = EnemySpawnPointsLEFT[UnityEngine.Random.Range(0, EnemySpawnPointsLEFT.Length - 1)].position;
                            enemy.SetActive(true);
                            enemySpawnerCounter = 0;
                        }
                    }
                }
                else
                {
                    foreach (GameObject enemy in MediumEnemies)
                    {
                        if (!enemy.activeInHierarchy && enemySpawnerCounter >= EnemySpawnRate)
                        {
                            enemy.transform.position = EnemySpawnPointsLEFT[UnityEngine.Random.Range(0, EnemySpawnPointsLEFT.Length - 1)].position;
                            enemy.SetActive(true);
                            enemySpawnerCounter = 0;
                        }
                    }
                }
            }
            else if (levelTimeCounter >= BossSpawnTime)
            {
                enemySpawnerCounter = 0;
                levelTimeCounter = BossSpawnTime;
                Boss.transform.position = BossSpawnPointLEFT.position;
                Boss.SetActive(true);
            }
        }
        else 
        {
            Boss.SetActive(false);
            enemySpawnerCounter = 0f;
            levelTimeCounter = 0;

            foreach (GameObject enemy in BasicEnemies) 
            {
                enemy.SetActive(false);
            }
            foreach (GameObject enemy in MediumEnemies) 
            {
                enemy.SetActive(false);
            }
        }
    }
}