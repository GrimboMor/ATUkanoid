using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoreDivide2 : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        //Track the Stats
        UIManager.Instance.StatCollectPowDown = UIManager.Instance.StatCollectPowDown + 1; ;
        UIManager.Instance.StatScorePowDown = UIManager.Instance.StatScorePowDown + (800 * GameManagerScript.Instance.ScoreMulti);

        GameManagerScript.Instance.Score = GameManagerScript.Instance.Score + (800 * GameManagerScript.Instance.ScoreMulti);
        UIManager.Instance.ShowPowDownText("Score Divider");

        //Then change the score multiplier
        if (GameManagerScript.Instance.ScoreMulti > 1)
        {
            GameManagerScript.Instance.ScoreMulti /= 2;
        }
        GameManagerScript.Instance.UpdateScoreMultiplierText();
    }
}
