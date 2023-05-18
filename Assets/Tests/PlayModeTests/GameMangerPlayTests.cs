using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameMangerPlayTests
{
    private GameManagerScript gameManager;

    [SetUp]
    public void Setup()
    {
        // Create a new instance of GameManagerScript
        GameObject gameManagerObject = new GameObject();
        gameManager = gameManagerObject.AddComponent<GameManagerScript>();
        gameManager.Start();
    }

    [TearDown]
    public void TearDown()
    {
        // Destroy the GameManagerScript instance 
        Object.Destroy(gameManager.gameObject);
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
        GameObject gameObject = new GameObject();
        GameManagerScript gameManager = gameObject.AddComponent<GameManagerScript>();
        int initialScore = gameManager.Score;
        int initialLives = gameManager.Lives;
        SceneManager.LoadScene("Level");

        // Act
        gameManager.RestartLevel();

        // Assert
        Assert.AreEqual(initialScore, gameManager.Score, "Score should be reset to initial value after restarting level.");
        Assert.AreEqual(initialLives, gameManager.Lives, "Lives should be reset to initial value after restarting level.");
    }

}
