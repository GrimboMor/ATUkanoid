using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrowPaddle : PowerUpBase
{
    private readonly float[] paddleSizes = { 1.2f, 1.6f, 2f, 3f, 3.6f };

    protected override void ApplyPowerUp()
    {
        float currentSize = PaddleScript.Instance.GetPaddleSize();
        int currentIndex = Array.IndexOf(paddleSizes, currentSize);
        UIManager.Instance.ShowPowUpText("Grow Paddle");

        if (currentIndex < paddleSizes.Length - 1)
        {
            PaddleScript.Instance.ChangePaddleSizeAnimated(paddleSizes[currentIndex + 1]);
        }
        GameManagerScript.Instance.Score += (50 * GameManagerScript.Instance.ScoreMulti);
        SoundEffectPlayer.Instance.GrowPad();
        //Track the Stats
        UIManager.Instance.StatCollectPowUps = UIManager.Instance.StatCollectPowUps + 1;
        UIManager.Instance.StatScorePowUps = UIManager.Instance.StatScorePowUps + (50 * GameManagerScript.Instance.ScoreMulti);
        UIManager.Instance.StatPadResized = UIManager.Instance.StatPadResized + 1;
    }
}
