using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    #region Singleton
    //creating a single instance of the gamemanager script

    private static UIManager _instance;

    public static UIManager Instance => _instance;

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

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public Text goScoreText;
    public Text goBrickText;

    private void Start()
    {
        livesText = GameObject.Find("UI-LivesText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("UI-ScoreText").GetComponent<TextMeshProUGUI>();
    }

    public void UIVictory()
    {
        BallsManagerScript.Instance.DestroyAllBalls();
        ClearScreen();
        UIManager.Instance.victoryScreen.SetActive(true);
        UIManager.Instance.livesScore.SetActive(false);
        

        GameObject VicScoreText = GameObject.Find("VicScoreText");
        Text scoreText = VicScoreText.GetComponent<Text>();
        scoreText.text = "SCORE : " + GameManagerScript.Instance.Score.ToString("D5");
    }

    public void UIGameOver()
    {
        ClearScreen();
        UIManager.Instance.gameOverScreen.SetActive(true);
        UIManager.Instance.livesScore.SetActive(false);

        GameObject goScoreText = GameObject.Find("GOScoreText");
        Text scoreText = goScoreText.GetComponent<Text>();
        scoreText.text = "SCORE : " + GameManagerScript.Instance.Score.ToString("D5");

        GameObject goBrickText = GameObject.Find("GOBrickText");
        Text brickText = goBrickText.GetComponent<Text>();
        brickText.text = "BRICKS : " + GameManagerScript.Instance.RemainingBricks + " / " + GameManagerScript.Instance.TotalBricks;
    }

    public void UIValuesUpdate()
    {
        livesText.text = "LIVES : " + GameManagerScript.Instance.Lives;
    }

    private void ClearScreen()
    {
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (GameObject powerUp in powerUps)
        {
            Destroy(powerUp);
        }

        GameObject[] particles = GameObject.FindGameObjectsWithTag("Particles");
        foreach (GameObject particle in particles)
        {
            Destroy(particle);
        }
    }
}
