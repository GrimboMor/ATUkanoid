using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    #region Singleton
    //creating a single instance of the gamemanager script

    private static PowerUpsManager _instance;

    public static PowerUpsManager Instance => _instance;

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

    public List<PowerUpBase> AvailablePowerUps;
    public List<PowerUpBase> AvailablePowerDowns;


    //Power Up percent timer settings
    private float powerUpSpawnTimer = 0f;
    private const float powerUpBaseSpawnChance = 200f;
    private const float powerUpMinSpawnChance = 0.001f;
    public float PowerUpSpawnCountdown {  get; set; }

    private void Start()
    {
        PowerUpSpawnCountdown = powerUpBaseSpawnChance;
    }

    private void Update()
    {
        // Only run the spawn logic when the game is started
        if (GameManagerScript.Instance.IsGameStarted)
        {
            // Check if at least one second has passed
            if (Time.time - powerUpSpawnTimer >= 1f)
            {
                // Halve the spawn values
                PowerUpSpawnCountdown /= 2f;
                if (PowerUpSpawnCountdown < powerUpMinSpawnChance)
                {
                    PowerUpSpawnCountdown = powerUpMinSpawnChance;
                }
                // Reset the timer
                powerUpSpawnTimer = Time.time;
            }
        }
    }
}
