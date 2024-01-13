using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed; 
    [SerializeField] private bool isAI; 
    [SerializeField] private GameObject ball;  

    private Rigidbody2D _rb;  
    private Vector2 playerMove; 

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component on startup
    }

    void Update()
    {
        // Check if the player is controlled by AI or the player
        if (isAI)
        {
            AIControl();  
        }
        else
        {
            PlayerControl();  
        }
    }

    private void PlayerControl()
    {
        // Capture player input for vertical movement
        playerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    private void AIControl()
    {
        // AI logic to follow the ball's vertical position
        if (ball.transform.position.y > transform.position.y + 0.5f)
        {
            playerMove = new Vector2(0, 1);   // move up 
        }
        else if (ball.transform.position.y < transform.position.y - 0.5f)
        {
            playerMove = new Vector2(0, -1);  // move down
        }
        else
        {
            playerMove = new Vector2(0, 0);  // Stay still
        }
    }

    private void FixedUpdate()
    {
        // Apply the calculated movement to the player using Rigidbody velocity
        _rb.velocity = playerMove * movementSpeed;
    }

    public void Teleport(Vector3 teleportPosition)
    {
        transform.position = teleportPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleportPowerUp"))
        {
            TeleportPowerUp teleportPowerUp = other.GetComponent<TeleportPowerUp>();
            if (teleportPowerUp != null)
            {
                Teleport(teleportPowerUp.teleportTarget.position);
                teleportPowerUp.DeactivatePowerUp(); 
            }
        }
    }
}
