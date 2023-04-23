using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    #region Singleton
    //creating a single instance of the TimerManager script

    private static TimerManager _instance;

    public static TimerManager Instance => _instance;

    //checking that there is no other instances of the TimerSpeedManager running
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

    public float duration = 40f;
    private float startTime;
    private float elapsedTime;
    private bool timerActive;

    void Start()
    {
        timerActive = false;
    }

    void Update()
    {
        if (timerActive)
        {
            elapsedTime = Time.time - startTime;

            if (elapsedTime >= duration)
            {
                timerActive = false;
            }
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timerActive = true;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        timerActive = false;
    }

    public float GetCompletion()
    {
        return Mathf.Clamp01(elapsedTime / duration);
    }
}
/*

// This variable is needed to reference to the coroutine, so that I can stop / restart it
public Coroutine increasePaddleBounceSpeedCoroutine;
    public Coroutine increaseBounceSpeedCoroutine;


    public IEnumerator IncreasePaddleBounceSpeed(float startSpeed, float resetTimeCount)
    {
        float elapsedTime = resetTimeCount;
        float currentSpeed = startSpeed;

        while (currentSpeed < BallsManagerScript.Instance.bmBallMaximumSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 40f;
            currentSpeed = Mathf.Lerp(startSpeed, BallsManagerScript.Instance.bmBallMaximumSpeed, t);
            BallsManagerScript.Instance.bmBallCurrentSpeed = currentSpeed;
            yield return null;

        }
        BallsManagerScript.Instance.bmBallCurrentSpeed = BallsManagerScript.Instance.bmBallMaximumSpeed;
    }


    public void AdjustBallSpeeds()
    {
        foreach (var ball in BallsManagerScript.Instance.BallsList)
        {
            Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
            Vector2 currentVelocity = ballRigidbody.velocity;
            float currentSpeed = currentVelocity.magnitude;
            float newSpeed = BallsManagerScript.Instance.bmBallCurrentSpeed;

            if (Mathf.Approximately(currentSpeed, newSpeed))
            {
                continue;
            }

            Vector2 newVelocity = currentVelocity.normalized * newSpeed;
            ballRigidbody.velocity = newVelocity;
        }
    }

}
*/