using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.UI; // For Unity's default Text

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI playerStatusText; // Or use Text if not using TextMeshPro

    public void UpdatePlayerStatus(string message)
    {
        if (playerStatusText != null)
        {
            playerStatusText.text = message;
        }
        else
        {
            Debug.LogWarning("PlayerStatusText is not assigned in the Inspector!");
        }
    }
}
