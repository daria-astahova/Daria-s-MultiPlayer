using Photon.Pun;
using UnityEngine;

public class GameController : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;  // Assign this in the Inspector

    public override void OnJoinedRoom()
    {
        // Instantiates the player prefab for all clients
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
}
