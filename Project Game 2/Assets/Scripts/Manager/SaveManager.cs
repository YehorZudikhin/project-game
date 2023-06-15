using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    // Singleton - One instance to rule them all.
    static SaveManager saveManager;

    private string settingsFilename = "settings.json";

    private Settings settings;

    // =====/////===== Start of: Unity Lifecycle Functions =====/////=====
    void Awake()
    {
        if(saveManager != null && saveManager != this)
        {
            Destroy(gameObject);
            return;
        }
        saveManager = this;
        DontDestroyOnLoad(gameObject);
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
    // =====/////===== Start of: Settings Functions =====/////=====
    public static void SetSettings(Settings settings)
    {
        if (saveManager == null || settings == null)
        {
            return;
        }
        saveManager.settings = settings;
        saveManager.SaveSettings();
    }

    public static Settings GetSettings()
    {
        if (saveManager == null)
        {
            return null;
        }
        if (saveManager.settings == null)
        {
            saveManager.LoadSettings();
        }
        return saveManager.settings;
    }

    void SaveSettings()
    {
        if (settings == null)
        {
            LoadSettings();
        }
        string jsonData = JsonUtility.ToJson(settings);
        string filePath = Path.Combine(Application.persistentDataPath, settingsFilename);
        File.WriteAllText(filePath, jsonData);
    }

    void LoadSettings()
    {
        if (settings != null) // prevent double loading
        {
            settings = new Settings();
            return;
        }

        // Path.Combine combines strings into a file path
        string filePath = Path.Combine(Application.persistentDataPath, settingsFilename);
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            settings = JsonUtility.FromJson<Settings>(dataAsJson);
        }
        else
        {
            settings = new Settings();
        }
    }
    // =====/////===== End of: Settings Functions =====/////=====

    // =//=//=//=//=//= End of: SaveManager Class =//=//=//=//=//=
}
