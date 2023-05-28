using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class BallPlayTests
{
    private BallScript ballScript;
    private GameObject paddle;

    [SetUp]
    public void SetUp()
    {
        // Create a new instance of BallScript
        GameObject ballObject = new GameObject();
        ballScript = ballObject.AddComponent<BallScript>();

        // Create a paddle object for collision testing
        paddle = new GameObject();
        paddle.tag = "Paddle";
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up created objects
        Object.Destroy(ballScript.gameObject);
        Object.Destroy(paddle);
    }

    [Test]
    public void Ball_InitializesValues()
    {
        // Assert that the starting values are correctly set
        Assert.IsFalse(ballScript.isALaserBall);
    }
    /*
    [UnityTest]
    public IEnumerator StartLaserBall()
    {
        BallScript newBallScript;
        GameObject newBallObject = new GameObject();
        newBallScript.StartLaserBall();
        Assert.IsTrue(newBallScript.isALaserBall);
        yield return null;
    } */

    [UnityTest]
    public IEnumerator DestroyLaserBall()
    {
        bool eventInvoked = false;
        BallScript.OnLaserBallSDisabled += ball => eventInvoked = true;
        ballScript.DestroyLaserBall();
        Assert.IsTrue(eventInvoked);
        yield return null;
    }
}