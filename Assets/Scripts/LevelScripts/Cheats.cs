using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{

    public GameObject gameDiagnostics;
    public bool CheatsEnabled = false;


    void Update()
    {
        //Press R to restart the level
        if (Input.GetKeyDown(KeyCode.R))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //GameManagerScript.Instance.RestartLevel();
            GameManagerScript.Instance.LifeLost();
        }

        //Press W to toggle Windowed Mode
        if (Input.GetKeyDown(KeyCode.W))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //GameManagerScript.Instance.RestartLevel();
            //GameManagerScript.Instance.ToggleWindowedMode();    
            BallsManagerScript.Instance.SlowBalls();    
        }

        //Press H to Clear Scores
        if (Input.GetKeyDown(KeyCode.H))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //GameManagerScript.Instance.RestartLevel();
            ClearHighScore();
        }



        if (CheatsEnabled == true)
        {
            //Press L to add a life
            if (Input.GetKeyDown(KeyCode.L))
            {
                GameManagerScript.Instance.Lives++;
                //GameManagerScript.Instance.UpdateLives();
            }
            // Check for growing or shrinking the paddle size
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                PaddleScript.Instance.ChangePaddleSizeAnimated(3.6f);
            }
            else if (Input.GetKeyDown(KeyCode.Minus))
            {
                PaddleScript.Instance.ChangePaddleSizeAnimated(2f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                PaddleScript.Instance.ChangePaddleSizeAnimated(1.2f);
            }

            //Press M to test MultiBalls
            if (Input.GetKeyDown(KeyCode.M))
            {
                foreach (BallScript ball in BallsManagerScript.Instance.BallsList.ToList())
                {
                    BallsManagerScript.Instance.MultiBalls(ball.gameObject.transform.position, 2, ball.isALaserBall);
                }
            }

            //Press M to test MultiBalls
            if (Input.GetKeyDown(KeyCode.L))
            {
                foreach (BallScript ball in BallsManagerScript.Instance.BallsList.ToList())
                {
                    ball.StartLaserBall();
                }
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                //Debug.Log("Current Multi: " + GameManagerScript.Instance.ScoreMulti);
                UIManager.Instance.StatCollectPowUps = UIManager.Instance.StatCollectPowUps + 1;
                UIManager.Instance.StatScorePowUps = UIManager.Instance.StatScorePowUps + (5 * GameManagerScript.Instance.ScoreMulti);

                GameManagerScript.Instance.Score = GameManagerScript.Instance.Score + (5 * GameManagerScript.Instance.ScoreMulti);

                if (GameManagerScript.Instance.ScoreMulti >= 1 && GameManagerScript.Instance.ScoreMulti < 32)
                {
                    GameManagerScript.Instance.ScoreMulti *= 2;
                }
                //Debug.Log("Current Multi: " + GameManagerScript.Instance.ScoreMulti);
                GameManagerScript.Instance.UpdateScoreMultiplierText();
            }
        }
    }

    private void ClearHighScore()
    {
        string levelToLoad = PlayerPrefs.GetString("LevelToLoad");
        string jsonFilePath = Path.Combine(Application.persistentDataPath, levelToLoad + ".json");

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonData);

            levelData.HighScore = 0;

            // Save the updated high score back to the JSON file
            string updatedJsonData = JsonUtility.ToJson(levelData);
            File.WriteAllText(jsonFilePath, updatedJsonData);
        }
        else
        {
            Debug.LogWarning("JSON file not found for level: " + levelToLoad);
        }
    }
}