using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameManagerTest
{
    private GameManagerScript gameManager;

    [Test]
    public void ChangeScore_UpdatesScoreCorrectly()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        GameManagerScript gameManager = gameObject.AddComponent<GameManagerScript>();
        int initialScore = gameManager.Score;
        int scoreMulti = gameManager.ScoreMulti;

        // Act
        int scoreToAdd = 10;
        gameManager.ChangeScore(scoreToAdd);

        // Assert
        int expectedScore = initialScore + (scoreToAdd * scoreMulti);
        int actualScore = gameManager.Score;
        Assert.AreEqual(expectedScore, actualScore, "The score was not updated correctly.");
    }

    [Test]
    public void GameManagerScript_Instance_IsNotNull()
    {
        GameObject gameManagerObj = new GameObject();
        gameManagerObj.AddComponent<GameManagerScript>();
        Assert.NotNull(gameManagerObj.GetComponent<GameManagerScript>());
    }
}
