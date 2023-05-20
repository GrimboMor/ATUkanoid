using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    private const string WindowedPrefKey = "Windowed";

    public Toggle fullscreenToggle;

    public GameObject BackGround;
    private float fadeTime = 0.25f; 
    private Image backgroundImage;
    public GameObject aboutPanel;


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
        backgroundImage = BackGround.GetComponent<Image>();
        StartCoroutine(FadeBackground());
        HideAboutMenu();    
    }

    public void ShowAboutMenu()
    {
        aboutPanel.SetActive(true);

    }

    public void HideAboutMenu()
    {
        aboutPanel.SetActive(false);
    }

    private void Update()
    {
      if (Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    private IEnumerator FadeBackground()
    {
        float elapsedTime = 0f;
        Color startColor = new Color(0f, 0f, 0f, 1f);
        Color endColor = new Color(1f, 1f, 1f, 1f);

        while (elapsedTime < fadeTime)
        {
            float t = elapsedTime / fadeTime;
            backgroundImage.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        backgroundImage.color = endColor;
    }
}
