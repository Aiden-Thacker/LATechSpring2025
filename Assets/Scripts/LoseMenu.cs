using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LoseMenu : MonoBehaviour
{
    public GameObject loseSection;
    public PauseMenu pauseMenu;
    public void Lose()
    {
        pauseMenu.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        loseSection.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        pauseMenu.enabled = true;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        pauseMenu.enabled = true;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame()
    {
        #if(UNITY_EDITOR)
        Debug.Log("Quiting Play Mode");
        EditorApplication.ExitPlaymode();
        #else
        Debug.Log("Quitting Build");
        Application.Quit();
        #endif
    }
}
