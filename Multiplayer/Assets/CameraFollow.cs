using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;  // Include Photon.Pun to access Photon functionality

public class CameraFollow : MonoBehaviourPun  // Make sure the script inherits from MonoBehaviourPun
{
    public Transform player;       // The player object to follow
    public Vector3 offset;         // The position offset of the camera relative to the player
    public float smoothSpeed = 0.125f;  // Speed at which the camera follows the player

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned in CameraFollow script.");
        }

        if (photonView.IsMine)  // Check if this camera is for the local player
        {
            player = this.transform; // Set the camera to follow the local player's transform
        }
        else
        {
            Destroy(this); // Destroy the script on remote players to avoid camera follow
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Only follow if the player exists and this camera is for the local player
        if (photonView.IsMine && player != null)
        {
            // Calculate the desired position
            Vector3 desiredPosition = player.position + offset;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;

            // Optionally, make the camera always look at the player
            transform.LookAt(player);
        }
    }
}
