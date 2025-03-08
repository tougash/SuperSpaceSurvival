using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAttack : MonoBehaviour
{
    public int damage = 15;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealthController playerHealth = other.gameObject.GetComponent<PlayerHealthController>();
            playerHealth.TakeDamage(damage);
        }
    }
}
