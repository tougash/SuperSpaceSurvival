using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [Header("Bullet Variables")]
    public float laserDamage;
    public bool firing = false;
    public GameObject laser;

    

    private void Update()
    {
        if(Input.GetButtonDown("Fire1") && !firing)
        {
            Shoot();
        }
        else if (Input.GetButtonUp("Fire1") && firing)
        {
            Stop();
        }
    }

    void Shoot()
    {
        laser.SetActive(true);
        firing = true;
    }

    void Stop()
    {
        laser.SetActive(false);
        firing = false;
    }

}
