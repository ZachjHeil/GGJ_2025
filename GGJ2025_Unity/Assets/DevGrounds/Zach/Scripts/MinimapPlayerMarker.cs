using UnityEngine;

public class MinimapPlayerMarker : MonoBehaviour
{
    public Transform player; // Reference to the player object

    void Update()
    {
        // Match the player's rotation on the Y-axis
        transform.rotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);
    }
}
