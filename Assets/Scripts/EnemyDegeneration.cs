using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDegeneration : MonoBehaviour
{
    private bool hasBeenVisible = false;
    private Camera cam;

    void Start()
    {
        cam = Camera.main; // Only check against the main gameplay camera
    }

    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);

        // Check if enemy is in front of the camera and within view
        bool onScreen = viewPos.z > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;

        if (!hasBeenVisible && onScreen)
        {
            hasBeenVisible = true;
        }

        // If it was visible and now offscreen, destroy it
        if (hasBeenVisible && !onScreen)
        {
            Destroy(gameObject);
            EnemyGeneration.DecrementSpawn();
        }
        Debug.DrawLine(transform.position, cam.transform.position);
    }
}