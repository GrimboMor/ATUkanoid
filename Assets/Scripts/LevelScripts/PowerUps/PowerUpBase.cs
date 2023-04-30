using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    // Define the game area boundaries
    private float minY = -5f;
    private float maxY = 10f;
    private float minX = -12f;
    private float maxX = 12f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Paddle")
        {
            this.ApplyPowerUp();
        }

        if (collision.tag == "Paddle")
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        // Check if the powerup is outside the game area
        if (this.transform.position.y < minY || this.transform.position.y > maxY || this.transform.position.x < minX || this.transform.position.x > maxX)
        {
            Destroy(this.gameObject);
        }
    }

    protected abstract void ApplyPowerUp();
}
