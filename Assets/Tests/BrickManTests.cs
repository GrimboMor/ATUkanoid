using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class BrickManTests
{
    private BricksManager bricksManager;
    private GameObject bricksManagerObject;

    [SetUp]
    public void Setup()
    {
        bricksManagerObject = new GameObject();
        bricksManager = bricksManagerObject.AddComponent<BricksManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(bricksManagerObject);
    }

    [Test]
    public void BricksManagerSingletonTest()
    {
        Assert.IsNotNull(BricksManager.Instance);
    }

    [Test]
    public void BricksManagerBrickPrefabTest()
    {
        Assert.IsNull(bricksManager.brickPrefab);
    }

    [Test]
    public void BricksManagerSpritesTest()
    {
        Assert.IsNotNull(bricksManager.Sprites);
    }

    [Test]
    public void BricksManagerBrickColourTest()
    {
        Assert.IsNotNull(bricksManager.BrickColour);
    }

    [Test]
    public void BricksManagerRemainingBricksTest()
    {
        Assert.IsNotNull(bricksManager.RemainingBricks);
    }

    [Test]
    public void BricksManagerBrickLayoutTest()
    {
        Assert.IsNotNull(bricksManager.BrickLayout);
    }

    [Test]
    public void BricksManagerColourLayoutTest()
    {
        Assert.IsNotNull(bricksManager.ColourLayout);
    }

    [Test]
    public void BricksManagerSpriteLayoutTest()
    {
        Assert.IsNotNull(bricksManager.SpriteLayout);
    }

    [Test]
    public void BricksManagerTotalBrickCountTest()
    {
        Assert.AreEqual(0, bricksManager.TotalBrickCount);
    }

    [Test]
    public void BricksManagerCurrentLevelTest()
    {
        Assert.AreEqual(0, bricksManager.CurrentLevel);
    }
}