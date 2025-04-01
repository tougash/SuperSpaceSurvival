using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
     public GameObject enemyPrefab; // Reference to the enemy prefab

    public float spawnCountDown = 3.5f; // Time before initial spawns
    public int MINSPAWNS = 3;
    public int MAXSPAWNS = 150;

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
        InitializePool(MAXSPAWNS); 
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
        float spawnInterval = 0.5f; // Initial spawn interval
        yield return new WaitForSeconds(spawnCountDown); // Wait for __ seconds before starting spawns

        // Spawn enemies based on the current spawn count
        while (true) // Runs indefinitely
            {   
                int minutesPassed = Mathf.FloorToInt(Time.time / 60); // Get elapsed minutes
                int spawnAmount = Mathf.Clamp(MINSPAWNS + (minutesPassed * 3), MINSPAWNS, MAXSPAWNS); // Scale spawn count
                
                // Reduce spawn interval over time, minimum 1 second
                if (minutesPassed >= 1)
                {
                    spawnInterval = Mathf.Clamp(8f - (minutesPassed * 0.5f), 1f, 8f);
                     for (int i = 0; i < spawnAmount; i++)
                    {
                        SpawnEnemy();
                        yield return null; // Wait one frame before spawning the next enemy
                    }
                }
                else
                {
                    SpawnEnemy(); // Spawn a single enemy
                }

                //Debug.Log("Current Spawns: " + currentSpawns);
                yield return new WaitForSeconds(spawnInterval); // Wait for the spawn interval before checking again
            }
    }

    void SpawnEnemy()
    {   
        if (enemyPool.Count == 0) return;

        GameObject enemy = enemyPool.Dequeue();
        enemy.SetActive(true);

        // Ensure health is reset
        EnemyHealthController healthController = enemy.GetComponent<EnemyHealthController>();
        if (healthController != null)
        {
            healthController.resetHealth();
        }

        // Get camera world bounds relative to cameraPivot
        Vector3 pivotPosition = cameraPivot.position; // Use assigned pivot
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;


        float spawnBuffer = 6f; // Ensures enemies spawn fully outside view

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
