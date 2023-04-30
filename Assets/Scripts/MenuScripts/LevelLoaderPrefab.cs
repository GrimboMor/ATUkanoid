using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LevelLoaderPrefab : MonoBehaviour
{
    public string levelName;
    public Image LoadLevelImage;
    public TextMeshProUGUI LevelLoadTextHS;
    public TextMeshProUGUI LevelLoadTextTP;
    public Button LevelLoadButton;

    private void Awake()
    {
        // Assign the OnButtonClick method to the button's onClick event
        LevelLoadButton.onClick.AddListener(OnButtonClick);
    }

    public void Setup(string levelName)
    {
        this.levelName = levelName;

        // Load the level image
        Sprite levelSprite = Resources.Load<Sprite>(levelName + "_Load");
        if (levelSprite != null)
        {
            LoadLevelImage.sprite = levelSprite;
        }
        else
        {
            Debug.LogWarning("Image file not found for level: " + levelName);
        }

        // Load the HS and TP from the JSON file
        string jsonFilePath = Path.Combine(Application.persistentDataPath, levelName + ".json");
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonData);

            LevelLoadTextHS.text = "High Score: " + levelData.HighScore.ToString();
            LevelLoadTextTP.text = "Times Played: " + levelData.TimesPlayed.ToString();
        }
        else
        {
            Debug.LogWarning("JSON file not found for level: " + levelName);
        }
    }

    public void OnButtonClick()
    {
        string jsonFilePath = Path.Combine(Application.persistentDataPath, levelName + ".json");

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonData);

            // Increment the TimesPlayed value
            levelData.TimesPlayed++;

            // Save the updated data back to the JSON file
            string updatedJsonData = JsonUtility.ToJson(levelData);
            File.WriteAllText(jsonFilePath, updatedJsonData);

            // Update the displayed TimesPlayed value in the prefab
            LevelLoadTextTP.text = "Times Played: " + levelData.TimesPlayed.ToString();
        }
        else
        {
            Debug.LogWarning("JSON file not found for level: " + levelName);
        }

        PlayerPrefs.SetString("LevelToLoad", levelName);
        SceneManager.LoadScene(1);
    }
}