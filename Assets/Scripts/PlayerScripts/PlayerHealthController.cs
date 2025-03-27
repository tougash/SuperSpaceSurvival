using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public GameObject healthBar;
    public Image currentHealth;
    public float healthAmount = 100f;
    int maxHealth = 100;
    public PlayerStats stats;

    // Update is called once per frame
    void Update()
    {
        if (PauseBehaviour.instance.GetIsPaused()) { return; }
        if(healthAmount <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        currentHealth.fillAmount = healthAmount/100f;
    }

    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth);
        currentHealth.fillAmount = healthAmount/100f;
    }

    void Die()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void updateMaxHealth() {
        maxHealth = 100 + (10*stats.getHealthMod());
        Heal(10*stats.getHealthMod());
    }

}
