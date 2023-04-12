using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BallScript : MonoBehaviour
{
    // Added to check the object is not null when called later in BallDeath method
    public static event Action<BallScript> OnBallDeath;

    // Add Sparks when the ball pits the paddle
    public ParticleSystem Sparks;

    // Get the Ball Speeds from the BallsManagerScript
    private BallsManagerScript ballsManager; 

    private void Start()
    {
        ballsManager = GameObject.FindObjectOfType<BallsManagerScript>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            CreateSparks();
        }
        if (collision.gameObject.tag == "Wall")
        {
            SoundEffectPlayer.Instance.BrickHit();
        }
    }

    // Original basic code to always creat sparks
    /*private void CreateSparks()
    {
        Vector3 BallPos = gameObject.transform.position;
        Vector3 SpawnPos = new Vector3(BallPos.x, BallPos.y - 0.3f, BallPos.z);
        GameObject effect = Instantiate(Sparks.gameObject, SpawnPos, Quaternion.identity);
        Destroy(effect, Sparks.main.startLifetime.constant);
    }*/

    //Updated code to create sparks based scaled in size to the ball speed
    private void CreateSparks()
    {
        Vector3 ballPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(ballPos.x, ballPos.y - 0.25f, ballPos.z);

        float ballStartSpeed = ballsManager.bmBallStartSpeed;
        float ballCurrentSpeed = ballsManager.bmBallCurrentSpeed;
        float ballMaxSpeed = ballsManager.bmBallMamimumSpeed;

        float sparksScale = Mathf.Lerp(0f, 0.7f, (ballCurrentSpeed - ballStartSpeed) / (ballMaxSpeed - ballStartSpeed));

        // Set the scale of the particle system
        var main = Sparks.main;
        main.startSizeMultiplier = sparksScale;

        GameObject effect = Instantiate(Sparks.gameObject, spawnPos, Quaternion.identity);
        Destroy(effect, main.startLifetime.constant);
    }

    public void BallDeath()
    {
        //First check the ball object is not null, then destroy it
        OnBallDeath?.Invoke(this);
        Destroy(gameObject);
    }

}
