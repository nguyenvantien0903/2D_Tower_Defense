using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;//so luong quai khi de nhat
    [SerializeField] private float enemiesPerSecondBase = 1f;
    [SerializeField] private float enemiesPerSecondMax = 15f;
    [SerializeField] private float timeBetweenWaves = 2f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    // Start is called before the first frame update

    [Header("Events")]
    public static UnityEvent OnEnemyDestroy = new();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float enemiesPerSecondEachWay;//enemy per second
    private bool isSpawning = false;

    private void Awake()
    {
        OnEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= (1f / enemiesPerSecondEachWay) && enemiesLeftToSpawn > 0){
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn= 0f;
        }
        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        isSpawning= false;
        timeSinceLastSpawn= 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        int index = 0;
        if(currentWave <= 5)
        {
            index = UnityEngine.Random.Range(1, enemyPrefabs.Length);
        }
        else
        {
            index = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        }
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn,LevelManager.instance.startPoint.position,Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies* Mathf.Pow(currentWave,difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecondBase * Mathf.Pow(currentWave, difficultyScalingFactor),0f,enemiesPerSecondMax);
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning= true;
        enemiesLeftToSpawn = EnemiesPerWave();
        enemiesPerSecondEachWay = EnemiesPerSecond();
    }

}
