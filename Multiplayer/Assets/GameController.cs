using Photon.Pun;
using UnityEngine;

public class GameController : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;  // Assign this in the Inspector

    public override void OnJoinedRoom()
    {
        // Instantiates the player prefab for all clients in the room
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 1, Random.Range(-5f, 5f));  // Example random spawn position
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
    }
}
