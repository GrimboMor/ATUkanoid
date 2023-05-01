using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BallScript : MonoBehaviour
{
    // Add Sparks when the ball pits the paddle
    public ParticleSystem Sparks;
    public bool isALaserBall;
    public ParticleSystem LaserEffect;
    public static event Action<BallScript> OnLaserBallSEnabled;
    public static event Action<BallScript> OnLaserBallSDisabled;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision detected with: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Paddle"))
        {
            CreateSparks();
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            BallsManagerScript.Instance.UpdateBallBounceSpeed();
            BallsManagerScript.Instance.UpdateAllBallsSpeed();
            SoundEffectPlayer.Instance.BallHitBall();
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Brick"))
        {

            if (!GameManagerScript.Instance.IsGameStarted)
            {
                BallsManagerScript.Instance.LaunchFirstBall();
            }

            BallsManagerScript.Instance.UpdateBallBounceSpeed();
            BallsManagerScript.Instance.UpdateAllBallsSpeed();
            SoundEffectPlayer.Instance.BrickHit();
            //UI Stat tracking
            UIManager.Instance.StatTotalBallBounces = UIManager.Instance.StatTotalBallBounces + 1;
            //LogBallSpeed();
        }
    }

    private void CreateSparks()
    {
        Vector3 ballPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(ballPos.x, ballPos.y - 0.25f, ballPos.z);

        float ballStartSpeed = BallsManagerScript.Instance.bmBallStartSpeed;
        float ballCurrentSpeed = BallsManagerScript.Instance.bmBallPaddleSpeed;
        float ballMaxSpeed = BallsManagerScript.Instance.bmBallMaximumSpeed;

        float sparksScale = Mathf.Lerp(0f, 0.7f, (ballCurrentSpeed - ballStartSpeed) / (ballMaxSpeed - ballStartSpeed));

        // Set the scale of the particle system
        var main = Sparks.main;
        main.startSizeMultiplier = sparksScale;

        GameObject effect = Instantiate(Sparks.gameObject, spawnPos, Quaternion.identity);
        Destroy(effect, main.startLifetime.constant);
    }

    public void LogBallSpeed()
    {
        Rigidbody2D ballRigidbody = GetComponent<Rigidbody2D>();
        float ballSpeed = ballRigidbody.velocity.magnitude;
        Debug.Log("Ball Speed: " + ballSpeed);
    }

    public void StartLaserBall()
    {
        if (!this.isALaserBall)
        {
            this.isALaserBall = true;
            LaserEffect.gameObject.SetActive(true);

            OnLaserBallSEnabled?.Invoke(this);
        }
    }

    public void DestroyLaserBall()
    {
        OnLaserBallSDisabled?.Invoke(this);
    }
}