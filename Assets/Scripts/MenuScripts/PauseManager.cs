using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public Button resumeButton;
    public float fadeDuration = 1f;

    private bool isPaused = false;

    private const string WindowedPrefKey = "Windowed";

    public Toggle fullscreenToggle;

    private void Start()
    {
        HidePauseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            ShowPauseMenu();
        }
    }

    private void ShowPauseMenu()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseMenuPanel.SetActive(true);

        }
    }

    private void HidePauseMenu()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        HidePauseMenu();
    }
    
    public void ButtonLevelSelect()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game.");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleWindowedMode()
    {
        bool isWindowed = PlayerPrefs.GetInt(WindowedPrefKey, 0) == 1;
        isWindowed = !isWindowed;
        PlayerPrefs.SetInt(WindowedPrefKey, isWindowed ? 1 : 0);
        PlayerPrefs.Save();

        Screen.SetResolution(1920, 1080, !isWindowed);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartLevel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
