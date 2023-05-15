using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplodingBricks : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        UIManager.Instance.ShowPowUpText("Exploding Bricks");
        GameManagerScript.Instance.explodingBricks = true;
        GameManagerScript.Instance.Score += (40 * GameManagerScript.Instance.ScoreMulti);
        SoundEffectPlayer.Instance.PowerUp();
        //Track the Stats
        UIManager.Instance.StatCollectPowUps = UIManager.Instance.StatCollectPowUps + 1;
        UIManager.Instance.StatScorePowUps = UIManager.Instance.StatScorePowUps + (40 * GameManagerScript.Instance.ScoreMulti);
    }
}
