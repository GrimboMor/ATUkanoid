using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            SoundEffectPlayer.Instance.LoseBall();
            BallScript ball = collision.gameObject.GetComponent<BallScript>();
            if (ball != null)
            {
                BallsManagerScript.Instance.BallsList.Remove(ball);
                GameManagerScript.Instance.ActiveBalls = BallsManagerScript.Instance.BallsList.Count;
                Destroy(ball.gameObject);
            }
        }
    }
}
