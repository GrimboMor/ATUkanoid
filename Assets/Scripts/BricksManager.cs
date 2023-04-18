using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BricksManager : MonoBehaviour
{
    #region Singleton
    //creating a single instance of the BrickManager Script

    private static BricksManager _instance;

    public static BricksManager Instance => _instance;

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

    // File used for level generation. The colour file will always be the level name with a C at the end of it
    // so I set that as a private variable and update it in the start code block.
    public string LevelTextFile = "Level_0001";
    private string LevelColorTextFile;
    private string LevelSpriteTextFile;

    private int maximumRows = 11;
    private int maximumColumns = 17;
    private GameObject BrickManagerBrickList;
    private float firstBrickX = -7.57f;
    private float firstBrickY = 3.25f;
    private float nextBrickSpacerX = 0.945f;
    private float nextBrickSpacerY = 0.510f;

    public Brick brickPrefab;

    public Sprite[] Sprites;
    public Color[] BrickColour;

    public List<Brick> RemainingBricks { get; set; }

    public List<int[,]> BrickLayout { get; set; }
    public List<int[,]> ColourLayout { get; set; }

    public List<int[,]> SpriteLayout { get; set; }
    public int TotalBrickCount { get; set; }

    private int CurrentLevel;

    private void Start()
    {
        LevelColorTextFile = LevelTextFile + "C";
        LevelSpriteTextFile = LevelTextFile + "S";
        this.BrickManagerBrickList = new GameObject("BrickList");
        this.BrickLayout = this.LoadBrickLayout(LevelTextFile);
        this.ColourLayout = this.LoadBrickLayout(LevelColorTextFile);
        this.SpriteLayout = this.LoadBrickLayout(LevelSpriteTextFile);
        this.LevelGeneration();
    }

    //Code to generate the brick layout by passing the level files to LoadBrickLayout and getting a 
    //list of INTs back. First list "currentLevelData" is used for brick placement and brick health
    //second list currentColourData is used for the brick colour data.
    private void LevelGeneration()
    {
        this.RemainingBricks = new List<Brick>();
        // This next line is for all levels in one text file loading the current level from a matrix
        int[,] currentLevelData = this.BrickLayout[this.CurrentLevel];
        int[,] currentColourData = this.ColourLayout[this.CurrentLevel];
        int[,] currentSpriteData = this.SpriteLayout[this.CurrentLevel];
        //float currentSpawnX = firstBrickX;
        float currentSpawnY = firstBrickY;
        float zShifter = 0;

        for (int row = 0; row < this.maximumRows; row++)
        {
            float currentSpawnX = firstBrickX;
            for (int col = 0; col < this.maximumColumns; col++)
            {
                int brickType = currentLevelData[row, col];
                int brickColour = currentColourData[row, col];
                int brickSprite = currentSpriteData[row, col];
                if (brickType > 0)
                {
                    Brick newBrick = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShifter), Quaternion.identity) as Brick;
                    newBrick.Init(BrickManagerBrickList.transform, this.Sprites[brickSprite - 1], brickSprite, this.BrickColour[brickColour], brickType);

                    this.RemainingBricks.Add(newBrick);
                    zShifter -= 0.0001f;

                    currentSpawnX += nextBrickSpacerX;
                }
                else
                {
                    currentSpawnX += nextBrickSpacerX;
                }
            }

            currentSpawnY -= nextBrickSpacerY;
        }

        this.TotalBrickCount = this.RemainingBricks.Count;
        //Set the GameManager Brick counts
        GameManagerScript.Instance.TotalBricks = this.RemainingBricks.Count;
        GameManagerScript.Instance.RemainingBricks = this.RemainingBricks.Count;
    }

    private List<int[,]> LoadBrickLayout(string TextFile)
    {
        TextAsset text = Resources.Load(TextFile) as TextAsset;
        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        List<int[,]> brickLayout = new List<int[,]>();
        int[,] currentLevel = new int[maximumRows, maximumColumns];
        int currentRow = 0;

        for (int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];

            if (line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < bricks.Length; col++)
                {
                    currentLevel[currentRow, col] = int.Parse(bricks[col]);
                }
                currentRow++;
            }
            else
            {
                currentRow = 0;
                // This is for all levels in one text file
                brickLayout.Add(currentLevel);
                currentLevel = new int[maximumRows, maximumColumns];
            }
        }
        return brickLayout;
    }

    // This can be called to quickly remove all the bricks for testing
    private void EraseBricks()
    {
        foreach (Brick brick in this.RemainingBricks.ToList())
        {
            Destroy(brick.gameObject);
        }
    }
}