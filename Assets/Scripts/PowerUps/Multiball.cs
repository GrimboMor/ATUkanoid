using System.Linq;

public class Multiball : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        foreach (BallScript ball in BallsManagerScript.Instance.BallsList.ToList())
        {
            BallsManagerScript.Instance.MultiBalls(ball.gameObject.transform.position,2);
        }
    }    
}
