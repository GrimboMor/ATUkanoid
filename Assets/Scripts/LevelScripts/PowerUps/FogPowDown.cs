using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogPowDown : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        FogScript.Instance.CreateFog();
        //Track the Stats
        UIManager.Instance.StatCollectPowDown = UIManager.Instance.StatCollectPowDown + 1;
        UIManager.Instance.StatScorePowDown = UIManager.Instance.StatScorePowDown + (600 * GameManagerScript.Instance.ScoreMulti);
        GameManagerScript.Instance.Score = GameManagerScript.Instance.Score + (600 * GameManagerScript.Instance.ScoreMulti);
    }
}