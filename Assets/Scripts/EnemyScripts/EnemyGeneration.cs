using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
     public GameObject enemyPrefab; // Reference to the enemy prefab

    public float spawnCountDown = 2f; // Time before initial spawns
    public int minSpawns = 50;
    public int maxSpawns = 100;

    public static int currentSpawns = 0;

    public Transform cameraPivot; // Reference to the pivot
    private Camera cam;

    private Queue<GameObject> enemyPool = new Queue<GameObject>(); // Object pool
    private float orthographicSize;
    private float screenAspect;

    void Start()
    {
        cam = Camera.main;
        orthographicSize = cam.orthographicSize;
        screenAspect = cam.aspect;
        InitializePool(maxSpawns);
        StartCoroutine(SpawnEnemiesRoutine());
    }

    void InitializePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false); // Disable the enemy initially
            enemyPool.Enqueue(enemy); // Add it to the pool
        }
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        yield return new WaitForSeconds(spawnCountDown); // Wait for __ seconds before starting spawns

            // Spawn enemies based on the current spawn count
        while (true) // Runs indefinitely
            {
            
                if (currentSpawns < minSpawns)
                {
                    SpawnEnemy();
                    SpawnEnemy();
                    SpawnEnemy();
                    SpawnEnemy();
                }
                else if (currentSpawns >= minSpawns && currentSpawns < maxSpawns)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(1f); // Wait for 1 seconds before checking again
                }

                Debug.Log("Current Spawns: " + currentSpawns);
                yield return new WaitForSeconds(0.5f); // Wait for 2 seconds before checking again
            }
    }

    void SpawnEnemy()
    {   
        if (enemyPool.Count == 0) return;

        GameObject enemy = enemyPool.Dequeue();
        enemy.SetActive(true);

        // Get camera world bounds relative to cameraPivot
        Vector3 pivotPosition = cameraPivot.position; // Use assigned pivot
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;


        float spawnBuffer = 4f; // Ensures enemies spawn fully outside view

        float x, z;
        bool spawnOnX = Random.value > 0.5f;

        if (spawnOnX)
        {
            // Spawn outside left or right of the cameraPivot
            x = Random.value > 0.5f ? pivotPosition.x + camWidth + spawnBuffer : pivotPosition.x - camWidth - spawnBuffer;
            z = Random.Range(pivotPosition.z - camHeight, pivotPosition.z + camHeight);
        }
        else
        {
            // Spawn outside top or bottom of the cameraPivot
            x = Random.Range(pivotPosition.x - camWidth, pivotPosition.x + camWidth);
            z = Random.value > 0.5f ? pivotPosition.z + camHeight + spawnBuffer : pivotPosition.z - camHeight - spawnBuffer;
        }

        // Spawn enemy outside camera view but relative to cameraPivot
        enemy.transform.position = new Vector3(x, 1, z);
        currentSpawns++;
    }



    public void DeactivateEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy); // Return it to the pool
        currentSpawns--;
    }

}
