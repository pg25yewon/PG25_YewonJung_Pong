using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private float InitialSpeed = 10f; 
    [SerializeField] private float SpeedIncrease = 0.25f;  
    [SerializeField] private Text AIScore;  
    [SerializeField] private Text PlayerScore;  

    private int _hitCounter;  
    private Rigidbody2D _rb;  

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody component on startup
        Invoke("StartBall", 2f);  // Delay the initial ball movement for 2 seconds
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
        Invoke("StartBall", 2f);
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
        _rb.velocity = new Vector2(xDirection, yDirection) * (InitialSpeed + (SpeedIncrease * _hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "AI")
        {
            PlayerBounce(collision.transform);
        }
    }
}
