using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public string LevelTextFile = "Level_0001";

    private int maximumRows = 12;
    private int maximumColumns = 18;
    private GameObject BrickManagerBrickList;
    private float firstBrickX = -7.3f;
    private float firstBrickY = 3.65f;
    private float nextBrickSpacerX = .915f;
    private float nextBrickSpacerY = .525f;

    public Brick brickPrefab;

    public Sprite[] Sprites;
    public Color[] BrickColour;

    public List<Brick> RemainingBricks { get; set; }

    public List<int[,]> BrickLayout { get; set; }
    public int TotalBrickCount { get; set; }

    public int CurrentLevel;

    private void Start()
    {
        this.RemainingBricks = new List<Brick>();
        this.BrickManagerBrickList = new GameObject("BrickList");
        this.BrickLayout = this.LoadBrickLayout();
        this.LevelGeneration();
    }

    private void LevelGeneration()
    {
        // This next line is for all levels in one text file loading the current level from a matrix
        int[,] currentLevelData = this.BrickLayout[this.CurrentLevel];
        //float currentSpawnX = firstBrickX;
        float currentSpawnY = firstBrickY;
        float zShifter = 0;

        for (int row = 0; row < this.maximumRows; row++)
        {
            float currentSpawnX = firstBrickX;
            for (int col = 0; col < this.maximumColumns; col++)
            {
                int brickType = currentLevelData[row, col];
                if (brickType > 0)
                {
                    Brick newBrick = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShifter), Quaternion.identity) as Brick;
                    newBrick.Init(BrickManagerBrickList.transform, this.Sprites[brickType - 1], this.BrickColour[brickType], brickType);

                    this.RemainingBricks.Add(newBrick);
                    zShifter += 0.0001f;

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
    }

    private List<int[,]> LoadBrickLayout()
    {
        TextAsset text = Resources.Load(LevelTextFile) as TextAsset;
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
}
