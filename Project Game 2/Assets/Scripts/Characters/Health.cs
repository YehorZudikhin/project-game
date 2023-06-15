using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //--------------------------------------------------
    // References
    [SerializeField] private Animator animator;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    //public bool isDead;
    //--------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //isDead = false;
    }

    /*
    // Update is called once per frame
    void Update()
    {

    }
    */

    public void TakeDamage(int damage)
    {
        //Reduce current Health
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage.");
        //Play hurt animation
        animator.SetTrigger("Hurt");
        //Check if character died
        if (currentHealth <= 0)
        {
            Die();
        }

    }


    public void HealDamage(int heal)
    {
        // Heal the character
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; //currentHealth is capped to maxHealth
        }
        Debug.Log(gameObject.name + " received " + heal + " health.");
    }
    /* /glitch safer variant
    public void HealDamage(int heal)
    {
        if ((currentHealth + heal) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += heal;
        }
    }
    */

    public void Die()
    {
        Debug.Log(gameObject.name + "died.");
        // Death animation
        animator.SetBool("IsDead", true);
        /// NOTE: death animation has to contain an event trigger,
        // which runs the personal Death() function in the respective combat script
        // This is so health and everything that all characters do is centralized here,
        // but individual unique treatment are handled by the combat script,
        // because it is unique for every character.
        // Player, enemies, bosses die in different ways.
    }

    // ========== Start of: Sound Effect Helper Functions ==========
    //individualizing with combat scripts
    // ========== End of: Sound Effect Helper Functions ==========

}
