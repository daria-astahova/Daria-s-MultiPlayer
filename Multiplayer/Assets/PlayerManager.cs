using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviourPun
{
    public float moveSpeed = 5f;  // Player movement speed
    private Rigidbody rb;         // Rigidbody for physics-based movement

    // Ensure the PhotonView and Rigidbody are properly configured for syncing movement
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (photonView.IsMine)
        {
            // Local player: Handle movement and camera setup
            Camera.main.GetComponent<AudioListener>().enabled = true; // Main camera listens to local player
        }
        else
        {
            // Remote player: Disable physics and camera for remote players
            rb.isKinematic = true; // Disable physics for remote players (they don’t control movement)

            // Disable audio listener for remote players to prevent double audio input
            Camera playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera != null)
            {
                AudioListener audioListener = playerCamera.GetComponent<AudioListener>();
                if (audioListener != null)
                {
                    audioListener.enabled = false;  // Disable AudioListener for remote players
                }
            }
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;  // Only allow local player to control movement

        // Get input from the local player (only local player moves)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;

        // Move the player
        MovePlayer(movement);
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return; // Only update movement for local player

        // Physics-based updates or smooth movement can go here if necessary
    }

    private void MovePlayer(Vector3 movement)
    {
        // Move the player while keeping physics in check
        rb.MovePosition(transform.position + movement);
    }
}
