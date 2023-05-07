using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridLoader : MonoBehaviour
{
    public GameObject levelCellPrefab;
    public Transform gridTransform;

    public int startLevelNumb = 1;
    public int endLevelNumb = 2;
    private List<string> levels = new List<string>();

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
          /*  foreach (string levelName in levels)
            {
                string filePath = Path.Combine(Application.persistentDataPath, levelName + ".json");
                LevelData levelData
                {
                    HighScore = 0;
                    TimesPlayed = 0;
                }
            }*/
        }
    }
}

[System.Serializable]
public class LevelData
{
    public int HighScore;
    public int TimesPlayed;
}
