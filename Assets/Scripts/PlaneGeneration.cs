using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaneGeneration : MonoBehaviour
{
    public GameObject plane;
    public GameObject player;

    private int radius = 2;
    private int planeOffset = 50;

    private Vector3 startPos = Vector3.zero;

    // Find the players X and Z movement direction
    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    // Find the players X and Z location
    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x/planeOffset)*planeOffset;
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z/planeOffset)*planeOffset;

    private Hashtable tilePlane = new Hashtable();


    // Update is called once per frame
    void Update()
    {
        // Only do this if the player has not moved
        if(startPos == Vector3.zero)
        {
            // Generate the initial planes in x and z direction using the radius
            for(int x = -radius; x < radius; x++)
            {
                for(int z = -radius; z< radius; z++)
                {
                    // Determine position for the tile
                    Vector3 pos = new Vector3((x*planeOffset + XPlayerLocation), 0, (z*planeOffset + ZPlayerLocation));
                    // If the tile isnt in the hashtable create a plane and add it to the hashtable
                    if(!tilePlane.Contains(pos))
                    {
                        GameObject _plane = Instantiate(plane,pos,Quaternion.identity, this.transform);
                        tilePlane.Add(pos,_plane);
                    }
                }
            }
        }

        // check if the player has moved enough to warrant generating a new plane
        if(hasPlayerMoved())
        {
            // generate planes in the raidus around the player
            for(int x = -radius; x < radius; x++)
            {
                for(int z = -radius; z< radius; z++)
                {
                    // Determine position for the tilet
                    Vector3 pos = new Vector3((x*planeOffset + XPlayerLocation), 0, (z*planeOffset + ZPlayerLocation));
                    // If the tile isnt in the hashtable create a plane and add it to the hashtable
                    if(!tilePlane.Contains(pos))
                    {
                        GameObject _plane = Instantiate(plane,pos,Quaternion.identity);
                        tilePlane.Add(pos,_plane);
                    }
                }
            }
        }
    }

    bool hasPlayerMoved()
    {
        if(Mathf.Abs(XPlayerMove) >= planeOffset || Mathf.Abs(ZPlayerMove) >= planeOffset)
        {
            return true;
        }
        return false;
    }
}
