using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    public float moveSpeed = 5f;  // Speed of movement
    public float turnSpeed = 700f;  // Speed of turning (rotation)

    private Rigidbody rb;

   void Start()
{
    // Get the Rigidbody component
    rb = GetComponent<Rigidbody>();
    
    // Check if Rigidbody is missing
    if (rb == null)
    {
        Debug.LogError("Rigidbody component not found on this GameObject!");
        return; // Exit the method if Rigidbody is not found
    }

    // If this player is not the local player (on other clients), disable the script to prevent multiple inputs
    if (!photonView.IsMine)
    {
        enabled = false;
    }
}


    void Update()
    {
        // Player movement and turning logic
        if (photonView.IsMine)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right arrow
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down arrow

        // Move the player
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);

        // Rotate the player to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }
}
