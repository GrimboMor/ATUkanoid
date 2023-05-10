using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridLoader : MonoBehaviour
{
    #region Singleton
    //creating a single instance of the GridLoader script

    private static GridLoader _instance;

    public static GridLoader Instance => _instance;

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

    public GameObject levelCellPrefab;
    public Transform gridTransform;

    public int startLevelNumb = 1;
    public int endLevelNumb = 9;
    private List<string> levels = new List<string>();
    public ScrollRect scrollRect;

    private void Start()
    {
        // Generate the level list
        PopulateLevelList(startLevelNumb, endLevelNumb);

        // Check and create the JSON files for each level
        CheckAndCreateJsonFiles();

        // Populate the grid with level cells
        PopulateGrid();
    }

    private void PopulateLevelList(int startNumb, int endNumb)
    {
        for (int i = startNumb; i <= endNumb; i++)
        {
            string levelName = "Level_" + i.ToString("D4");
            levels.Add(levelName);
        }
    }

    private void CheckAndCreateJsonFiles()
    {
        foreach (string levelName in levels)
        {
            string filePath = Path.Combine(Application.persistentDataPath, levelName + ".json");

            if (!File.Exists(filePath))
            {
                LevelData levelData = new LevelData
                {
                    HighScore = 0,
                    TimesPlayed = 0
                };

                string json = JsonUtility.ToJson(levelData);
                File.WriteAllText(filePath, json);
            }
        }
    }
    
    private void PopulateGrid()
    {
        // Calculate the grid size and set the panel height
        int numRows = Mathf.CeilToInt((float)endLevelNumb / 4);
        float gridHeight = 15f + (numRows * 260f) + ((numRows - 1) * 25f) + 15f;
        RectTransform gridRect = gridTransform.GetComponent<RectTransform>();
        gridRect.sizeDelta = new Vector2(gridRect.sizeDelta.x, gridHeight);

        foreach (string levelName in levels)
        {
            Debug.Log("Creating Grid Cell: " + levelName);
            GameObject levelCell = Instantiate(levelCellPrefab, gridTransform);
            levelCell.name = levelName;

            TextMeshProUGUI levelText = levelCell.GetComponentInChildren<TextMeshProUGUI>();
            levelText.text = levelName;

            LevelLoaderPrefab levelLoaderPrefab = levelCell.GetComponent<LevelLoaderPrefab>();
            levelLoaderPrefab.Setup(levelName);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Scroll to top of grid, as it loads in the middle of the list by default
        scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, 0f);
        scrollRect.verticalNormalizedPosition = 1f;
    }

    public void ResetStats()
    {
        foreach (string levelName in levels)
            {
                string filePath = Path.Combine(Application.persistentDataPath, levelName + ".json");
                LevelData levelData = new LevelData
                {
                    HighScore = 0,
                    TimesPlayed = 0
                };
                string json = JsonUtility.ToJson(levelData);
                File.WriteAllText(filePath, json);
            }
    }
}

[System.Serializable]
public class LevelData
{
    public int HighScore;
    public int TimesPlayed;
}
