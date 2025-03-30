using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int detontationTime = 2;
    [SerializeField] float remainingTime;
    [SerializeField] float radius = 1;
    [SerializeField] int damage = 200;
    public GameObject explosion;
    void Start()
    {
        remainingTime = detontationTime;
        StartCoroutine("GrenadeCooking");
    }

    IEnumerator GrenadeCooking()
    {
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
        while(remainingTime > 0)
        {
            yield return new WaitForSeconds(1);
            remainingTime -=1;
            gameObject.GetComponent<Rigidbody>().velocity *= 0.5f;
        }
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        explosion.SetActive(true);
        Collider[] enemiesHit = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider enemy in enemiesHit)
        {
            EnemyHealthController enemyController = enemy.transform.GetComponent<EnemyHealthController>();
            if(enemyController != null)
            {
                enemyController.takeDamage(damage);
            }
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    
}
