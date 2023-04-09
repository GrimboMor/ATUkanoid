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
        if (collision.gameObject.tag == "Paddle")
        {
            CreateSparks();
        }
    }

    private void CreateSparks()
    {
        Vector3 BallPos = gameObject.transform.position;
        Vector3 SpawnPos = new Vector3(BallPos.x, BallPos.y - 0.3f, BallPos.z);
        GameObject effect = Instantiate(Sparks.gameObject, SpawnPos, Quaternion.identity);
        Destroy(effect, Sparks.main.startLifetime.constant);
    }
}
