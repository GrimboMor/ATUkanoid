using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    #region Singleton
    //creating a single instance of the BallsManagerScript script

    private static SoundEffectPlayer _instance;

    public static SoundEffectPlayer Instance => _instance;

    //checking that there is no other instances of the game manger running
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public AudioSource AudSrc;
    public AudioClip sfxBounce, sfxBrickHit, sfxBrickBreak, sfxLoseBall, sfxVictory, sfxGameOver, sfxOneUp, sfxShrink,sfxGrow,sfxBallHit;

    public void Bounce()
    {
        AudSrc.clip = sfxBounce;
        AudSrc.Play();
    }
    public void BrickHit()
    {
        AudSrc.clip = sfxBrickHit;
        AudSrc.Play();
    }
    public void BrickBreak()
    {
        AudSrc.clip = sfxBrickBreak;
        AudSrc.Play();
    }
    public void LoseBall()
    {
        AudSrc.clip = sfxLoseBall;
        AudSrc.Play();
    }
    public void Victory()
    {
        AudSrc.clip = sfxVictory;
        AudSrc.Play();
    }
    public void GameOver()
    {
        AudSrc.clip = sfxGameOver;
        AudSrc.Play();
    }
    public void OneUp()
    {
        AudSrc.clip = sfxOneUp;
        AudSrc.Play();
    }
    public void ShrinkPad()
    {
        AudSrc.clip = sfxShrink;
        AudSrc.Play();
    }
    public void GrowPad()
    {
        AudSrc.clip = sfxGrow;
        AudSrc.Play();
    }

    public void BallHitBall()
    {
        AudSrc.clip = sfxBallHit;
        AudSrc.Play();
    }
}
