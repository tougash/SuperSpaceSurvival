using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLaser : MonoBehaviour
{
    [Header("Laser Variables")]
    public int laserDamage = 25;
    public bool firing = false;
    public GameObject laser;
    public Slider meter;
    [SerializeField] private bool overheated = false;
    public int maxHeat = 100;
    public int heatIncrease = 10;
    [SerializeField]private int currentHeat = 0;
    private float fireInterval = 0.5f;
    public PlayerStats stats;


    private void Update()
    {
        if(Input.GetButtonDown("Fire1") && !firing && !overheated)
        {
            Debug.Log("X");
            Shoot();
        }
        else if (Input.GetButtonUp("Fire1") && firing)
        {
            Stop();
        }

        if(currentHeat > maxHeat)
        {
            overheated = true;
            StartCoroutine("laserCooldown");
            Stop();
        }

        if(meter.value != currentHeat)
        {
            meter.value = currentHeat;
        }
    }

    void Shoot()
    {
        Debug.Log("Y");
        laser.SetActive(true);
        currentHeat += heatIncrease;
        firing = true;
        StopCoroutine("laserCooldown");
        StartCoroutine("laserHeating");
    }

    void Stop()
    {
        laser.SetActive(false);
        firing = false;
        StopCoroutine("laserHeating");
        StartCoroutine("laserCooldown");
    }

    IEnumerator laserCooldown()
    {
        if(overheated)
        {
            while(overheated)
            {
                if(currentHeat == 0)
                {
                    overheated = false;
                    break;
                }
                else
                {
                    currentHeat -=heatIncrease;
                    yield return new WaitForSeconds(fireInterval*1.5f);
                }
            }
        }
        else
        {
            while(currentHeat != 0)
            {
                currentHeat -=heatIncrease;
                yield return new WaitForSeconds(fireInterval/2);
            }
        }
        
    }

    IEnumerator laserHeating()
    {
        while(firing && !overheated)
        {
            currentHeat += heatIncrease;
            yield return new WaitForSeconds(fireInterval);
        }
    }
}
