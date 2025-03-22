using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
     public GameObject enemyPrefab; // Reference to the enemy prefab

    private float minSpawns = 50;
    private float maxSpawns = 100;

    public static int currentSpawns = 0;
    private Vector2 screenBounds;

    void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds before spawning starts
        while (true) // Runs indefinitely
            {
            
                if (currentSpawns < minSpawns)
                {
                    SpawnEnemy();
                    currentSpawns++;
                }
                else if (currentSpawns >= minSpawns && currentSpawns < maxSpawns)
                {
                    SpawnEnemy();
                    currentSpawns++;
                    yield return new WaitForSeconds(1f); // Wait for 1 seconds before checking again
                }

                Debug.Log("Current Spawns: " + currentSpawns);
                yield return new WaitForSeconds(0.5f); // Wait for 2 seconds before checking again
            }
    }

    void SpawnEnemy()
    {   
        // Get the screen bounds for the orthographic camera
        Camera camera = Camera.main;
        float screenHeight = camera.orthographicSize * 2; // Full height of the camera view
        float screenWidth = screenHeight * camera.aspect; // Width is calculated from height and aspect ratio

 
        // Generate a random position outside the screen
        Vector3 randomPosition = new Vector3(
            Random.Range(-screenWidth, screenWidth), // Random X position outside the screen width
            1, // The Y position (assuming enemies spawn at a fixed height above the ground)
            Random.Range(-screenHeight, screenHeight) // Random Z position outside the screen height
        );

        // Instantiate the enemy prefab at the random position
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }


    public static void DecrementSpawn()
    {
        currentSpawns--; // Modify the shared counter
    }

}
