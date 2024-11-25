using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro; // For TextMeshPro
using UnityEngine.UI; // For Unity's default Text

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerCountText; // For TextMeshPro
    public GameObject playerPrefab;         // The player prefab to instantiate

    void Start()
    {
        Debug.Log("Connecting to Photon...");
        PhotonNetwork.ConnectUsingSettings(); // Connects to Photon master server
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon master server in region: " + PhotonNetwork.CloudRegion);
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the lobby!");
        PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }public override void OnJoinedRoom()
{
    Debug.Log("Successfully joined the room: " + PhotonNetwork.CurrentRoom.Name);
    
    // Check if player prefab is assigned
    if (playerPrefab != null)
    {
        // Instantiate the player prefab at the origin (you can change this to a spawn point later)
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
    else
    {
        Debug.LogError("PlayerPrefab is not assigned in the Inspector!");
    }

    // Update the player count text only if it's assigned
    if (playerCountText != null)
    {
        UpdatePlayerCount();
    }
    else
    {
        Debug.LogError("PlayerCountText is not assigned in the Inspector!");
    }
}


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player joined the room: " + newPlayer.NickName);
        UpdatePlayerCount();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left the room: " + otherPlayer.NickName);
        UpdatePlayerCount();
    }

    private void UpdatePlayerCount()
    {
        if (playerCountText != null)
        {
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            int maxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;
            playerCountText.text = $"Players: {playerCount}/{maxPlayers}";
        }
        else
        {
            Debug.LogWarning("PlayerCountText is not assigned in the Inspector!");
        }
    }
}
