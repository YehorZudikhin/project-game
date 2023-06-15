using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    /*
    // Update is called once per frame
    void Update()
    {

    }
    */

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + ".");

        //Play hurt animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
            SoundManagerScript.PlaySound("EnemyDeath");
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        // Die animation
        animator.SetBool("IsDead", true);

        // Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

}
