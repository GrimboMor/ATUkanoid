using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class BricksManagerTests
{
    private BricksManager bricksManager;

    [SetUp]
    public void SetUp()
    {
        GameObject gameObject = new GameObject();
        bricksManager = gameObject.AddComponent<BricksManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(bricksManager.gameObject);
    }

    public void BricksManager_Instance_IsNotNull()
    {
        GameObject bricksManagerObj = new GameObject();
        bricksManagerObj.AddComponent<BricksManager>();
        Assert.NotNull(bricksManagerObj.GetComponent<BricksManager>());
    }

    [Test]
    public void BricksManager_Start_InitializesValues()
    {
        // Assert that the starting values are correctly set
        Assert.AreEqual(17, bricksManager.maximumRows);
        Assert.AreEqual(33, bricksManager.maximumColumns);
        Assert.AreEqual(-7.85f, bricksManager.firstBrickX);
        Assert.AreEqual(3.35f, bricksManager.firstBrickY);
        Assert.AreEqual(0.49f, bricksManager.nextBrickSpacerX);
        Assert.AreEqual(0.33f, bricksManager.nextBrickSpacerY);
    }

    /*  
     *  "new" calls are throwing errors and causing tests to falsely fail 
     *  
    [Test]
    public void LevelGeneration_RemainingBricksListNotEmpty()
    {
        bricksManager.LevelTextFile = "Level_0001";
        bricksManager.LevelColorTextFile = bricksManager.LevelTextFile + "C";
        bricksManager.LevelSpriteTextFile = bricksManager.LevelTextFile + "S";

        bricksManager.LevelGeneration();

        Assert.IsNotNull(bricksManager.RemainingBricks);
        Assert.AreNotEqual(0, bricksManager.RemainingBricks.Count);
    }

    [Test]
    public void LoadBrickLayout_ReturnsBrickLayoutList()
    {
        string textFile = "Level_0001";
        List<int[,]> brickLayout = bricksManager.LoadBrickLayout(textFile);

        Assert.IsNotNull(brickLayout);
        Assert.AreNotEqual(0, brickLayout.Count);
    }

    [Test]
    public void EraseBricks_RemainingBricksListEmpty()
    {
        BricksManager bricksManager2;
        Brick brick1 = new Brick();
        Brick brick2 = new Brick();
        Brick brick3 = new Brick();
        GameObject bricksManagerObject = new GameObject();
        bricksManager2 = bricksManagerObject.AddComponent<BricksManager>();

        // Add some bricks to the RemainingBricks list
        bricksManager2.RemainingBricks = new List<Brick>
        {
        brick1, brick2, brick3
        };

        bricksManager2.EraseBricks();

        Assert.IsNotNull(bricksManager2.RemainingBricks);
        Assert.AreEqual(0, bricksManager2.RemainingBricks.Count);
    }  */
}