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
    private BoxCollider2D box2D;
    private int ScoreToAdd = 5;
    private int ScoreHitCount = 0;

    private void Awake()
    {
        //Get the colour for the brick and particles
        this.SR = this.GetComponent<SpriteRenderer>();
        this.box2D = this.GetComponent<BoxCollider2D>();
        BallScript.OnLaserBallSEnabled += OnLaserBallSEnabled;
        BallScript.OnLaserBallSDisabled += OnLaserBallSDisabled;
    }

    private void OnLaserBallSEnabled(BallScript obj)
    {
        if (this != null)
        {
            this.box2D.isTrigger = true;
        }
    }
    private void OnLaserBallSDisabled(BallScript obj)
    {
        if (this != null)
        {
            this.box2D.isTrigger = false;
        }
    }

    public static event Action<Brick> OnBrickDestroyed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallScript ball = collision.gameObject.GetComponent<BallScript>();
        ApplyCollisionFunction(ball);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallScript ball = collision.gameObject.GetComponent<BallScript>();
        ApplyLaserCollisionFunction(ball);
    }

    private void ApplyCollisionFunction(BallScript ball)
    {
        this.BrickHealth--;
        this.BrickSprite--;
        this.ScoreHitCount++;

        if (this.BrickHealth <= 0)
        {
            BricksManager.Instance.RemainingBricks.Remove(this);
            //spawn particles + destroy brick
            OnBrickDestroyed?.Invoke(this);
            SpawnBrickDestroyed();
            SpawnPowerUp();
            SoundEffectPlayer.Instance.BrickBreak();
            Destroy(this.gameObject);
            GameManagerScript.Instance.RemainingBricks = BricksManager.Instance.RemainingBricks.Count;
            AddScoreToGM(ScoreToAdd, ScoreHitCount);
        }
        else
        {
            this.SR.sprite = BricksManager.Instance.Sprites[this.BrickSprite - 1];
        }
    }

    private void ApplyLaserCollisionFunction(BallScript ball)
    {
        this.BrickHealth = -10000;
        BricksManager.Instance.RemainingBricks.Remove(this);
        OnBrickDestroyed?.Invoke(this);
        SpawnBrickDestroyed();
        SpawnPowerUp();
        SoundEffectPlayer.Instance.BrickBreak();
        Destroy(this.gameObject);
        GameManagerScript.Instance.RemainingBricks = BricksManager.Instance.RemainingBricks.Count;
        AddScoreToGM(ScoreToAdd, 2);
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

    private void SpawnPowerUp()
    {
        float powUpSpawnChance = UnityEngine.Random.Range(0, 100f);
        float powDownSpawnChance = UnityEngine.Random.Range(0, 100f);
        bool thisBrickAlreadySpawned = false;

        if (powUpSpawnChance <= PowerUpsManager.Instance.PowUpChance)
        {
            thisBrickAlreadySpawned = true;
            PowerUpBase newPowUp = this.SpawnPowerUp(true);
            AddForceToPowerUp(newPowUp);
        }

        if (powDownSpawnChance <= PowerUpsManager.Instance.PowDownChance && !thisBrickAlreadySpawned)
        {
            PowerUpBase newPowDown = this.SpawnPowerUp(false);
            AddForceToPowerUp(newPowDown);
        }
    }

    private PowerUpBase SpawnPowerUp(bool powPow)
    {
        List<PowerUpBase> powType;

        if (powPow)
        {
            powType = PowerUpsManager.Instance.AvailablePowerUps;
        }
        else
        {
            powType = PowerUpsManager.Instance.AvailablePowerDowns;
        }

        int powIndex = UnityEngine.Random.Range(0, powType.Count);
        PowerUpBase prefab = powType[powIndex];
        PowerUpBase newPowerUp = Instantiate(prefab, this.transform.position, Quaternion.identity) as PowerUpBase;
        return newPowerUp;
    }

    private void AddForceToPowerUp(PowerUpBase powerUp)
    {
        // Making sure the Power-ups have a Rigidbody2D component.
        Rigidbody2D rb = powerUp.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = powerUp.gameObject.AddComponent<Rigidbody2D>();
        }

        // Add a random direction (left or right) and an upwards bump.
        float randomXForce = UnityEngine.Random.Range(-45f, 45f);
        Vector2 force = new Vector2(randomXForce, 250f).normalized * .75f;
        rb.AddForce(force, ForceMode2D.Impulse);

        // Start a coroutine to gradually decrease the upward force.
        StartCoroutine(ApplyGravity(rb, 1.1f));
    }

    private IEnumerator ApplyGravity(Rigidbody2D rb, float duration)
    {
        float elapsedTime = 0;
        float initialYForce = 1f;
        float targetYForce = -100f;
        float initialGravity = 1f;
        float targetGravity = 1000f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the new vertical force and gravity force.
            float newYForce = Mathf.Lerp(initialYForce, targetYForce, elapsedTime / duration);
            float newGravityForce = Mathf.Lerp(initialGravity, targetGravity, elapsedTime / duration);

            // Apply the new vertical force.
            rb.AddForce(new Vector2(0f, newYForce) * Time.deltaTime, ForceMode2D.Force);

            // Apply the new gravity force.
            rb.gravityScale = newGravityForce;

            yield return null;
        }
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
            GameManScore = ScoreToAdd * 2;
            GameManagerScript.Instance.ChangeScore(GameManScore);
        }
        else if (hits == 3)
        {
            GameManScore = ScoreToAdd * 5;
            GameManagerScript.Instance.ChangeScore(GameManScore);
        }
    }
}