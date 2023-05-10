using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManagerScript : MonoBehaviour
{
    #region Singleton
    //creating a single instance of the gamemanager script

    private static GameManagerScript _instance;

    public static GameManagerScript Instance => _instance;

    //checking that there is no other instances of the game manger running
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public int StartingLives = 3;
    public int Lives { get; set; }
    public int ScoreMulti { get; set; }

    public int TotalBricks { get; set; }
    public int RemainingBricks { get; set; }
    public int Score { get; set; }
    public int ActiveBalls { get; set; }

    // Bool to see if the game is started
    public bool IsGameStarted { get; set; }
    public bool IsGameOver { get; set; }

    private bool isLifeLostInProgress = false;

    private const string WindowedPrefKey = "Windowed";

    public bool canInteract = true;

    //public Animator fadeInPanelAnimator; Moved this to code on the panel not using an animation.

    public bool explodingBricks = false;


    private void Start()
    {
        // Setting the starting lives
        this.Lives = this.StartingLives;
        // Setting the game resolution - this is static for this project
        bool isWindowed = PlayerPrefs.GetInt(WindowedPrefKey, 0) == 1;
        Screen.SetResolution(1920, 1080, !isWindowed);
        // Resetting the score on level load / reload
        Score = 0;
        ScoreMulti = 4;
        IsGameOver = false;
        //fadeInPanelAnimator.SetTrigger("FadeInPanel");
    }
    void Update()
    {
        UIManager.Instance.livesText.text = "LIVES : " + Lives;
        UIManager.Instance.scoreText.text = "SCORE : " + Score.ToString("D5");
        //UIManager.Instance.scoreText.text = "BALLS : " + ActiveBalls.ToString("D5");

        bool isGameOverScreenActive = UIManager.Instance.gameOverScreen.activeInHierarchy;
        bool isVictoryScreenActive = UIManager.Instance.victoryScreen.activeInHierarchy;

        if (IsGameStarted && !isGameOverScreenActive && !isVictoryScreenActive)
        {
            // Hide the cursor when the game is started and both gameOverScreen and victoryScreen are disabled
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        if (ActiveBalls <= 0 && !isLifeLostInProgress)
        {
            LifeLost();
        }

        if (RemainingBricks <= 0 && !IsGameOver)
        {
            UIManager.Instance.StatLivesLeft = Lives;
            IsGameStarted = false;
            IsGameOver = true;
            this.Score = this.Score + (Lives * 250);
            UIManager.Instance.StatLivesLeft = Lives;
            UIManager.Instance.UIVictory();
        }
    }

    // Call this to update the score
    public void ChangeScore(int value)
    {
        this.Score = this.Score+(value * ScoreMulti);
        //Debug.Log("Score changed to: " + Score);
    }

    public void UpdateScoreMultiplierText()
    {
        switch (ScoreMulti)
        {
            case 1:
                UIManager.Instance.UIScoreMultiUpdate("/ 4", true);
                break;
            case 2:
                UIManager.Instance.UIScoreMultiUpdate("/ 2", true);
                break;
            case 4:
                UIManager.Instance.UIScoreMultiUpdate("", false);
                break;
            case 8:
                UIManager.Instance.UIScoreMultiUpdate("x 2", true);
                break;
            case 16:
                UIManager.Instance.UIScoreMultiUpdate("x 4", true);
                break;
            case 32:
                UIManager.Instance.UIScoreMultiUpdate("x 8", true);
                break;
            default:
                UIManager.Instance.UIScoreMultiUpdate("", false);
                break;
        }
    }

    public void LifeLost()
    {
        isLifeLostInProgress = true;
        this.Lives = this.Lives - 1;

        if (Lives > 0)
        {
            canInteract = false;
            StartCoroutine(ResetBall());
            UIManager.Instance.UIValuesUpdate();
            UIManager.Instance.livesScore.SetActive(true);
            PaddleScript.Instance.ChangePaddleSizeAnimated(2f);
            ScoreMulti = 4;
            explodingBricks = false;
            UpdateScoreMultiplierText();
        }
        else
        {
            IsGameStarted = false;
            UIManager.Instance.UIGameOver();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ResetBall()
    {
        // Stop the game
        IsGameStarted = false;
        // Wait for a bit of a second
        yield return new WaitForSeconds(0.35f);

        // Reset the starting ball
        BallsManagerScript.Instance.BallReset();

        // Set the isLifeLostInProgress flag to false to stop a bug where lives were continually lost
        isLifeLostInProgress = false;
        // Set interactable to fix a bug with clicking too early and spawning a non interactive ball
        canInteract = true;
    }

    public void ToggleWindowedMode()
    {
        bool isWindowed = PlayerPrefs.GetInt(WindowedPrefKey, 0) == 1;
        isWindowed = !isWindowed;
        PlayerPrefs.SetInt(WindowedPrefKey, isWindowed ? 1 : 0);
        PlayerPrefs.Save();

        Screen.SetResolution(1920, 1080, !isWindowed);
    }

    public void ReturnToMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnToLevelSelect()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("LevelSelect");
    }
}