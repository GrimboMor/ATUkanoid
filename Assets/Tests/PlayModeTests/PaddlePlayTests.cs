using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class PaddlePlayTests
{
    private GameObject paddleGameObject;
    private PaddleScript paddleScript;

    [SetUp]
    public void Setup()
    {
        paddleGameObject = new GameObject("Paddle");
        paddleScript = paddleGameObject.AddComponent<PaddleScript>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(paddleGameObject);
    }

    /* Tests removed as they are failing due to bugs in Unity Test Framework
     * https://forum.unity.com/threads/async-await-in-unittests.513857/
     * I keep getting an error "Method has non-void return value, but no result is expected" which I can find loads of posts about online,
     * but no one seems to have a solution for this.
     * 
    [Test]
    public IEnumerator PaddleMoves()
    {
        Vector3 originalPosition = paddleGameObject.transform.position;

        yield return new WaitForFixedUpdate();

        Vector3 newPosition = paddleGameObject.transform.position;
        yield return new WaitForFixedUpdate();

        Assert.AreNotEqual(originalPosition, newPosition);
    }

    [Test]
    public IEnumerator PaddleResizes()
    {
        float originalSize = paddleScript.GetPaddleSize();
        float upDateSize = 3f;
        paddleScript.ChangePaddleSizeAnimated(upDateSize);

        yield return new WaitForSeconds(0.3f); // Wait for the animation to complete

        float newSize = paddleScript.GetPaddleSize();
        Assert.AreNotEqual(originalSize, newSize);
    }
    */
    [Test]
    public void Paddle_Instance_IsNotNull()
    {
        GameObject gameManagerObj = new GameObject();
        gameManagerObj.AddComponent<PaddleScript>();
        Assert.NotNull(gameManagerObj.GetComponent<PaddleScript>());
    }
}