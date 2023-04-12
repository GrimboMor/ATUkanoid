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
            BallsManagerScript.Instance.BallsList.Remove(ball);
            ball.BallDeath();

        }
    }
}
