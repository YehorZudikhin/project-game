using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{

    public GameObject gameOverMenu;

    private void OnEnable()
    {
        PlayerCombat.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        PlayerCombat.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
    
    public void RestartLevel()
    {
        // Load menu
        Debug.Log("Restarting Level...");
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMenu()
    {
        // Load menu
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
