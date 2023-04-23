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
    public GameObject multiScore;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public Text goScoreText;
    public Text goBrickText;
    public Text goStatsText;
    public Text scoreMultiText;

    //Strat tracking!!
    public int StatCollectPowUps { get; set; }
    public int StatCollectPowDown { get; set; }
    public int StatScorePowUps { get; set; } //needed as powerups have different scores
    public int StatScorePowDown { get; set; } //needed as powerups have different scores
    public int StatLivesLeft { get; set; } 
    private int StatLivesBonus { get; set; } 
    public int StatPadResized { get; set; }
    public int StatTotalBallBounces { get; set; }

    private void Start()
    {
        livesText = GameObject.Find("UI-LivesText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("UI-ScoreText").GetComponent<TextMeshProUGUI>();

        StatCollectPowUps = 0;
        StatCollectPowDown = 0;
        StatScorePowUps = 0;
        StatScorePowDown = 0;
        StatLivesLeft = 0;
        StatLivesBonus = 0;
        StatPadResized = 0;
        StatTotalBallBounces = 0;
    }

    public void UIVictory()
    {
        BallsManagerScript.Instance.DestroyAllBalls();
        ClearScreen();
        UIManager.Instance.victoryScreen.SetActive(true);
        UIManager.Instance.livesScore.SetActive(false);
        UIManager.Instance.multiScore.SetActive(false);


        GameObject VicScoreText = GameObject.Find("VicScoreText");
        Text scoreText = VicScoreText.GetComponent<Text>();
        scoreText.text = "SCORE : " + GameManagerScript.Instance.Score.ToString("D5");

        UIValuesUpdate();
        GameObject goStatsText = GameObject.Find("VicStatsValues");
        Text statText = goStatsText.GetComponent<Text>();
        statText.text = " " + StatCollectPowUps + "\n" + StatScorePowUps + "\n" + StatCollectPowDown + "\n" + StatScorePowDown + "\n" + StatPadResized + "\n" + StatTotalBallBounces + "\n" + StatLivesLeft + "\n" + StatLivesBonus;

    }

    public void UIGameOver()
    {
        ClearScreen();
        UIManager.Instance.gameOverScreen.SetActive(true);
        UIManager.Instance.livesScore.SetActive(false);
        UIManager.Instance.multiScore.SetActive(false);

        GameObject goScoreText = GameObject.Find("GOScoreText");
        Text scoreText = goScoreText.GetComponent<Text>();
        scoreText.text = "SCORE : " + GameManagerScript.Instance.Score.ToString("D5");

        GameObject goBrickText = GameObject.Find("GOBrickText");
        Text brickText = goBrickText.GetComponent<Text>();
        brickText.text = "BRICKS : " + GameManagerScript.Instance.RemainingBricks + " / " + GameManagerScript.Instance.TotalBricks;

        UIValuesUpdate();
        GameObject goStatsText = GameObject.Find("GOStatsValues");
        Text statText = goStatsText.GetComponent<Text>();
        statText.text = " " + StatCollectPowUps + "\n" + StatScorePowUps + "\n" + StatCollectPowDown + "\n" + StatScorePowDown + "\n" + StatPadResized + "\n" + StatTotalBallBounces;
    }

    public void UIValuesUpdate()
    {
        livesText.text = "LIVES : " + GameManagerScript.Instance.Lives;
        StatLivesBonus = StatLivesLeft * 250;
    }

    public void UIScoreMultiUpdate(string multiValue, bool enabled)
    {
        UIManager.Instance.multiScore.SetActive(enabled);
        if (enabled)
        {
            scoreMultiText.text = multiValue;
            scoreMultiText.color = GetMultiplierColor(multiValue);
        }
    }

    private Color GetMultiplierColor(string multiValue)
    {
        if (multiValue.StartsWith("/"))
        {
            return new Color32(0xAE, 0x23, 0x34, 0xFF);
        }
        else if (multiValue.StartsWith("x"))
        {
            return new Color32(0x23, 0x90, 0x63, 0xFF);
        }
        else
        {
            return Color.white;
        }
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
