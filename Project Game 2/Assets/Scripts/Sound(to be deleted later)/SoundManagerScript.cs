using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] AttackSounds;
    [SerializeField] private AudioClip[] CollectCoinSounds;
    [SerializeField] private AudioClip[] JumpSounds;
    [SerializeField] private AudioClip[] EnemyDeathSounds;

    private static AudioClip AttackSound, CollectCoinSound, JumpSound, EnemyDeathSound;
    static AudioSource audioSrc;

    void Start()
    {
        AttackSound = Resources.Load <AudioClip> ("Attack");
        CollectCoinSound = Resources.Load <AudioClip> ("CollectCoin");
        JumpSound = Resources.Load <AudioClip> ("Jump");
        EnemyDeathSound = Resources.Load <AudioClip> ("EnemyDeath");
        
        audioSrc = GetComponent <AudioSource> ();
    }

    public static void PlaySound (string clip)
    {
        switch (clip) {

        case "Attack":
            audioSrc.PlayOneShot (AttackSound);
            break;

        case "CollectCoin":
            audioSrc.PlayOneShot (CollectCoinSound);
            break;

        case "JumpSounds":
            audioSrc.PlayOneShot (JumpSound);
            break;

        case "EnemyDeath":
            audioSrc.PlayOneShot (EnemyDeathSound);
            break;
        }
    }
    /*
    public static void PlaySoundLocal(string clip, Vector2 position)
    {
        if (attack2Sounds.Length > 0)
        {
            AudioSource.PlayClipAtPoint(
                attack2Sounds[Random.Range(0, attack2Sounds.Length - 1)], //Which sound effect - (a random one)
                transform.position  //2D location from where it's heard
            );
        }
    }
    */
}
