using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserBall : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        foreach (BallScript ball in BallsManagerScript.Instance.BallsList.ToList())
        {
            ball.StartLaserBall();
        }

        GameManagerScript.Instance.Score += (40 * GameManagerScript.Instance.ScoreMulti);
        SoundEffectPlayer.Instance.GrowPad();
        //Track the Stats
        UIManager.Instance.StatCollectPowUps = UIManager.Instance.StatCollectPowUps + 1;
        UIManager.Instance.StatScorePowUps = UIManager.Instance.StatScorePowUps + (40 * GameManagerScript.Instance.ScoreMulti);
    }
}
