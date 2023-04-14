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

    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public GameObject livesScore;

    public int StartingLives = 3;
    public int Lives { get; set; }
    private TextMeshProUGUI livesText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI brickText;
    private TextMeshProUGUI goBrickText;

    public int TotalBricks { get; set; }
    public int RemainingBricks { get; set; }
    public int Score { get; set; }

    // Bool to see if the game is started
    public bool IsGameStarted { get; set; }

    // Setting the game resolution - this is static for this project
    private void Start()
    {
        this.Lives = this.StartingLives;
        Screen.SetResolution(1920, 1080, false);
        // Subscribe the OnBallDeath method to the BallScript.OnBallDeath event.
        // This allows the OnBallDeath method to be called when a ball dies.
        BallScript.OnBallDeath += OnBallDeath;
        Score = 0;
        livesText = GameObject.Find("LivesText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        brickText = GameObject.Find("BrickText").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        livesText.text = "LIVES: " + Lives;
        brickText.text = "BRICKS: " + RemainingBricks + " / " + TotalBricks;
        scoreText.text = "SCORE: " + Score.ToString("D5");

        // Hide the cursor when the game is started
        if (IsGameStarted)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
        if (Lives > 0)
        {
            livesText.text = "LIVES: " + Lives;
            livesScore.SetActive(true);
        }
        else
        {
            livesScore.SetActive(false);
        }
        if (RemainingBricks <= 0)
        {
            Victory();
        }
    }

    // Call this to update the score
    public void ChangeScore(int value)
    {
        Score += value;
    }

    public void Victory()
    {
        victoryScreen.SetActive(true);
        IsGameStarted = false;
        livesScore.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnBallDeath(BallScript obj)
    {
        if (BallsManagerScript.Instance.BallsList.Count <= 0)
        {
            // Decrease the number of lives by 1
            this.Lives = this.Lives - 1;

            if (this.Lives < 1)
            {
                livesScore.SetActive(false);
                gameOverScreen.SetActive(true);
                IsGameStarted = false;
                UpdateGameOverScreen();
            }
            else
            {
                StartCoroutine(ResetBall());
            }
        }
    }


    IEnumerator ResetBall()
    {
        // Wait for a bit of a second
        yield return new WaitForSeconds(0.35f);

        // Reset the starting ball
        BallsManagerScript.Instance.BallReset();
        // Stop the game
        IsGameStarted = false;
    }


    private void OnDisable()
    {
        // Unsubscribe the OnBallDeath method from the BallScript.OnBallDeath event.
        // This prevents the OnBallDeath method from being called when a ball dies.
        BallScript.OnBallDeath -= OnBallDeath;
    }

    public void UpdateGameOverScreen()
    {
        goBrickText = GameObject.Find("GOBrickText").GetComponent<TextMeshProUGUI>();
        goBrickText.text = "BRICKS: " + RemainingBricks + " / " + TotalBricks;
    }
}