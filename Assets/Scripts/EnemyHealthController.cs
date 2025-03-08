using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyHealthController : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive())
        {
            Die();
        }
    }

    bool isAlive()
    {
        return currentHealth>0;
    }
    void Die()
    {
        Destroy(gameObject);
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
