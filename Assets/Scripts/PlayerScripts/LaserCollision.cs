using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    public PlayerLaser laserScript;
    IEnumerator OnTriggerStay(Collider other)
    {
        yield return new WaitForSeconds(0.25f);
        EnemyHealthController kController = other.gameObject.GetComponent<EnemyHealthController>();
        if(kController != null)
        {
            kController.takeDamage(laserScript.laserDamage + (5*laserScript.stats.getSpeedMod()));
        }
    }
}
