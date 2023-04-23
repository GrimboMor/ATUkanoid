using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Paddle")
        {
            this.ApplyPowerUp();
        }

        if (collision.tag == "Paddle" || collision.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
    }

    protected abstract void ApplyPowerUp();
}
