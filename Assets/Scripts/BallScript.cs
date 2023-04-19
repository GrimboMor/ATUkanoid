using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BallScript : MonoBehaviour
{
    // Add Sparks when the ball pits the paddle
    public ParticleSystem Sparks;

    private void OnCollisionEnter2D(Collision2D collision)
    {
            //Debug.Log("Collision detected with: " + collision.gameObject.tag);
            if (collision.gameObject.CompareTag("Paddle"))
            {
                CreateSparks();
            }
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Brick"))
            {
                SoundEffectPlayer.Instance.BrickHit();
                //LogBallSpeed();
            }
    }

    private void CreateSparks()
    {
        Vector3 ballPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(ballPos.x, ballPos.y - 0.25f, ballPos.z);

        float ballStartSpeed = BallsManagerScript.Instance.bmBallStartSpeed;
        float ballCurrentSpeed = BallsManagerScript.Instance.bmBallCurrentSpeed;
        float ballMaxSpeed = BallsManagerScript.Instance.bmBallMamimumSpeed;

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
}
