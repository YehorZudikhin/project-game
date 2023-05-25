using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    
    // Update is called once per frame
    void Update()
    {
        /*  //debug
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            HealDamage(10);
        }
        */
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void HealDamage(int heal)
    {
        if((currentHealth + heal) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += heal;
        }
        healthBar.SetHealth(currentHealth);
    }
}