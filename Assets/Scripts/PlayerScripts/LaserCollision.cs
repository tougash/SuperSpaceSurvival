using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    public PlayerLaser laserScript;
    private void OnTriggerStay(Collider other)
    {
        EnemyHealthController kController = other.gameObject.GetComponent<EnemyHealthController>();
        if(kController)
        {
            kController.takeDamage(laserScript.laserDamage + (5*laserScript.stats.getSpeedMod()));
            Debug.Log("Dealt Damage");
        }
    }
}
