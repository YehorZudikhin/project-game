using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int basicAttackDamage = 20;

    public float basicAttackRate = 1f;  ///  ( 1 / attackRate )
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("Player Pressed K.");
                
                BasicAttack();
                nextAttackTime = Time.time + 1f / basicAttackRate;

                Debug.Log("Attack function ended.");
            }
        }
        
    }

    void BasicAttack()
    {
        // Play an attack animation
        animator.SetTrigger("BasicAttack");

        SoundManagerScript.PlaySound("Attack");

        // Detect enemies in range of attacak
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Debug.Log("Collider2D was filled with " + hitEnemies.Length + " enemies");

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<Health>() != null)
            {
                Debug.Log("We hit " + enemy.name + ".");
                enemy.GetComponent<Health>().TakeDamage(basicAttackDamage);
            }
            
        }
    }

    void OnDrawGizmosSelected()
    {

        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Death()
    {
        Debug.Log("The Player died!");
        ///TODO:
        /// Disable the player and their physics,
        /// Open Game Over menu screen and respawn if player wants to retry
        
        //GetComponent<Collider2D>().enabled = false;
    }
}