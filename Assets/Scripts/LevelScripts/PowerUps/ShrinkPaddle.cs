using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShrinkPaddle : PowerUpBase
{
    private readonly float[] paddleSizes = { 1.2f, 1.6f, 2f, 3f, 3.6f };

    protected override void ApplyPowerUp()
    {
        float currentSize = PaddleScript.Instance.GetPaddleSize();
        int currentIndex = Array.IndexOf(paddleSizes, currentSize);

        if (currentIndex > 0)
        {
            PaddleScript.Instance.ChangePaddleSizeAnimated(paddleSizes[currentIndex - 1]);
        }
        GameManagerScript.Instance.Score += (300 * GameManagerScript.Instance.ScoreMulti);
        SoundEffectPlayer.Instance.ShrinkPad();
        //Track the Stats
        UIManager.Instance.StatCollectPowDown = UIManager.Instance.StatCollectPowDown + 1;
        UIManager.Instance.StatScorePowDown = UIManager.Instance.StatScorePowDown + (300 * GameManagerScript.Instance.ScoreMulti);
        UIManager.Instance.StatPadResized = UIManager.Instance.StatPadResized + 1;
    }
}
