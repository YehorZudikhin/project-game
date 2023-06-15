using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public GameObject player;

    public float damageCooldown = 0.3f;  // Cooldown between each damage instance
    private float lastDamageTime;     // Time when the last damage was applied


    // Start is called before the first frame update
    private void Start()
    {
        lastDamageTime = -Mathf.Infinity;  // Initialize lastDamageTime to a very small value
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Time.time - lastDamageTime >= damageCooldown)
        {       
            player.GetComponent<Health>().TakeDamage(10);
            lastDamageTime = Time.time;  // Update the last damage time
        }
    }
}
