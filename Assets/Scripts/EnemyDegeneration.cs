using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDegeneration : MonoBehaviour
{
    private Vector2 screenBounds;
    private bool boundsFlag = false;

    void Start()
    {
        // Calculate once on Start (assuming the screen size does not change dynamically)
        screenBounds = new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize);
    }

    void Update()
    {
        Transform cameraPivot = Camera.main.transform.parent; // Get the CameraPivot
        if (cameraPivot == null) return;

        Vector2 camPos = cameraPivot.position; // Get CameraPivot's position

        if (!boundsFlag && transform.position.x > camPos.x - screenBounds.x && transform.position.x < camPos.x + screenBounds.x &&
            transform.position.y > camPos.y - screenBounds.y && transform.position.y < camPos.y + screenBounds.y)
        {
            boundsFlag = true;
        }

        if (boundsFlag && (transform.position.x < camPos.x - screenBounds.x || transform.position.x > camPos.x + screenBounds.x ||
                        transform.position.y < camPos.y - screenBounds.y || transform.position.y > camPos.y + screenBounds.y))
        {
            Destroy(gameObject);
        }
    }
}
