using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaddleScript : MonoBehaviour
{
    #region Singleton
    //creating a single instance of the PaddleScript script

    private static PaddleScript _instance;

    public static PaddleScript Instance => _instance;

    //checking that there is no other instances of the PaddleScript running
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

    //Variables used in the paddle movement
    private Camera mainCamera;
    private float paddleYPos;
    private float paddleWidth;
    //Variables used in the checking the paddle size, for variable paddle size power ups
    private SpriteRenderer spriteRend;
    private BoxCollider2D paddleCollider;
    private Coroutine sizeChangeCoroutine;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        paddleYPos = this.transform.position.y;
        spriteRend = GetComponent<SpriteRenderer>();
        paddleCollider = GetComponent<BoxCollider2D>();
        paddleWidth = spriteRend.size.x * spriteRend.sprite.pixelsPerUnit / mainCamera.pixelWidth;
    }

    // Update is called once per frame
    private void Update()
    {
        PaddleMovement();

        // Check for input and update the paddle size accordingly
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            ChangePaddleSizeAnimated(3.6f);
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            ChangePaddleSizeAnimated(2f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangePaddleSizeAnimated(1.2f);
        }
    }

    private void PaddleMovement()
    {
        // Get the current paddle width in pixels
        float halfPaddleWidth = spriteRend.size.x / 2;

        // Calculate the screen edges
        Vector3 leftScreenEdge = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightScreenEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        // Calculate the position of the paddle's edges in relation to the screen edges
        float leftEdge = leftScreenEdge.x + halfPaddleWidth;
        float rightEdge = rightScreenEdge.x - halfPaddleWidth;

        // Get the mouse position on the screen
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0));

        // Clamp the paddle edges, so they can't go past the screen edgeds
        float clampedXPos = Mathf.Clamp(mouseWorldPos.x, leftEdge, rightEdge);

        // Update the paddle's position
        this.transform.position = new Vector3(clampedXPos, paddleYPos, 0);
    }


    // Code to animate changing the paddle size over time
    public void ChangePaddleSizeAnimated(float newSize)
    {
        // If a size change is already in progress, stop it
        if (sizeChangeCoroutine != null)
        {
            StopCoroutine(sizeChangeCoroutine);
        }
        // Start the coroutine to change the paddle size smoothly
        sizeChangeCoroutine = StartCoroutine(ChangePaddleSizeCoroutine(newSize, .2f));
    }
    private IEnumerator ChangePaddleSizeCoroutine(float newSize, float duration)
    {
        float elapsedTime = 0;
        float startingSize = spriteRend.size.x;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentSize = Mathf.Lerp(startingSize, newSize, elapsedTime / duration);
            SetPaddleSize(currentSize);
            yield return null;
        }

        SetPaddleSize(newSize);
        sizeChangeCoroutine = null;
    }

    private void SetPaddleSize(float size)
    {
        spriteRend.size = new Vector2(size, spriteRend.size.y);
        paddleCollider.size = new Vector2(size, paddleCollider.size.y);
    }

    // Code for bouncing the Balls
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 paddleHitLocation = collision.contacts[0].point;
            Vector3 paddleCentre = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
            float paddleWidth = this.gameObject.GetComponent<BoxCollider2D>().size.x * this.gameObject.transform.localScale.x;

            ballRB.velocity = Vector2.zero;

            float difference = paddleCentre.x - paddleHitLocation.x;
            float normalizedDifference = difference / (paddleWidth / 2);

            // Calculate the X component of the direction vector
            // The factor "800" determines how extreme the angle will be when the hit location is closer to the edges
            float xComponent = -normalizedDifference * 800;

            // Calculate the Y component of the direction vector
            float yComponent = BallsManagerScript.Instance.bmBallCurrentSpeed;

            Vector2 direction = new Vector2(xComponent, yComponent);
            direction.Normalize();
            float ballSpeed = BallsManagerScript.Instance.bmBallCurrentSpeed;

            ballRB.AddForce(direction * ballSpeed);

            // Play the bounce sound
            SoundEffectPlayer.Instance.Bounce();
            GameManagerScript.Instance.ChangeScore(5);
        }
    }
}
