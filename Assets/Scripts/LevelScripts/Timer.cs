using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float duration = 30f;
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

    public bool IsTimerActive()
    {
        return timerActive;
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




}
*/