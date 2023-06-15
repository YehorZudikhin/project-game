using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    static GameManager gameManager;

    bool isGameOver;
    float totalGameTime;

    float bgmVolume = 1f;
    float sfxVolume = 1f;

    public static bool isPlayerAlive = true;

    // =====/////===== Start of: Unity Lifecycle Functions =====/////=====
    // Awake is called before the script starts, regardless if active.
    void Awake()
    {
        // There can only be one!
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }

        gameManager = this;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update (upon activation)
    void Start()
    {
        LoadSettings();
        //LoadRecords();
        //SaveRecords();

        GameReset();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            SoundManager.PlayGameOverBGM();
            //show game over screen
            //etc
        }
        totalGameTime += Time.deltaTime;
    }
    // =====/////===== End of: Unity Lifecycle Functions =====/////=====
    // ========== Start of: Save/Load ==========
    private void SaveSettings()
    {
        Settings settings = SaveManager.GetSettings();
        if (settings == null)
        {
            settings = new Settings();
        }
        settings.bgmVolume = GetBgmVolume();
        settings.sfxVolume = GetBgmVolume();
        SaveManager.SetSettings(settings);
    }

    private void LoadSettings()
    {
        Settings settings = SaveManager.GetSettings();
        if (settings != null)
        {
            SetBgmVolume(settings.bgmVolume);
            SetSfxVolume(settings.sfxVolume);
            SoundManager.RefreshAudioVolume();
        }
    }
    // ========== End of: Save/Load ==========
    // ========== Start of: BGM Volume ==========
    public static void SetBgmVolume(float bgmVolume, bool save = false)
    {
        if (gameManager == null)
        {
            return;
        }
        gameManager.bgmVolume = bgmVolume;
        if (save) //if save==true
        {
            gameManager.SaveSettings();
        }
    }

    public static float GetBgmVolume()
    {
        if (gameManager == null)
        {
            return 0f;
        }
        return gameManager.bgmVolume;
    }
    // ========== End of: BGM Volume ==========
    // ========== Start of: SFX Volume ==========
    public static void SetSfxVolume(float sfxVolume, bool save = false)
    {
        if (gameManager == null)
        {
            return;
        }
        gameManager.sfxVolume = sfxVolume;
        if (save)
        {
            gameManager.SaveSettings();
        }
    }

    public static float GetSfxVolume()
    {
        if (gameManager == null)
        {
            return 0f;
        }
        return gameManager.sfxVolume;
    }
    // ========== End of: SFX Volume ==========
    // ========== Start of: Game Control ==========
    public static void GameReset()
    {
        if (!gameManager)
        {
            return;
        }
        // Reset Values
        Time.timeScale = 1;
        gameManager.isGameOver = false;
        gameManager.totalGameTime = 0;
    }
    // ========== End of: Game Control ==========
}