using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreX2 : PowerUpBase
{
        protected override void ApplyPowerUp()
    {
        //Track the Stats
        UIManager.Instance.StatCollectPowDown = UIManager.Instance.StatCollectPowDown + 1;
        UIManager.Instance.StatScorePowUps = UIManager.Instance.StatScorePowUps + (5 * GameManagerScript.Instance.ScoreMulti);

        GameManagerScript.Instance.Score = GameManagerScript.Instance.Score + (5 * GameManagerScript.Instance.ScoreMulti);

        //Then change the score multiplier
        if (GameManagerScript.Instance.ScoreMulti >= 1 && GameManagerScript.Instance.ScoreMulti < 32)
        {
            GameManagerScript.Instance.ScoreMulti *= 2;
        }
        GameManagerScript.Instance.UpdateScoreMultiplierText();
    }
}
