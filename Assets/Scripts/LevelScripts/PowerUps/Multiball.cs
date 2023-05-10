using System.Linq;

public class Multiball : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        UIManager.Instance.ShowPowUpText("Multiball");

        foreach (BallScript ball in BallsManagerScript.Instance.BallsList.ToList())
        {
            BallsManagerScript.Instance.MultiBalls(ball.gameObject.transform.position, 2,ball.isALaserBall);
            GameManagerScript.Instance.Score = GameManagerScript.Instance.Score + (50 * GameManagerScript.Instance.ScoreMulti);
            SoundEffectPlayer.Instance.OneUp();
            //Track the Stats
            UIManager.Instance.StatCollectPowUps = UIManager.Instance.StatCollectPowUps + 1;
            UIManager.Instance.StatScorePowUps = UIManager.Instance.StatScorePowUps + (50 * GameManagerScript.Instance.ScoreMulti);
        }
    }    
}
