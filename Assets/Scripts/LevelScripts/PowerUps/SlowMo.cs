using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlowMo : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        BallsManagerScript.Instance.SlowBalls();
        GameManagerScript.Instance.Score = GameManagerScript.Instance.Score + (10 * GameManagerScript.Instance.ScoreMulti);
        SoundEffectPlayer.Instance.OneUp();
        
        //Track the Stats
        UIManager.Instance.StatCollectPowUps = UIManager.Instance.StatCollectPowUps + 1;
        UIManager.Instance.StatScorePowUps = UIManager.Instance.StatScorePowUps + (10 * GameManagerScript.Instance.ScoreMulti);
       
    }
}
