using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaneGeneration : MonoBehaviour
{
    public GameObject plane;
    public GameObject player;
    private List<GameObject> pool;
    private int radius = 4;
    private int planeOffset = 50;

    private Vector3 startPos = Vector3.zero;

    // Find the players X and Z movement direction
    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    // Find the players X and Z location
    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x/planeOffset)*planeOffset;
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z/planeOffset)*planeOffset;

    private Hashtable tilePlane = new Hashtable();

    float min_x,max_x,min_z,max_z;

    private void Awake() {
        // Generate the initial planes in x and z direction using the radius
            for(int x = -radius; x < radius; x++)
            {
                for(int z = -radius; z< radius; z++)
                {
                    // Determine position for the tile
                    Vector3 pos = new Vector3((x*planeOffset + XPlayerLocation), 0.5f, (z*planeOffset + ZPlayerLocation));
                    max_x = (pos.x > max_x) ? pos.x : max_x;
                    min_x = (pos.x < min_x) ? pos.x : min_x;
                    max_z = (pos.z > max_z) ? pos.z : max_z;
                    min_z = (pos.x < min_z) ? pos.z : min_z;
                    // If the tile isnt in the hashtable create a plane and add it to the hashtable
                    if(!tilePlane.Contains(pos))
                    {
                        GameObject _plane = Instantiate(plane, pos, Quaternion.identity, gameObject.transform);
                        tilePlane.Add(pos,_plane);
                    }
                }
            }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > max_x )
        {
            Vector3 newPos = player.transform.position;
            newPos.x = min_x;
            player.transform.position = newPos;
        }
        else if (player.transform.position.x < min_x ) 
        {
            Vector3 newPos = player.transform.position;
            newPos.x = max_x;
            player.transform.position = newPos;
        }
        if(player.transform.position.z > max_z )
        {
            Vector3 newPos = player.transform.position;
            newPos.z = min_z;
            player.transform.position = newPos;
        }
        else if (player.transform.position.z < min_z ) 
        {
            Vector3 newPos = player.transform.position;
            newPos.z = max_z;
            player.transform.position = newPos;
        }
    }
}
