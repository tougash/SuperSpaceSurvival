using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{

    public Image healthBar;
    public float healthAmount = 100f;
    int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthAmount <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount/100f;
    }

    public void Heal(float healAmount)
    {
        healthAmount+= healAmount;
        healthAmount = Mathf.Clamp(healAmount, 0, maxHealth);
        healthBar.fillAmount = healthAmount/100f;
    }

    void Die()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
