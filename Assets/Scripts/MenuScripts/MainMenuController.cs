using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class MainMenuController : MonoBehaviour
{
    private const string WindowedPrefKey = "Windowed";

    public Toggle fullscreenToggle;

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

    public void ToggleWindowedMode()
    {
        bool isWindowed = PlayerPrefs.GetInt(WindowedPrefKey, 0) == 1;
        isWindowed = !isWindowed;
        PlayerPrefs.SetInt(WindowedPrefKey, isWindowed ? 1 : 0);
        PlayerPrefs.Save();

        Screen.SetResolution(1920, 1080, !isWindowed);
    }

    private void Start()
    {
        // Set the toggle's initial value based on the PlayerPrefs value
        fullscreenToggle.isOn = PlayerPrefs.GetInt(WindowedPrefKey, 0) == 0;
    }

    private void Update()
    {
      if (Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

        // All this code to move to the level select Grid
        public void LoadLevel(int levelIndex)
    {
        LevelToLoad(levelIndex);
        SceneManager.LoadScene(1);
    }

    private List<string> LoadLevelNames()
    {
        TextAsset text = Resources.Load("LevelList") as TextAsset;
        return text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public string GetLevelName(int index)
    {
        List<string> levelNames = LoadLevelNames();
        if (index >= 0 && index < levelNames.Count)
        {
            return levelNames[index];
        }
        return null;
    }

    public void LevelToLoad(int levelIndex)
    {
        string levelName = GetLevelName(levelIndex);
        if (!string.IsNullOrEmpty(levelName))
        {
            PlayerPrefs.SetString("LevelToLoad", levelName);
            PlayerPrefs.Save();
        }
    }
}
