using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManagerScript : MonoBehaviour
{
    #region Singleton
    //creating a single instance of the BallsManagerScript script

    private static BallsManagerScript _instance;

    public static BallsManagerScript Instance => _instance;

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

    [SerializeField]
    private BallScript ballPrefab;
    private BallScript firstBall;
    private Rigidbody2D firstBallRB;
    private int maxBalls;
    public Timer timer;
    // * Time.deltaTime

    // Here we set the speed for the ball to move
    public float bmBallStartSpeed = 400;
    public float bmBallPaddleSpeed = 400;
    public float bmBallMaximumSpeed = 700;
    public double bmBallBounceSpeed { get; set; }

    //As there will be multiple balls, I need a list to manage them
    public List<BallScript> BallsList { get; set; }

    // Define the game area boundaries
    private float minY = -5f;
    private float maxY = 10f;
    private float minX = -12f;
    private float maxX = 12f;

    private void Start()
    {
        maxBalls = 12;
        InitialiseBall();
    }

    private void Update()
    {
        // First check if the game has started. If not, then stick the first ball to the paddles x position 
        if (GameManagerScript.Instance.IsGameStarted != true)
        {
            if (firstBall != null)
            {
                Vector3 paddlePos = PaddleScript.Instance.gameObject.transform.position;
                Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y + .34f, 0);
                firstBall.transform.position = ballPos;
            }

            //When the mouse is clicked, then the ball will be released and the game will start            
            if (Input.GetMouseButton(0))
            {
                //This check is needed to fix spawning non interactive balls during teh pause when a life is lost
                if (GameManagerScript.Instance.canInteract)
                {
                    LaunchFirstBall();
                }
            }
        }

        //CheckTimerDetails();

        // Check if any ball is outside the game area
        if (this.transform.position.y < minY || this.transform.position.y > maxY || this.transform.position.x < minX || this.transform.position.x > maxX)
        {
            Destroy(this.gameObject);
        }
    }

    public void LaunchFirstBall()
    {
        if (firstBall != null)
        {
            firstBallRB.isKinematic = false;
            timer.StartTimer();
        }

        bmBallPaddleSpeed = bmBallStartSpeed;


        if (firstBall != null)
        {
            firstBallRB.AddForce(new Vector2(5, bmBallPaddleSpeed));
        }
        GameManagerScript.Instance.IsGameStarted = true;
    }

    private void InitialiseBall()
    {
        // The First ball will initialise on top of the paddle, so I need to get the position for that here
        Vector3 paddlePos = PaddleScript.Instance.gameObject.transform.position;
        Vector3 startingPos = new Vector3(paddlePos.x, paddlePos.y + .1f, 0);
        firstBall = Instantiate(ballPrefab, startingPos, Quaternion.identity);
        firstBallRB = firstBall.GetComponent<Rigidbody2D>();

        this.BallsList = new List<BallScript>
        {
            firstBall
        };
        firstBall.DestroyLaserBall();
        GMActiveBallsUpdate();
    }

    public void BallReset()
    {
        timer.ResetTimer();
        //Make sure all previous balls objects are destoyed
        DestroyAllBalls();
        bmBallStartSpeed = 400;
        InitialiseBall();
    }

    private void CheckTimerDetails()
    {
        if (timer == null)
        {
            Debug.LogWarning("Timer component is not assigned in BallsManagerScript");
            return;
        }

        if (timer.IsTimerActive())
        {
            Debug.Log("Current timer value: " + timer.GetCompletion() * timer.duration);
        }
        else
        {
            Debug.Log("The timer is not active");
        }
    }

    public void UpdateBallBounceSpeed()
    {
        // Calculate the current speed based on the timer completion
        float completion = timer.GetCompletion();
        bmBallPaddleSpeed = Mathf.Lerp(bmBallStartSpeed, bmBallMaximumSpeed, completion);

        // Convert the paddle speed to bounce speed based on  + values I got from debugging
        bmBallBounceSpeed = PadtoBounce(bmBallPaddleSpeed, 320, 700, 5.77166, 13.7);
    }

    private static double PadtoBounce(double value, double fromSource, double toSource, double fromTarget, double toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public void UpdateAllBallsSpeed()
    {
        foreach (BallScript ball in BallsList)
        {
            Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
            Vector2 currentVelocity = ballRigidbody.velocity;
            float currentSpeed = currentVelocity.magnitude;
            float newSpeed = (float)bmBallBounceSpeed;
            Vector2 newVelocity = currentVelocity.normalized * newSpeed;
            ballRigidbody.velocity = newVelocity;
        }
    }

    public void DestroyAllBalls()
    {
        /*foreach (var ball in this.BallsList.ToList())
        {
            Destroy(ball.gameObject);
        }*/
        for (int i = BallsList.Count - 1; i >= 0; i--)
        {
            if (BallsList[i] != null)
            {
                Destroy(BallsList[i].gameObject);
                BallsList.RemoveAt(i);
            }
        }
    }

    private void GMActiveBallsUpdate()
    {
        GameManagerScript.Instance.ActiveBalls = BallsList.Count;
        //Debug.Log("Active balls: " + BallsList.Count);
    }

    public void MultiBalls(Vector3 position, int count, bool isLaser)
    {
        if (BallsList.Count < maxBalls)
        {
            for (int i = 0; i < count; i++)
            {
                BallScript spawnedBall = Instantiate(ballPrefab, position, Quaternion.identity) as BallScript;
                if (isLaser)
                {
                    spawnedBall.StartLaserBall();
                }
                Rigidbody2D spawnedBallRB = spawnedBall.GetComponent<Rigidbody2D>();
                spawnedBallRB.isKinematic = false;
                float initialDirectionX = Random.Range(-180, 180);
                float initialDirectionY = bmBallPaddleSpeed - Random.Range(1, 2);
                spawnedBallRB.AddForce(new Vector2(initialDirectionX, initialDirectionY));

                this.BallsList.Add(spawnedBall);
                GMActiveBallsUpdate();
            }
        }
    }

    public void SlowBalls()
    {
        bmBallStartSpeed = 320;
        timer.ResetTimer();
        timer.StartTimer(); 
        UpdateBallBounceSpeed();
        UpdateAllBallsSpeed();
    }
}