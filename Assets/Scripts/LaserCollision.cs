using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    public PlayerLaser laserScript;
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealthController kController = other.gameObject.GetComponent<EnemyHealthController>();
        if(kController)
        {
            kController.takeDamage(laserScript.laserDamage);
            Debug.Log("Dealt Damage");
        }
    }
}
