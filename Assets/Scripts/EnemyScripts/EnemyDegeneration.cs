using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDegeneration : MonoBehaviour
{
    private bool hasBeenVisible = false;
    private Camera cam;

    private float buffer = 0.5f; // Buffer to ensure the enemy is completely offscreen

    private EnemyGeneration spawner; // Reference to the spawner

    void Start()
    {
        cam = Camera.main; // Only check against the main gameplay camera
    }

    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);

        // Check if enemy is in front of the camera and within view
        bool onScreen = viewPos.z > 0 && 
                        viewPos.x > -buffer && viewPos.x < 1 + buffer && 
                        viewPos.y > -buffer && viewPos.y < 1 + buffer;

        if (!hasBeenVisible && onScreen)
        {
            hasBeenVisible = true;
        }

        // If it was visible and now offscreen, deactivate it
        if (hasBeenVisible && !onScreen)
        {
            hasBeenVisible = false; // Reset for next time it goes offscreen
            FindObjectOfType<EnemyGeneration>().DeactivateEnemy(gameObject); // Deactivate the enemy
        }
        Debug.DrawLine(transform.position, cam.transform.position);
    }
}