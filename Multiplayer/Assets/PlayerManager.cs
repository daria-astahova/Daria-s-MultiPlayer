using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{
    // Player movement speed
    public float moveSpeed = 5f;

    // For smoother movement, optional: Rigidbody to handle physics-based movement
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // If this player is the one controlled by the local player
        if (photonView.IsMine)
        {
            // Set up any necessary components for the local player
            rb = GetComponent<Rigidbody>();  // Assuming you're using Rigidbody-based movement

            // Here you could also set up camera control to follow the local player, etc.
        }
        else
        {
            // If this is a remote player, disable certain components to avoid control
            Destroy(GetComponent<PlayerManager>()); // Prevent other clients from controlling the remote player.
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Only allow the local player to control the character
        if (!photonView.IsMine)
        {
            return; // Return early if this player is not controlled by the local player
        }

        // Get input from the player (keyboard/controller)
        float moveX = Input.GetAxis("Horizontal");  // Left/Right movement (A/D or Arrow Keys)
        float moveZ = Input.GetAxis("Vertical");    // Forward/Backward movement (W/S or Arrow Keys)

        // Calculate movement direction
        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;

        // Move the player using Rigidbody
        rb.MovePosition(transform.position + movement);  // Move using Rigidbody to avoid directly manipulating the transform
    }

    // Optionally, you can add more features like jumping, rotating, etc., here.
}
