using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPowerUp : MonoBehaviour
{
    public Transform teleportTarget; // Set this in the Inspector to the destination point

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Adjust the tag based on your player's tag
        {
            TeleportPlayer(other.gameObject);
            DeactivatePowerUp(); 
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        player.transform.position = teleportTarget.position;
    }

    public void DeactivatePowerUp()
    {
        gameObject.SetActive(false); // Deactivate the power-up object
    }
}