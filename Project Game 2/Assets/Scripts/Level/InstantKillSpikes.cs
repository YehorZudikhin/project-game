using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKillSpikes : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {       
            player.GetComponent<Health>().TakeDamage(150);
        }
    }
}
