using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        UIManager.Instance.ShowPowUpText("Extra Life");
        GameManagerScript.Instance.Lives = GameManagerScript.Instance.Lives + 1;
        GameManagerScript.Instance.Score = GameManagerScript.Instance.Score + (250* GameManagerScript.Instance.ScoreMulti);
        SoundEffectPlayer.Instance.OneUp();
        //Track the Stats
        UIManager.Instance.StatCollectPowUps = UIManager.Instance.StatCollectPowUps + 1;
        UIManager.Instance.StatScorePowUps = UIManager.Instance.StatScorePowUps + (250 * GameManagerScript.Instance.ScoreMulti);
    }
}
