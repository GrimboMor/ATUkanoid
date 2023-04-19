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
    // * Time.deltaTime

    // Here we set the speed for the ball to move
    public float bmBallStartSpeed = 400;
    public float bmBallCurrentSpeed = 400;
    public float bmBallMamimumSpeed = 700;
    // This variable is needed to reference to the coroutine, so that I can stop / restart it
    private Coroutine increaseBallSpeedCoroutine;


    //As there will be multiple balls (if I can get multiball powerup working) I need a list to manage them
    public List<BallScript> BallsList { get; set; }

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
                if (firstBall != null)
                {
                    firstBallRB.isKinematic = false;
                }
                bmBallCurrentSpeed = bmBallStartSpeed;
                if (increaseBallSpeedCoroutine != null) // Stop any previous coroutine
                {
                    StopCoroutine(increaseBallSpeedCoroutine);
                }
                increaseBallSpeedCoroutine = StartCoroutine(IncreaseBallSpeed(bmBallCurrentSpeed, 0f));
                if (firstBall != null)
                {
                    firstBallRB.AddForce(new Vector2(5, bmBallCurrentSpeed));
                }
                GameManagerScript.Instance.IsGameStarted = true;
            }
        }
    }

    private IEnumerator IncreaseBallSpeed(float startSpeed, float resetTimeCount)
    {
        float elapsedTime = resetTimeCount;
        float currentSpeed = startSpeed;

        while (currentSpeed < bmBallMamimumSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 40f;
            currentSpeed = Mathf.Lerp(startSpeed, bmBallMamimumSpeed, t);
            bmBallCurrentSpeed = currentSpeed;
            yield return null;
        }

        bmBallCurrentSpeed = bmBallMamimumSpeed;
        AdjustBallSpeeds();
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
        GMActiveBallsUpdate();
    }

    public void BallReset()
    {
        // Stop the IncreaseBallSpeed coroutine before destroying the balls
        if (increaseBallSpeedCoroutine != null)
        {
            StopCoroutine(increaseBallSpeedCoroutine);
            increaseBallSpeedCoroutine = null;
        }

        //Make sure all previous balls objects are destoyed
        DestroyAllBalls();
        InitialiseBall();
    }

    public void AdjustBallSpeeds()
    {
        foreach (var ball in BallsList)
        {
            Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
            Vector2 currentVelocity = ballRigidbody.velocity;
            float currentSpeed = currentVelocity.magnitude;
            float newSpeed = bmBallCurrentSpeed;

            if (Mathf.Approximately(currentSpeed, newSpeed))
            {
                continue;
            }

            Vector2 newVelocity = currentVelocity.normalized * newSpeed;
            ballRigidbody.velocity = newVelocity;
        }
    }

    public void DestroyAllBalls()
    {
        foreach (var ball in this.BallsList.ToList())
        {
            Destroy(ball.gameObject);
        }
    }

    private void GMActiveBallsUpdate()
    {
        GameManagerScript.Instance.ActiveBalls = BallsList.Count;
        //Debug.Log("Active balls: " + BallsList.Count);
    }

    public void MultiBalls(Vector3 position, int count)
    {
        if (BallsList.Count < maxBalls)
        {
            for (int i = 0; i < count; i++)
            {
                BallScript spawnedBall = Instantiate(ballPrefab, position, Quaternion.identity) as BallScript;
                Rigidbody2D spawnedBallRB = spawnedBall.GetComponent<Rigidbody2D>();
                spawnedBallRB.isKinematic = false;
                spawnedBallRB.AddForce(new Vector2(Random.Range(-180, 180), bmBallCurrentSpeed-(Random.Range(1, 5))));
                this.BallsList.Add(spawnedBall);
                GMActiveBallsUpdate();
            }
        }
    }
}