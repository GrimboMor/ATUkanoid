using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.IO;

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
    public GameObject fadeInPanel;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powUpText;
    public TextMeshProUGUI powDownText;
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
        UIManager.Instance.livesScore.SetActive(true);
        UIManager.Instance.fadeInPanel.SetActive(true);
        livesText = GameObject.Find("UI-LivesText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("UI-ScoreText").GetComponent<TextMeshProUGUI>();
        powUpText = GameObject.Find("UI-PowerUp").GetComponent<TextMeshProUGUI>();
        powDownText = GameObject.Find("UI-PowerDown").GetComponent<TextMeshProUGUI>();

        StatCollectPowUps = 0;
        StatCollectPowDown = 0;
        StatScorePowUps = 0;
        StatScorePowDown = 0;
        StatLivesLeft = 0;
        StatLivesBonus = 0;
        StatPadResized = 0;
        StatTotalBallBounces = 0;
    }

    public void ShowPowUpText(string powText)
    {
        powUpText.text = powText;
        Animator animator = powUpText.GetComponent<Animator>();
        animator.SetTrigger("PowShowTrigger");
    }

    public void ShowPowDownText(string powText)
    {
        powDownText.text = powText;
        Animator animator = powDownText.GetComponent<Animator>();
        animator.SetTrigger("PowShowTrigger");
    }

    public void UIVictory()
    {
        BallsManagerScript.Instance.DestroyAllBalls();
        ClearScreen();
        FadePanelAnimation.Instance.ResetFadePanel();
        UIManager.Instance.victoryScreen.SetActive(true);
        UIManager.Instance.livesScore.SetActive(false);
        UIManager.Instance.multiScore.SetActive(false);

        bool isNewHighScore = UpdateHighScore();

        GameObject VicScoreText = GameObject.Find("VicScoreText");
        Text scoreText = VicScoreText.GetComponent<Text>();
        if (isNewHighScore)
        {
            scoreText.text = "NEW HIGH SCORE : " + GameManagerScript.Instance.Score.ToString("D5");
            Animator animator = VicScoreText.GetComponent<Animator>();
            animator.SetTrigger("NewHighScoreTrigger");
        }
        else
        {
            scoreText.text = "SCORE : " + GameManagerScript.Instance.Score.ToString("D5");
        }

        UIValuesUpdate();
        GameObject goStatsText = GameObject.Find("VicStatsValues");
        Text statText = goStatsText.GetComponent<Text>();
        statText.text = " " + StatCollectPowUps + "\n" + StatScorePowUps + "\n" + StatCollectPowDown + "\n" + StatScorePowDown + "\n" + StatPadResized + "\n" + StatTotalBallBounces + "\n" + StatLivesLeft + "\n" + StatLivesBonus;

        UpdateHighScore();
        SoundEffectPlayer.Instance.Victory();
    }

    public void UIGameOver()
    {
        ClearScreen();
        FadePanelAnimation.Instance.ResetFadePanel();
        UIManager.Instance.gameOverScreen.SetActive(true);
        UIManager.Instance.livesScore.SetActive(false);
        UIManager.Instance.multiScore.SetActive(false);

        bool isNewHighScore = UpdateHighScore();

        GameObject goBrickText = GameObject.Find("GOBrickText");
        Text brickText = goBrickText.GetComponent<Text>();
        brickText.text = "BRICKS : " + GameManagerScript.Instance.RemainingBricks + " / " + GameManagerScript.Instance.TotalBricks;

        UIValuesUpdate();
        GameObject goStatsText = GameObject.Find("GOStatsValues");
        Text statText = goStatsText.GetComponent<Text>();
        statText.text = " " + StatCollectPowUps + "\n" + StatScorePowUps + "\n" + StatCollectPowDown + "\n" + StatScorePowDown + "\n" + StatPadResized + "\n" + StatTotalBallBounces;

        UpdateHighScore();
        SoundEffectPlayer.Instance.GameOver();

        GameObject goScoreText = GameObject.Find("GOScoreText");
        Text scoreText = goScoreText.GetComponent<Text>();
        
        if (isNewHighScore)
        {
            scoreText.text = "NEW HIGH SCORE : " + GameManagerScript.Instance.Score.ToString("D5");
            Animator animator = goScoreText.GetComponent<Animator>();
            animator.SetTrigger("NewHighScoreTrigger");
        }
        else
        {
            scoreText.text = "SCORE : " + GameManagerScript.Instance.Score.ToString("D5");
        }
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

    private bool UpdateHighScore()
    {
        bool isNewHighScore = false;
        string levelToLoad = PlayerPrefs.GetString("LevelToLoad");
        string jsonFilePath = Path.Combine(Application.persistentDataPath, levelToLoad + ".json");

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonData);

            int currentScore = GameManagerScript.Instance.Score;

            // Check if the current score is higher than the high score
            if (currentScore > levelData.HighScore)
            {
                levelData.HighScore = currentScore;

                // Save the updated high score back to the JSON file
                string updatedJsonData = JsonUtility.ToJson(levelData);
                File.WriteAllText(jsonFilePath, updatedJsonData);

                isNewHighScore = true;
            }
        }
        else
        {
            Debug.LogWarning("JSON file not found for level: " + levelToLoad);
        }

        return isNewHighScore;
    }
}