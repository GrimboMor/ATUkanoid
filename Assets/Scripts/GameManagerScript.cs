using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    #region Singleton
    //creating a single instance of the gamemanager script

    private static GameManagerScript _instance;

    public static GameManagerScript Instance => _instance;

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
    // Bool to see if the game is started
    public bool IsGameStarted { get; set; }

    // Setting the game resolution - this is static for this project
    static void Start()
    {
        Screen.SetResolution(1920, 1080, false);
    }
    void Update()
    {
        // Hide the cursor when the game is started
        if (IsGameStarted)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}