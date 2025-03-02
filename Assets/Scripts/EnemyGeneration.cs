using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
     public GameObject enemyPrefab; // Reference to the enemy prefab
    public float spawnInterval = 5f; // Time interval to spawn enemies

    private Vector2 screenBounds;
    private int buffer = 5;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval); // Start spawning enemies every 5 seconds
    }

    void SpawnEnemy()
    {
        // Get the screen bounds
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Generate a random position outside the screen
        Vector3 randomPosition = new Vector3(Random.Range(-screenBounds.x-buffer, screenBounds.x+buffer), 1, Random.Range(-screenBounds.y-buffer, screenBounds.y+buffer));

        // Instantiate the enemy prefab at the random position
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }

}
