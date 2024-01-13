using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private float InitialSpeed = 10f;
    [SerializeField] private float SpeedIncrease = 0.25f;
    [SerializeField] private Text AIScore;
    [SerializeField] private Text PlayerScore;
    [SerializeField] private int WinningScore = 6;
    [SerializeField] private Canvas GameOver;

    private int _hitCounter;
    private Rigidbody2D _rb;
    private Camera _mainCamera;
    private float _originalCameraSize;
    private float _maxZoomSize = 5f; 

    void Start()
    {
        // Initialization
        GameOver.enabled = false;
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
        _originalCameraSize = _mainCamera.orthographicSize;
        Invoke("StartBall", 2f);
    }

    private void FixedUpdate()
    {
        // Limit the velocity magnitude to maintain consistent speed
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, InitialSpeed + (SpeedIncrease * _hitCounter));
    }

    private void StartBall()
    {
        // Set the initial velocity of the ball
        _rb.velocity = new Vector2(-1, 0) * (InitialSpeed + SpeedIncrease * _hitCounter);
    }

    private void ResetBall()
    {
        // Reset ball properties and invoke StartBall after a delay
        _rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        _hitCounter = 0;

        // Check for the winning condition
        if (int.Parse(PlayerScore.text) == WinningScore || int.Parse(AIScore.text) == WinningScore)
        {
            GameOver.enabled = true;
        }
        else
        {
            Invoke("StartBall", 2f);
        }
    }

    private void PlayerBounce(Transform myObject)
    {
        // Increment hit counter
        _hitCounter++;

        // Get ball and player positions
        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection, yDirection;

        // Determine x-direction based on the ball's position
        if (transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }

        // Calculate y-direction based on relative position to the player
        yDirection = (ballPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }

        // Set the new velocity based on the hit
        _rb.velocity = new Vector2(xDirection, yDirection) * (InitialSpeed + (SpeedIncrease * _hitCounter));

        // Zoom in by changing the camera's orthographic size
        StartCoroutine(ZoomCamera(_maxZoomSize, 0.5f));
    }

    private void UpdateCameraPosition()
    {
        // Smoothly adjust the camera position to follow the ball
        Vector3 ballPosition = transform.position;
        ballPosition.z = _mainCamera.transform.position.z; 
        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, ballPosition, 0.1f);
    }

    private void LateUpdate()
    {
        // Update the camera position after the ball has moved
        UpdateCameraPosition();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collisions with the player or AI
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "AI")
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for scoring and reset the ball
        if (transform.position.x > 0)
        {
            ResetBall();
            PlayerScore.text = (int.Parse(PlayerScore.text) + 1).ToString();
        }
        else if (transform.position.x < 0)
        {
            ResetBall();
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
        }
    }

    private IEnumerator ZoomCamera(float targetSize, float duration)
    {
        // Coroutine to smoothly zoom the camera
        float startTime = Time.time;
        float initialSize = _mainCamera.orthographicSize;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            _mainCamera.orthographicSize = Mathf.Lerp(initialSize, targetSize, t);
            yield return null;
        }

        _mainCamera.orthographicSize = targetSize;
    }
}
