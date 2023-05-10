using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevSelectButtons : MonoBehaviour
{
    public GameObject resetPanel;
    public Button MainMenuButton;
    public Button ResetMenuButton;
    public void ReturnToMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetStats()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GridLoader.Instance.ResetStats();
        SceneManager.LoadScene("LevelSelect");
    }

    public void ShowResetStats()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GridLoader.Instance.ResetStats();
        resetPanel.SetActive(true);
        MainMenuButton.interactable = false;
        ResetMenuButton.interactable = false;   
    }

    public void CancelReset()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        resetPanel.SetActive(false);
        MainMenuButton.interactable = true;
        ResetMenuButton.interactable = true;
    }
}
