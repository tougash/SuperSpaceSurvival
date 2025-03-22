using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform Player;
    public int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 5;

    
    void Start()
    {
        // Find the player by tag (make sure your player has the tag "Player")
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (Player == null)
        {
            Debug.LogError("Player not found.");
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            transform.position += new Vector3(transform.forward.x, 0f, transform.forward.z ) * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                // Here you can place the code for attacking player
            }
        }
        
    }
}