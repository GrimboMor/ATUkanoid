using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameManagerTest
{
    private GameManagerScript gameManager;

    [SetUp]
    public void Setup()
    {
        // Create a new instance of GameManagerScript before each test
        GameObject gameManagerObject = new GameObject();
        gameManager = gameManagerObject.AddComponent<GameManagerScript>();
        gameManager.Start();
    }

    [Test]
    public void GameManagerScript_Start_InitializesValues()
    {
        // Assert that the starting values are correctly set
        Assert.AreEqual(3, gameManager.StartingLives);
        Assert.AreEqual(3, gameManager.Lives);
        Assert.AreEqual(0, gameManager.Score);
        Assert.AreEqual(4, gameManager.ScoreMulti);
        Assert.AreEqual(0, gameManager.RemainingBricks);
        Assert.AreEqual(0, gameManager.ActiveBalls);
        Assert.IsFalse(gameManager.IsGameStarted);
        Assert.IsFalse(gameManager.IsGameOver);
        Assert.IsFalse(gameManager.explodingBricks);
    }

    [TearDown]
    public void TearDown()
    {
        // Destroy the GameManagerScript instance after each test
        Object.Destroy(gameManager.gameObject);
    }

    [Test]
    public void ChangeScore_UpdatesScoreCorrectly()
    {
        // Arrange
        GameManagerScript gameManager = new GameManagerScript();
        int initialScore = gameManager.Score;

        // Act
        int scoreToAdd = 10;
        gameManager.ChangeScore(scoreToAdd);

        // Assert
        int expectedScore = initialScore + scoreToAdd;
        int actualScore = gameManager.Score;
        Assert.AreEqual(expectedScore, actualScore, "The score was not updated correctly.");
    }

    [Test]
    public void LifeLost_UpdatesLivesAndEndsGameWhenLivesReachZero()
    {
        // Arrange
        var gameManager = new GameManagerScript();
        gameManager.Lives = 3;
        var expectedLives = 2;

        // Act
        gameManager.LifeLost();

        // Assert
        Assert.AreEqual(expectedLives, gameManager.Lives);

        if (expectedLives == 0)
        {
            Assert.IsTrue(gameManager.IsGameOver());
        }
        else
        {
            Assert.IsFalse(gameManager.IsGameOver());
        }
    }

    [Test]
    public void GameManagerScript_Instance_IsNotNull()
    {
        GameObject gameManagerObj = new GameObject();
        gameManagerObj.AddComponent<GameManagerScript>();
        Assert.NotNull(gameManagerObj.GetComponent<GameManagerScript>());
    }

    [Test]
    public void RestartLevel_ReloadsScene()
    {
        // Arrange
        GameManagerScript gameManager = new GameManagerScript();
        int initialScore = gameManager.Score;
        int initialLives = gameManager.Lives;
        SceneManager.LoadScene("Level1");
  
        // Act
        gameManager.RestartLevel();

        // Assert
        Assert.AreEqual(initialScore, gameManager.Score, "Score should be reset to initial value after restarting level.");
        Assert.AreEqual(initialLives, gameManager.Lives, "Lives should be reset to initial value after restarting level.");
        Assert.AreEqual("Level1", SceneManager.GetActiveScene().name, "Active scene should be reset to Level1 after restarting level.");
    }
}
