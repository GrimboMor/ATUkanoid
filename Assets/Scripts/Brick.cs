using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    public int BrickHealth = 1;
    public int BrickSprite = 4;
    public ParticleSystem BrickDestroyed;
    private SpriteRenderer SR;
    private int ScoreToAdd = 10;
    private int ScoreHitCount = 0;

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
        this.BrickSprite--;
        this.ScoreHitCount++;

        if (this.BrickHealth <= 0)
        {
            BricksManager.Instance.RemainingBricks.Remove(this);
            GameManagerScript.Instance.RemainingBricks--;
            //spawn particles + destroy brick
            OnBrickDestroyed?.Invoke(this);
            SpawnBrickDestroyed();
            SoundEffectPlayer.Instance.BrickHit();
            SoundEffectPlayer.Instance.BrickBreak();
            Destroy(this.gameObject);
            AddScoreToGM(ScoreToAdd,ScoreHitCount);
        }
        else
        {
            this.SR.sprite = BricksManager.Instance.Sprites[this.BrickSprite - 1];
            SoundEffectPlayer.Instance.BrickHit();
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

    public void Init(Transform containerTransform, Sprite sprite, int spriteID, Color colour, int health)
    {
        this.transform.SetParent(containerTransform);
        this.SR.sprite = sprite;
        this.SR.color = colour;
        this.BrickHealth = health;
        this.BrickSprite = spriteID;
     }

    private void AddScoreToGM(int score, int hits)
    {
        int GameManScore;
        if (hits == 1) 
        {
            GameManScore = ScoreToAdd;
            GameManagerScript.Instance.ChangeScore(GameManScore);
        }
        else if (hits == 2)
        {
            GameManScore = ScoreToAdd*2;
            GameManagerScript.Instance.ChangeScore(GameManScore);
        }
        else if (hits == 3)
        {
            GameManScore = ScoreToAdd * 5;
            GameManagerScript.Instance.ChangeScore(GameManScore);
        }
    }
}
