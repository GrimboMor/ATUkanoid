using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    public int BrickHealth = 1;
    public ParticleSystem BrickDestroyed;
    private SpriteRenderer SR;

    private void Awake()
    {
        //Get the colour for the brick and particles
        this.SR = this.GetComponent<SpriteRenderer>();
    }

    public static event Action<Brick> OnBrickDestroyed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallScript ball = collision.gameObject.GetComponent<BallScript>();
        ApplyCollisionFunction(ball);
    }

    private void ApplyCollisionFunction(BallScript ball)
    {
        this.BrickHealth--;

        if (this.BrickHealth <= 0)
        {
            //spawn particles + destryo brick
            OnBrickDestroyed?.Invoke(this);
            SpawnBrickDestroyed();
            Destroy(this.gameObject);
            //add score
        }
        else
        {
            this.SR.sprite = BricksManager.Instance.Sprites[this.BrickHealth - 1];
            //add score
        }
    }

    private void SpawnBrickDestroyed()
    {
        Vector3 BrickPos = gameObject.transform.position;   
        Vector3 SpawnPos = new Vector3(BrickPos.x, BrickPos.y, BrickPos.z);
        GameObject effect = Instantiate(BrickDestroyed.gameObject, SpawnPos, Quaternion.identity);
    
        MainModule MM = effect.GetComponent<ParticleSystem>().main; 
        MM.startColor = this.SR.color;
        Destroy(effect, BrickDestroyed.main.startLifetime.constant);
    }

    public void Init(Transform containerTransform, Sprite sprite, Color colour, int health)
    {
        this.transform.SetParent(containerTransform);
        this.SR.sprite = sprite;
        this.SR.color = colour;
        this.BrickHealth = health;
     }
}
