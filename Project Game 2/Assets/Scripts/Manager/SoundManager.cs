using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // =//=//=//=//=//= Start of: SoundManager Class =//=//=//=//=//=

    // Singleton - one instance to rule them all
    static SoundManager soundManager;
    public static SoundManager Instance
    {
        get { return soundManager; }
    }

    [Header("Background Music")]
    [SerializeField] private AudioClip fullBGM;
    [SerializeField] private AudioClip startBGM;
    [SerializeField] private AudioClip loopBGM;
    [SerializeField] private AudioClip endBGM;
    [SerializeField] private AudioClip gameOverBGM;
    [SerializeField] bool use_full_BGM;

    [Header("Player")]
    [SerializeField] private AudioClip[] playerBasicAttacks;
    [SerializeField] private AudioClip[] playerSlideGrounds;
    [SerializeField] private AudioClip[] playerDashAttacks;
    [SerializeField] private AudioClip[] playerDashes;
    [SerializeField] private AudioClip[] playerDeaths;
    [SerializeField] private AudioClip[] playerJumps;
    [SerializeField] private AudioClip[] playerDoubleJumps;
    [SerializeField] private AudioClip[] playerLandings;
    [SerializeField] private AudioClip[] playerHits;
    [SerializeField] private AudioClip[] playerHeals;

    [Header("Skeleton_A")]
    [SerializeField] private AudioClip[] skeletonAttacks1;
    [SerializeField] private AudioClip[] skeletonAttacks2;
    [SerializeField] private AudioClip[] skeletonHits;
    [SerializeField] private AudioClip[] skeletonDeaths;
    [SerializeField] private AudioClip[] skeletonHeals;

    [Header("Scene")]
    [SerializeField] private AudioClip collectCoin;

    [Header("Menu")]
    [SerializeField] private AudioClip buttonPress;
    [SerializeField] private AudioClip launch;
    [SerializeField] private AudioClip quit;

    [Header("Audio Mixer Groups")]
    [SerializeField] private AudioMixerGroup playerGroup;
    [SerializeField] private AudioMixerGroup enemyGroup;
    [SerializeField] private AudioMixerGroup sceneGroup;
    [SerializeField] private AudioMixerGroup menuGroup;
    [SerializeField] private AudioMixerGroup musicGroup;

    AudioSource playerSource;
    AudioSource enemySource;
    AudioSource sceneSource;
    AudioSource menuSource;
    AudioSource musicSource;

    // =====/////===== Start of: Unity Lifecycle Functions =====/////=====
    void Awake()
    {
        // Singleton SoundManager has to exist accross scenes.
        if(soundManager == null)
        {
            soundManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject); //makes sure the instance doesn't get removed during runtime

        //Generate Audio Source "channels"
        playerSource = gameObject.AddComponent<AudioSource>();
        enemySource = gameObject.AddComponent<AudioSource>();
        sceneSource = gameObject.AddComponent<AudioSource>();
        menuSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();

        playerSource.outputAudioMixerGroup = playerGroup;
        enemySource.outputAudioMixerGroup = enemyGroup;
        sceneSource.outputAudioMixerGroup = sceneGroup;
        menuSource.outputAudioMixerGroup = menuGroup;
        musicSource.outputAudioMixerGroup = musicGroup;

        //Begin playing the audio
        RefreshAudioVolume();
        StartLevelBGM();
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
    // =====/////===== End of: Unity Lifecycle Functions =====/////=====
    // ========== Start of: Startup Functions ==========
    public static void RefreshAudioVolume()
    {
        if (soundManager == null)
        {
            return;
        }
        soundManager.musicSource.volume = GameManager.GetBgmVolume();
    }
    // ========== End of: Startup Functions ==========
    // ========== Start of: BGM Functions ==========
    public static void StartLevelBGM()
    {
        if(soundManager.use_full_BGM == true)
        {
            if(soundManager.fullBGM != null)
            {
                soundManager.musicSource.clip = soundManager.fullBGM;
                soundManager.musicSource.loop = true;
                soundManager.musicSource.Play();
            }
        }
        else
        {
            float introDuration = soundManager.startBGM != null ? soundManager.startBGM.length : 0f;
            // Play the start part of the background music
            if (soundManager.startBGM != null)
            {
                soundManager.musicSource.PlayOneShot(soundManager.startBGM);
            }
            // Play the loop part of the background music - in loop obviously.
            if (soundManager.loopBGM != null)
            {
                soundManager.musicSource.clip = soundManager.loopBGM;
                soundManager.musicSource.loop = true;
                soundManager.musicSource.PlayDelayed(introDuration);
            }
        }
    }
    public static void PlayEndLevelBGM()
    {
        // Stop the BGM loop
        soundManager.musicSource.loop = false;
        // Play the end part of the background music
        if (soundManager.endBGM != null)
        {
            soundManager.musicSource.PlayOneShot(soundManager.endBGM);
        }
    }
    public static void PlayGameOverBGM()
    {
        // Interrupt all running music
        soundManager.musicSource.Stop();
        soundManager.musicSource.loop = false;
        // Play the game over background music
        if (soundManager.gameOverBGM != null)
        {
            soundManager.musicSource.PlayOneShot(soundManager.gameOverBGM);
        }
    }
    // ========== End of: BGM Functions ==========
    // ========== Start of: Sound Effect Functions ==========
    // ----- Player
    
    public static void PlayPlayerBasicAttack(Vector2 position)
    {
        if (soundManager.playerBasicAttacks.Length > 0)
        {
            soundManager.playerSource.clip = soundManager.playerBasicAttacks[Random.Range(0, soundManager.playerBasicAttacks.Length - 1)];
            soundManager.playerSource.transform.position = position;
            soundManager.playerSource.Play();
        }
    }

    public static void PlayPlayerSlideGround(Vector2 position)
    {
        if (soundManager.playerSlideGrounds.Length > 0)
        {
            soundManager.playerSource.clip = soundManager.playerSlideGrounds[Random.Range(0, soundManager.playerSlideGrounds.Length - 1)];
            soundManager.playerSource.transform.position = position;
            soundManager.playerSource.Play();
        }
    }

    public static void PlayPlayerDashAttack(Vector2 position)//
    {
        if (soundManager.playerDashAttacks.Length > 0)
        {
            soundManager.playerSource.clip = soundManager.playerDashAttacks[Random.Range(0, soundManager.playerDashAttacks.Length - 1)];
            soundManager.playerSource.transform.position = position;
            soundManager.playerSource.Play();
        }
    }

    public static void PlayPlayerDash(Vector2 position)
    {
        if (soundManager.playerDashes.Length > 0)
        {
            soundManager.playerSource.clip = soundManager.playerDashes[Random.Range(0, soundManager.playerDashes.Length - 1)];
            soundManager.playerSource.transform.position = position;
            soundManager.playerSource.Play();
        }
    }

    public static void PlayPlayerDeath(Vector2 position)
    {
        if (soundManager.playerDeaths.Length > 0)
        {
            soundManager.playerSource.clip = soundManager.playerDeaths[Random.Range(0, soundManager.playerDeaths.Length - 1)];
            soundManager.playerSource.transform.position = position;
            soundManager.playerSource.Play();
        }
    }

    public static void PlayPlayerJump(Vector2 position)
    {
        if (soundManager.playerJumps.Length > 0)
        {
            soundManager.playerSource.clip = soundManager.playerJumps[Random.Range(0, soundManager.playerJumps.Length - 1)];
            soundManager.playerSource.transform.position = position;
            soundManager.playerSource.Play();
        }
    }

    public static void PlayPlayerDoubleJump(Vector2 position)
    {
        if (soundManager.playerDoubleJumps.Length > 0)
        {
            soundManager.playerSource.clip = soundManager.playerDoubleJumps[Random.Range(0, soundManager.playerDoubleJumps.Length - 1)];
            soundManager.playerSource.transform.position = position;
            soundManager.playerSource.Play();
        }
    }

    public static void PlayPlayerLanding(Vector2 position)
    {
        if (soundManager.playerLandings.Length > 0)
        {
            soundManager.playerSource.clip = soundManager.playerLandings[Random.Range(0, soundManager.playerLandings.Length - 1)];
            soundManager.playerSource.transform.position = position;
            soundManager.playerSource.Play();
        }
    }

    public static void PlayPlayerHit(Vector2 position)
    {
        if (soundManager.playerHits.Length > 0)
        {
            soundManager.playerSource.clip = soundManager.playerHits[Random.Range(0, soundManager.playerHits.Length - 1)];
            soundManager.playerSource.transform.position = position;
            soundManager.playerSource.Play();
        }
    }

    public static void PlayPlayerHeal(Vector2 position)
    {
        if (soundManager.playerHeals.Length > 0)
        {
            soundManager.playerSource.clip = soundManager.playerHeals[Random.Range(0, soundManager.playerHeals.Length - 1)];
            soundManager.playerSource.transform.position = position;
            soundManager.playerSource.Play();
        }
    }
    // ----- Skeleton
    public static void PlaySkeletonAttack1(Vector2 position)
    {
        if(soundManager.skeletonAttacks1.Length > 0)
        {
            soundManager.enemySource.clip = soundManager.skeletonAttacks1[Random.Range(0, soundManager.skeletonAttacks1.Length - 1)];
            soundManager.enemySource.transform.position = position;
            soundManager.enemySource.Play();
        }
    }
    public static void PlaySkeletonAttack2(Vector2 position)
    {
        if (soundManager.skeletonAttacks2.Length > 0)
        {
            soundManager.enemySource.clip = soundManager.skeletonAttacks2[Random.Range(0, soundManager.skeletonAttacks2.Length - 1)];
            soundManager.enemySource.transform.position = position;
            soundManager.enemySource.Play();
        }
    }

    public static void PlaySkeletonHit(Vector2 position)
    {
        if (soundManager.skeletonHits.Length > 0)
        {
            soundManager.enemySource.clip = soundManager.skeletonHits[Random.Range(0, soundManager.skeletonHits.Length - 1)];
            soundManager.enemySource.transform.position = position;
            soundManager.enemySource.Play();
        }
    }

    public static void PlaySkeletonDeath(Vector2 position)
    {
        if (soundManager.skeletonDeaths.Length > 0)
        {
            soundManager.enemySource.clip = soundManager.skeletonDeaths[Random.Range(0, soundManager.skeletonDeaths.Length - 1)];
            soundManager.enemySource.transform.position = position;
            soundManager.enemySource.Play();
        }
    }

    public static void PlaySkeletonHeal(Vector2 position)
    {
        if (soundManager.skeletonHeals.Length > 0)
        {
            soundManager.enemySource.clip = soundManager.skeletonHeals[Random.Range(0, soundManager.skeletonHeals.Length - 1)];
            soundManager.enemySource.transform.position = position;
            soundManager.enemySource.Play();
        }
    }
    // ----- Scene
    public static void PlayCollectCoin(Vector2 position)
    {
        soundManager.sceneSource.clip = soundManager.collectCoin;
        soundManager.sceneSource.Play();
    }
    // ========== End of: Sound Effect Functions ==========
    // =//=//=//=//=//= End of: SoundManager Class =//=//=//=//=//=
}
