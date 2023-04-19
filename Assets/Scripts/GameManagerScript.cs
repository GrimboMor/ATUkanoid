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

    public int TotalBricks { get; set; }
    public int RemainingBricks { get; set; }
    public int Score { get; set; }
    public int ActiveBalls { get; set; }

    // Bool to see if the game is started
    public bool IsGameStarted { get; set; }

 
    private void Start()
    {
        // Setting the starting lives
        this.Lives = this.StartingLives;
        // Setting the game resolution - this is static for this project
        Screen.SetResolution(1920, 1080, false);
        // Resetting the score on level load / reload
        Score = 0;
    }
    void Update()
    {
        UIManager.Instance.livesText.text = "LIVES : " + Lives;
        UIManager.Instance.scoreText.text = "SCORE : " + Score.ToString("D5");
        //UIManager.Instance.scoreText.text = "BALLS : " + ActiveBalls.ToString("D5");

        
        if (IsGameStarted)
        {
            // Hide the cursor when the game is started
            Cursor.visible = false;
                        
            if (ActiveBalls <= 0)
            {
                LifeLost();
            }
        }
        else
        {
            Cursor.visible = true;
        }

        if (RemainingBricks <= 0)
        {
            IsGameStarted = false;
            UIManager.Instance.UIVictory();
        }
    }

    // Call this to update the score
    public void ChangeScore(int value)
    {
        this.Score += value;
    }

    public void LifeLost()
    {
        this.Lives = this.Lives -1;

        if (Lives > 0)
        {
            StartCoroutine(ResetBall());
            UIManager.Instance.UIValuesUpdate();
            UIManager.Instance.livesScore.SetActive(true);
            PaddleScript.Instance.ChangePaddleSizeAnimated(2f);
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

    private IEnumerator CheckRemainingBricksAndBalls()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Wait for 2 seconds

            // Check if there are remaining bricks and balls in the BallsList
            bool hasRemainingBricks = BricksManager.Instance.RemainingBricks.Count > 0;
            bool hasActiveBalls = BallsManagerScript.Instance.BallsList.Count > 0;

            if (hasRemainingBricks && !hasActiveBalls)
            {
                // Trigger a lost life
                LifeLost();
            }
        }
    }

    IEnumerator ResetBall()
    {
        // Stop the game
        IsGameStarted = false;
        // Wait for a bit of a second
        yield return new WaitForSeconds(0.35f);

        // Reset the starting ball
        BallsManagerScript.Instance.BallReset();
    }
}