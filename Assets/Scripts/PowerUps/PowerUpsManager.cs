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

    [Range(0f, 100f)]
    public float PowUpChance;

    [Range(0f, 100f)]
    public float PowDownChance;


}
